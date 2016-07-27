using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Messages;
using System.Dynamic;
using System.Linq.Expressions;
using FakeXrmEasy.Extensions;
using System.Reflection;
using Microsoft.Xrm.Sdk.Client;
using System.Globalization;
using System.Xml.Linq;
using FakeXrmEasy.Extensions.FetchXml;
using FakeXrmEasy.OrganizationFaults;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmFakedContext
    {
        protected internal Type FindReflectedType(string sLogicalName)
        {
            Assembly assembly = this.ProxyTypesAssembly;
            try
            {
                if (assembly == null)
                {
                    assembly = Assembly.GetExecutingAssembly();
                }
                var subClassType = assembly.GetTypes()
                        .Where(t => typeof(Entity).IsAssignableFrom(t))
                        .Where(t => t.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true).Length > 0)
                        .Where(t => ((EntityLogicalNameAttribute)t.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true)[0]).LogicalName.Equals(sLogicalName.ToLower()))
                        .FirstOrDefault();

                return subClassType;
            }
            catch (System.Reflection.ReflectionTypeLoadException ex)
            {
                // now look at ex.LoaderExceptions - this is an Exception[], so:
                string s = "";
                foreach (Exception inner in ex.LoaderExceptions)
                {
                    // write details of "inner", in particular inner.Message
                    s += inner.Message + " ";
                }

                throw new Exception("XrmFakedContext.FindReflectedType: " + s);
            }
            
        }

        protected internal Type FindReflectedAttributeType(Type earlyBoundType, string sAttributeName)
        {
            //Get that type properties
            var attributeInfo = earlyBoundType
                .GetProperties()
                .Where(pi => pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true).Length > 0)
                .Where(pi => (pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true)[0] as AttributeLogicalNameAttribute).LogicalName.Equals(sAttributeName))
                .FirstOrDefault();

            if(attributeInfo == null)
            {
                throw new Exception(string.Format("XrmFakedContext.FindReflectedAttributeType: Attribute {0} not found for type {1}" + sAttributeName, earlyBoundType.ToString()));
            }

            return attributeInfo.PropertyType;
        }

        public IQueryable<Entity> CreateQuery(string entityLogicalName)
        {
            return this.CreateQuery<Entity>(entityLogicalName);
        }

        public IQueryable<T> CreateQuery<T>() where T : Entity
        {
            Type typeParameter = typeof(T);

            if(ProxyTypesAssembly == null)
            {
                //Try to guess proxy types assembly
                var asm = Assembly.GetAssembly(typeof(T));
                if(asm != null)
                {
                    ProxyTypesAssembly = asm;
                }
            }
            string sLogicalName = "";

            if (typeParameter.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true).Length > 0)
            {
                sLogicalName = (typeParameter.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true)[0] as EntityLogicalNameAttribute).LogicalName;
            }

            return this.CreateQuery<T>(sLogicalName);
        }

        protected IQueryable<T> CreateQuery<T>(string entityLogicalName) where T : Entity
        {
            List<T> lst = new List<T>();
            var subClassType = FindReflectedType(entityLogicalName);
            if ((subClassType == null && !(typeof(T).Equals(typeof(Entity))))
                || (typeof(T).Equals(typeof(Entity)) && string.IsNullOrWhiteSpace(entityLogicalName)))
            {
                throw new Exception(string.Format("The type {0} was not found", entityLogicalName));
            }

            if (!Data.ContainsKey(entityLogicalName))
            {
                return lst.AsQueryable<T>(); //Empty list
            }

            foreach (var e in Data[entityLogicalName].Values)
            {
                if (subClassType != null)
                {
                    var instance = Activator.CreateInstance(subClassType);

                    //Fix for Linux/Mono =>  error CS0445: Cannot modify the result of an unboxing conversion
                    T unboxed = (T)instance; 
                    unboxed.Attributes = e.Attributes;
                    unboxed.Id = e.Id;
                    lst.Add(unboxed);
                }
                else
                    lst.Add((T)e);
            }

            return lst.AsQueryable<T>();
        }

        public IQueryable<Entity> CreateQueryFromEntityName(string s)
        {
            return Data[s].Values.AsQueryable();
        }

        public static IQueryable<Entity> TranslateLinkedEntityToLinq(XrmFakedContext context, LinkEntity le, IQueryable<Entity> query, ColumnSet previousColumnSet, string linkFromAlias = "") {
            
            var leAlias = string.IsNullOrWhiteSpace(le.EntityAlias) ? le.LinkToEntityName : le.EntityAlias;
            context.EnsureEntityNameExistsInMetadata(le.LinkFromEntityName);
            context.EnsureEntityNameExistsInMetadata(le.LinkToEntityName);

            if (!context.AttributeExistsInMetadata(le.LinkToEntityName, le.LinkToAttributeName))
            {
                OrganizationServiceFaultQueryBuilderNoAttributeException.Throw(le.LinkToAttributeName);
            }

            var inner = context.CreateQuery<Entity>(le.LinkToEntityName);

            //if (!le.Columns.AllColumns && le.Columns.Columns.Count == 0)
            //{
            //    le.Columns.AllColumns = true;   //Add all columns in the joined entity, otherwise we can't filter by related attributes, then the Select will actually choose which ones we need
            //}

            if (string.IsNullOrWhiteSpace(linkFromAlias))
            {
                linkFromAlias = le.LinkFromAttributeName;
            }
            else
            {
                linkFromAlias += "." + le.LinkFromAttributeName;
            }

            switch (le.JoinOperator)
            {
                case JoinOperator.Inner:
                case JoinOperator.Natural:
                    query = query.Join(inner,
                                    outerKey => outerKey.KeySelector(linkFromAlias, context),
                                    innerKey => innerKey.KeySelector(le.LinkToAttributeName, context),
                                    (outerEl, innerEl) => outerEl
                                                            .JoinAttributes(innerEl, new ColumnSet(true), leAlias, context));

                    break;
                case JoinOperator.LeftOuter:
                    query = query.GroupJoin(inner,
                                    outerKey => outerKey.KeySelector(le.LinkFromAttributeName, context),
                                    innerKey => innerKey.KeySelector(le.LinkToAttributeName, context),
                                    (outerEl, innerElemsCol) => new { outerEl, innerElemsCol })
                                                .SelectMany(x => x.innerElemsCol.DefaultIfEmpty()
                                                            , (x, y) => x.outerEl
                                                                            .JoinAttributes(y, new ColumnSet(true), leAlias, context));


                    break;
                default: //This shouldn't be reached as there are only 3 types of Join...
                    throw new PullRequestException(string.Format("The join operator {0} is currently not supported. Feel free to implement and send a PR.", le.JoinOperator));

            }

            //Process nested linked entities recursively
            foreach (LinkEntity nestedLinkedEntity in le.LinkEntities)
            {
                if (string.IsNullOrWhiteSpace(le.EntityAlias))
                    le.EntityAlias = le.LinkToEntityName;

                query = TranslateLinkedEntityToLinq(context, nestedLinkedEntity, query, le.Columns, le.EntityAlias);
            }
            return query;
        }

        

        protected static XElement RetrieveFetchXmlNode(XDocument xlDoc, string sName)
        {
            return xlDoc.Descendants().Where(e => e.Name.LocalName.Equals(sName)).FirstOrDefault();
        }


        public static QueryExpression TranslateFetchXmlToQueryExpression(XrmFakedContext context, string fetchXml)
        {
            XDocument xlDoc = null;
            try {
                xlDoc = XDocument.Parse(fetchXml);
            }
            catch
            {
                throw new Exception("FetchXml must be a valid XML document");
            }

            //Validate nodes
            if (!xlDoc.Descendants().All(el => el.IsFetchXmlNodeValid()))
                throw new Exception("At least some node is not valid");

            //Root node
            if (!xlDoc.Root.Name.LocalName.Equals("fetch"))
            {
                throw new Exception("Root node must be fetch");
            }

            var entityNode = RetrieveFetchXmlNode(xlDoc, "entity");
            var query = new QueryExpression(entityNode.GetAttribute("name").Value);

            query.ColumnSet = xlDoc.ToColumnSet();

            var orders = xlDoc.ToOrderExpressionList();
            foreach(var order in orders)
            {
                query.AddOrder(order.AttributeName, order.OrderType);
            }

            query.Criteria = xlDoc.ToCriteria(context);

            query.TopCount = xlDoc.ToTopCount();

            var linkedEntities = xlDoc.ToLinkEntities(context);
            foreach(var le in linkedEntities)
            {
                query.LinkEntities.Add(le);
            }

            return query;
        }

        public static IQueryable<Entity> TranslateQueryExpressionToLinq(XrmFakedContext context, QueryExpression qe)
        {
            if (qe == null) return null;

            //Start form the root entity and build a LINQ query to execute the query against the In-Memory context:
            context.EnsureEntityNameExistsInMetadata(qe.EntityName);

            IQueryable<Entity> query = null;

            query = context.CreateQuery<Entity>(qe.EntityName);

            //Add as many Joins as linked entities
            foreach (LinkEntity le in qe.LinkEntities)
            {
                query = TranslateLinkedEntityToLinq(context, le, query, qe.ColumnSet);
            }

            // Compose the expression tree that represents the parameter to the predicate.
            ParameterExpression entity = Expression.Parameter(typeof(Entity));
            var expTreeBody = TranslateQueryExpressionFiltersToExpression(qe, entity);
            Expression<Func<Entity, bool>> lambda = Expression.Lambda<Func<Entity, bool>>(expTreeBody, entity);
            query = query.Where(lambda);

            //Project the attributes in the root column set  (must be applied after the where clause, not before!!)
            query = query.Select(x => x.ProjectAttributes(qe, context));

            //Sort results
            if (qe.Orders != null)
            {
                foreach (var order in qe.Orders)
                {
                    if (order.OrderType == OrderType.Ascending)
                        query = query.OrderBy(e => e.Attributes.ContainsKey(order.AttributeName) ? e.Attributes[order.AttributeName] : null, new XrmOrderByAttributeComparer());
                    else
                        query = query.OrderByDescending(e => e.Attributes.ContainsKey(order.AttributeName) ? e.Attributes[order.AttributeName] : null, new XrmOrderByAttributeComparer());
                }
            }

            //Apply TopCount
            if(qe.TopCount != null)
            {
                query = query.Take(qe.TopCount.Value);
            }
            return query;
        }

        protected static Expression TranslateConditionExpression(ConditionExpression c, ParameterExpression entity)
        {
            Expression attributesProperty = Expression.Property(
                entity,
                "Attributes"
                );


            //If the attribute comes from a joined entity, then we need to access the attribute from the join
            //But the entity name attribute only exists >= 2013
#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016
            string attributeName = "";

            if (!string.IsNullOrWhiteSpace(c.EntityName))
            {
                attributeName = c.EntityName + "." + c.AttributeName;
            }
            else
                attributeName = c.AttributeName;

            Expression containsAttributeExpression = Expression.Call(
                attributesProperty,
                typeof(AttributeCollection).GetMethod("ContainsKey", new Type[] { typeof(string) }),
                Expression.Constant(attributeName)
                );

            Expression getAttributeValueExpr = Expression.Property(
                            attributesProperty, "Item",
                            Expression.Constant(attributeName, typeof(string))
                            );
#else
            Expression containsAttributeExpression = Expression.Call(
                attributesProperty,
                typeof(AttributeCollection).GetMethod("ContainsKey", new Type[] { typeof(string) }),
                Expression.Constant(c.AttributeName)
                );

            Expression getAttributeValueExpr = Expression.Property(
                            attributesProperty, "Item",
                            Expression.Constant(c.AttributeName, typeof(string))
                            );
#endif



            Expression getNonBasicValueExpr = getAttributeValueExpr;

            switch (c.Operator)
            {
                case ConditionOperator.Equal:
                case ConditionOperator.Today:
                case ConditionOperator.Yesterday:
                case ConditionOperator.Tomorrow:
                    return TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.BeginsWith:
                case ConditionOperator.Like:
                    return TranslateConditionExpressionLike(c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.EndsWith:
                    return TranslateConditionExpressionEndsWith(c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.Contains:
                    return TranslateConditionExpressionContains(c, getNonBasicValueExpr, containsAttributeExpression);
                
                case ConditionOperator.NotEqual:
                    return Expression.Not(TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.DoesNotBeginWith:
                case ConditionOperator.DoesNotEndWith:
                case ConditionOperator.NotLike:
                case ConditionOperator.DoesNotContain:
                    return Expression.Not(TranslateConditionExpressionLike(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.Null:
                    return TranslateConditionExpressionNull(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.NotNull:
                    return Expression.Not(TranslateConditionExpressionNull(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.GreaterThan:
                    return TranslateConditionExpressionGreaterThan(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.GreaterEqual:
                    return Expression.Or(
                                TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionGreaterThan(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.LessThan:
                    return TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.LessEqual:
                    return Expression.Or(
                                TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.In:
                    return TranslateConditionExpressionIn(c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.NotIn:
                    return Expression.Not(TranslateConditionExpressionIn(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.On:
                    return TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.NotOn:
                    return Expression.Not(TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.OnOrAfter:
                    return Expression.Or(
                               TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression),
                               TranslateConditionExpressionGreaterThan(c, getNonBasicValueExpr, containsAttributeExpression));
                case ConditionOperator.OnOrBefore:
                    return Expression.Or(
                                TranslateConditionExpressionEqual(c, getNonBasicValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression));

                default:
                    throw new PullRequestException(string.Format("Operator {0} not yet implemented for condition expression", c.Operator.ToString()));


            }
        }
        protected static Expression GetAppropiateTypedValue(object value)
        {
            //Basic types conversions
            //Special case => datetime is sent as a string
            if (value is string)
            {
                DateTime dtDateTimeConversion;
                if (DateTime.TryParse(value.ToString(), CultureInfo.InvariantCulture,DateTimeStyles.AdjustToUniversal, out dtDateTimeConversion))
                {
                    return Expression.Constant(dtDateTimeConversion, typeof(DateTime));
                }
                else
                {
                    return GetCaseInsensitiveExpression(Expression.Constant(value, typeof(string)));
                }
            }
            else if(value is EntityReference) {
                var cast = (value as EntityReference).Id;
                return Expression.Constant(cast);
            }
            else if (value is OptionSetValue)
            {
                var cast = (value as OptionSetValue).Value;
                return Expression.Constant(cast);
            }
            else if (value is Money)
            {
                var cast = (value as Money).Value;
                return Expression.Constant(cast);
            }
            return Expression.Constant(value);
        }

        protected static Expression GetAppropiateCastExpressionBasedOnValue(Expression input, object value)
        {
            var typedExpression = GetAppropiateCastExpressionBasedOnValueInherentType(input, value);

            //Now, any value (entity reference, string, int, etc,... could be wrapped in an AliasedValue object
            //So let's add this
            var getValueFromAliasedValueExp = Expression.Call(Expression.Convert(input, typeof(AliasedValue)),
                                            typeof(AliasedValue).GetMethod("get_Value"));

            var  exp = Expression.Condition(Expression.TypeIs(input, typeof(AliasedValue)),
                    GetAppropiateCastExpressionBasedOnValueInherentType(getValueFromAliasedValueExp, value),
                    typedExpression //Not an aliased value
                );

            return exp;
        }

        protected static Expression GetAppropiateCastExpressionBasedOnValueInherentType(Expression input, object value)
        {
            if (value is Guid || value is EntityReference)
                return GetAppropiateCastExpressionBasedGuid(input); //Could be compared against an EntityReference
            if (value is int || value is OptionSetValue)
                return GetAppropiateCastExpressionBasedOnInt(input); //Could be compared against an OptionSet
            if (value is decimal || value is Money)
                return GetAppropiateCastExpressionBasedOnDecimal(input); //Could be compared against a Money
            if (value is bool)
                return GetAppropiateCastExpressionBasedOnBoolean(input); //Could be a BooleanManagedProperty
            if (value is string)
            {
                return GetAppropiateCastExpressionBasedOnString(input, value);
            }
            return GetAppropiateCastExpressionDefault(input, value); //any other type
        }
        protected static Expression GetAppropiateCastExpressionBasedOnString(Expression input, object value)
        {
            DateTime dtDateTimeConversion;
            if (DateTime.TryParse(value.ToString(), out dtDateTimeConversion))
            {
                return Expression.Convert(input, typeof(DateTime));
            }

            return GetCaseInsensitiveExpression(GetAppropiateCastExpressionDefault(input, value)); //Non datetime string
        }

        protected static Expression GetAppropiateCastExpressionDefault(Expression input, object value)
        {
            return Expression.Convert(input, value.GetType());  //Default type conversion
        }
        protected static Expression GetAppropiateCastExpressionBasedGuid(Expression input)
        {
            var getIdFromEntityReferenceExpr = Expression.Call(Expression.TypeAs(input, typeof(EntityReference)),
                                            typeof(EntityReference).GetMethod("get_Id"));

            return Expression.Condition(
                        Expression.TypeIs(input, typeof(EntityReference)),  //If input is an entity reference, compare the Guid against the Id property
                                Expression.Convert(
                                            getIdFromEntityReferenceExpr,
                                            typeof(Guid)),
                                Expression.Condition(Expression.TypeIs(input, typeof(Guid)),  //If any other case, then just compare it as a Guid directly
                                            Expression.Convert(input, typeof(Guid)),
                                            Expression.Constant(Guid.Empty, typeof(Guid))));

        }

        protected static Expression GetAppropiateCastExpressionBasedOnDecimal(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(Money)),
                                Expression.Convert(
                                    Expression.Call(Expression.TypeAs(input, typeof(Money)),
                                            typeof(Money).GetMethod("get_Value")),
                                            typeof(decimal)),
                           Expression.Condition(Expression.TypeIs(input, typeof(decimal)),
                                        Expression.Convert(input, typeof(decimal)),
                                        Expression.Constant(0.0M)));

        }

        protected static Expression GetAppropiateCastExpressionBasedOnBoolean(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(BooleanManagedProperty)),
                                Expression.Convert(
                                    Expression.Call(Expression.TypeAs(input, typeof(BooleanManagedProperty)),
                                            typeof(BooleanManagedProperty).GetMethod("get_Value")),
                                            typeof(bool)),
                           Expression.Condition(Expression.TypeIs(input, typeof(bool)),
                                        Expression.Convert(input, typeof(bool)),
                                        Expression.Constant(false)));

        }

        protected static Expression GetAppropiateCastExpressionBasedOnInt(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(OptionSetValue)), 
                                            Expression.Convert(
                                                Expression.Call(Expression.TypeAs(input, typeof(OptionSetValue)),
                                                        typeof(OptionSetValue).GetMethod("get_Value")),
                                                        typeof(int)),
                                                    Expression.Convert(input, typeof(int)));
        }
        protected static Expression TranslateConditionExpressionEqual(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            object unaryOperatorValue = null;

            switch(c.Operator)
            {
                case ConditionOperator.Today:
                    unaryOperatorValue = DateTime.Today;
                    break;
                case ConditionOperator.Yesterday:
                    unaryOperatorValue = DateTime.Today.AddDays(-1);
                    break;
                case ConditionOperator.Tomorrow:
                    unaryOperatorValue = DateTime.Today.AddDays(1);
                    break;
            }
            if (unaryOperatorValue != null)
            {
                //c.Values empty in this case
                expOrValues = Expression.Equal(
                                GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, unaryOperatorValue),
                                GetAppropiateTypedValue(unaryOperatorValue));
            }
            else
            {
                foreach (object value in c.Values)
                {

                    expOrValues = Expression.Or(expOrValues, Expression.Equal(
                                GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, value),
                                GetAppropiateTypedValue(value)));


                }
            }
            
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionIn(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {

                expOrValues = Expression.Or(expOrValues, Expression.Equal(
                            GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, value),
                            GetAppropiateTypedValue(value)));


            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        //protected static Expression TranslateConditionExpressionOn(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        //{
        //    BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
        //    foreach (object value in c.Values)
        //    {

        //        expOrValues = Expression.Or(expOrValues, Expression.Equal(
        //                    GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, value),
        //                    GetAppropiateTypedValue(value)));


        //    }
        //    return Expression.AndAlso(
        //                    containsAttributeExpr,
        //                    Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
        //                        expOrValues));
        //}

        protected static Expression TranslateConditionExpressionGreaterThan(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues,
                        Expression.GreaterThan(
                            GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, value),
                            GetAppropiateTypedValue(value)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionLessThan(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues,
                        Expression.LessThan(
                            GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, value),
                            GetAppropiateTypedValue(value)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionNull(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            return Expression.Or(Expression.AndAlso(
                                    containsAttributeExpr,
                                    Expression.Equal(
                                    getAttributeValueExpr,
                                    Expression.Constant(null))),   //Attribute is null
                                 Expression.AndAlso(
                                    Expression.Not(containsAttributeExpr),
                                    Expression.Constant(true)));   //Or attribute is not defined (null)
        }

        protected static Expression TranslateConditionExpressionEndsWith(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString()).ToList() );
            return TranslateConditionExpressionLike(computedCondition, getAttributeValueExpr, containsAttributeExpr);
        }

        protected static Expression GetCaseInsensitiveExpression(Expression e)
        {
            return Expression.Call(e,
                                typeof(string).GetMethod("ToLowerInvariant", new Type[] { }));
        }

        protected static Expression TranslateConditionExpressionLike(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            Expression convertedValueToStr = Expression.Convert(getAttributeValueExpr, typeof(string));

            Expression convertedValueToStrAndToLower = GetCaseInsensitiveExpression(convertedValueToStr);

            string sLikeOperator = "%";
            foreach (object value in c.Values)
            {
                var strValue = value.ToString();
                string sMethod = "";

                if (strValue.EndsWith(sLikeOperator) && strValue.StartsWith(sLikeOperator))
                    sMethod = "Contains";

                else if (strValue.StartsWith(sLikeOperator))
                    sMethod = "EndsWith";

                else
                    sMethod = "StartsWith";

                expOrValues = Expression.Or(expOrValues, Expression.Call(
                    convertedValueToStrAndToLower,
                    typeof(string).GetMethod(sMethod, new Type[] { typeof(string) }),
                    Expression.Constant(value.ToString().ToLowerInvariant().Replace("%", "")) //Linq2CRM adds the percentage value to be executed as a LIKE operator, here we are replacing it to just use the appropiate method
                ));
            }

            return Expression.AndAlso(
                            containsAttributeExpr,
                            expOrValues);
        }

        protected static Expression TranslateConditionExpressionContains(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString() + "%").ToList());
            return TranslateConditionExpressionLike(computedCondition, getAttributeValueExpr, containsAttributeExpr);
        
        }

        protected static BinaryExpression TranslateMultipleConditionExpressions(List<ConditionExpression> conditions, LogicalOperator op, ParameterExpression entity)
        {
            BinaryExpression binaryExpression = null;  //Default initialisation depending on logical operator
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var c in conditions)
            {
                //Build a binary expression  
                if (op == LogicalOperator.And)
                {
                    binaryExpression = Expression.And(binaryExpression, TranslateConditionExpression(c, entity));
                }
                else
                    binaryExpression = Expression.Or(binaryExpression, TranslateConditionExpression(c, entity));
            }

            return binaryExpression;
        }

        protected static BinaryExpression TranslateMultipleFilterExpressions(List<FilterExpression> filters, LogicalOperator op, ParameterExpression entity)
        {
            BinaryExpression binaryExpression = null;
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var f in filters)
            {
                var thisFilterLambda = TranslateFilterExpressionToExpression(f, entity);

                //Build a binary expression  
                if (op == LogicalOperator.And)
                {
                    binaryExpression = Expression.And(binaryExpression, thisFilterLambda);
                }
                else
                    binaryExpression = Expression.Or(binaryExpression, thisFilterLambda);
            }

            return binaryExpression;
        }

        protected static Expression TranslateLinkedEntityFilterExpressionToExpression(LinkEntity le, ParameterExpression entity)
        {
            //In CRM 2011, condition expressions are at the LinkEntity level without an entity name
            //From CRM 2013, condition expressions were moved to outside the LinkEntity object at the QueryExpression level,
            //with an EntityName alias attribute

            //If we reach this point, it means we are translating filters at the Link Entity level (2011),
            //Therefore we need to prepend the alias attribute because the code to generate attributes for Joins (JoinAttribute extension) is common across versions
            
            if(le.LinkCriteria != null)
            {
                foreach (var ce in le.LinkCriteria.Conditions)
                {
                    var entityAlias = !string.IsNullOrEmpty(le.EntityAlias) ? le.EntityAlias : le.LinkToEntityName;
                    ce.AttributeName = entityAlias + "." + ce.AttributeName;
                }
            }
            
            return TranslateFilterExpressionToExpression(le.LinkCriteria, entity);
        }

        protected static Expression TranslateQueryExpressionFiltersToExpression(QueryExpression qe, ParameterExpression entity)
        {
            var linkedEntitiesQueryExpressions = new List<Expression>();
            foreach(var le in qe.LinkEntities)
            {
                var e = TranslateLinkedEntityFilterExpressionToExpression(le, entity);
                linkedEntitiesQueryExpressions.Add(e);
            }

            if(linkedEntitiesQueryExpressions.Count > 0 && qe.Criteria != null)
            {
                //Return the and of the two
                Expression andExpression = Expression.Constant(true);
                foreach(var e in linkedEntitiesQueryExpressions)
                {
                    andExpression = Expression.And(e, andExpression);

                }
                var feExpression = TranslateFilterExpressionToExpression(qe.Criteria, entity);
                return Expression.And(andExpression, feExpression);
            }
            else if (linkedEntitiesQueryExpressions.Count > 0)
            {
                //Linked entity expressions only
                Expression andExpression = Expression.Constant(true);
                foreach (var e in linkedEntitiesQueryExpressions)
                {
                    andExpression = Expression.And(e, andExpression);

                }
                return andExpression;
            }
            else
            {
                //Criteria only
                return TranslateFilterExpressionToExpression(qe.Criteria, entity);
            }
        }
        protected static Expression TranslateFilterExpressionToExpression(FilterExpression fe, ParameterExpression entity)
        {
            if (fe == null) return Expression.Constant(true);

            BinaryExpression conditionsLambda = null;
            BinaryExpression filtersLambda = null;
            if (fe.Conditions != null && fe.Conditions.Count > 0)
            {
                conditionsLambda = TranslateMultipleConditionExpressions(fe.Conditions.ToList(), fe.FilterOperator, entity);
            }

            //Process nested filters recursively
            if (fe.Filters != null && fe.Filters.Count > 0)
            {
                filtersLambda = TranslateMultipleFilterExpressions(fe.Filters.ToList(), fe.FilterOperator, entity);
            }

            if (conditionsLambda != null && filtersLambda != null)
            {
                //Satisfy both
                if (fe.FilterOperator == LogicalOperator.And)
                {
                    return Expression.And(conditionsLambda, filtersLambda);
                }
                else
                {
                    return Expression.Or(conditionsLambda, filtersLambda);
                }
            }
            else if (conditionsLambda != null)
                return conditionsLambda;
            else if (filtersLambda != null)
                return filtersLambda;

            return Expression.Constant(true); //Satisfy filter if there are no conditions nor filters
        }
    }
}