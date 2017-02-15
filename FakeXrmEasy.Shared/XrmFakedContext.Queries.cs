﻿using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Messages;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using FakeXrmEasy.Extensions;
using System.Reflection;
using Microsoft.Xrm.Sdk.Client;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.Schema;
using FakeXrmEasy.Extensions.FetchXml;
using FakeXrmEasy.OrganizationFaults;
using FakeXrmEasy.Models;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmContext
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
                throw new Exception(string.Format("XrmFakedContext.FindReflectedAttributeType: Attribute {0} not found for type {1}", sAttributeName, earlyBoundType.ToString()));
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
                    var cloned = e.Clone(subClassType);
                    lst.Add((T)cloned);
                }
                else
                    lst.Add((T)e.Clone());
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

        public static XDocument ParseFetchXml(string fetchXml)
        {            
            try
            {
                return XDocument.Parse(fetchXml);                
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FetchXml must be a valid XML document: {0}", ex.ToString()));
            }
        }
       
        public static QueryExpression TranslateFetchXmlToQueryExpression(XrmFakedContext context, string fetchXml)
        {
            return TranslateFetchXmlDocumentToQueryExpression(context, ParseFetchXml(fetchXml));
        }

        public static QueryExpression TranslateFetchXmlDocumentToQueryExpression(XrmFakedContext context, XDocument xlDoc)
        {
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

            // Ordering is done after grouping/aggregation
            if (!xlDoc.IsAggregateFetchXml())
            {
                var orders = xlDoc.ToOrderExpressionList();
                foreach (var order in orders)
                {
                    query.AddOrder(order.AttributeName, order.OrderType);
                }
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
            var expTreeBody = TranslateQueryExpressionFiltersToExpression(context, qe, entity);
            Expression<Func<Entity, bool>> lambda = Expression.Lambda<Func<Entity, bool>>(expTreeBody, entity);
            query = query.Where(lambda);

            //Project the attributes in the root column set  (must be applied after the where clause, not before!!)
            query = query.Select(x => x.Clone(x.GetType()).ProjectAttributes(qe, context));

            //Sort results
            if (qe.Orders != null)
            {
                if(qe.Orders.Count > 0)
                {
                    IOrderedQueryable<Entity> orderedQuery = null;

                    var order = qe.Orders[0];
                    if (order.OrderType == OrderType.Ascending)
                        orderedQuery = query.OrderBy(e => e.Attributes.ContainsKey(order.AttributeName) ? e[order.AttributeName] : null, new XrmOrderByAttributeComparer());
                    else
                        orderedQuery = query.OrderByDescending(e => e.Attributes.ContainsKey(order.AttributeName) ? e[order.AttributeName] : null, new XrmOrderByAttributeComparer());

                    //Subsequent orders should use ThenBy and ThenByDescending
                    for (var i = 1; i < qe.Orders.Count; i++)
                    {
                        var thenOrder = qe.Orders[i];
                        if (thenOrder.OrderType == OrderType.Ascending)
                            orderedQuery = orderedQuery.ThenBy(e => e.Attributes.ContainsKey(thenOrder.AttributeName) ? e[thenOrder.AttributeName] : null, new XrmOrderByAttributeComparer());
                        else
                            orderedQuery = orderedQuery.ThenByDescending(e => e[thenOrder.AttributeName], new XrmOrderByAttributeComparer());
                    }

                    query = orderedQuery;
                }
            }

            //Apply TopCount
            if (qe.TopCount != null)
            {
                query = query.Take(qe.TopCount.Value);
            }
            return query;
        }
        

        protected static Expression TranslateConditionExpression(XrmFakedContext context, TypedConditionExpression c, ParameterExpression entity)
        {
            Expression attributesProperty = Expression.Property(
                entity,
                "Attributes"
                );


            //If the attribute comes from a joined entity, then we need to access the attribute from the join
            //But the entity name attribute only exists >= 2013
#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365
            string attributeName = "";

            if (!string.IsNullOrWhiteSpace(c.CondExpression.EntityName))
            {
                attributeName = c.CondExpression.EntityName + "." + c.CondExpression.AttributeName;
            }
            else
                attributeName = c.CondExpression.AttributeName;

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
                Expression.Constant(c.CondExpression.AttributeName)
                );

            Expression getAttributeValueExpr = Expression.Property(
                            attributesProperty, "Item",
                            Expression.Constant(c.CondExpression.AttributeName, typeof(string))
                            );
#endif



            Expression getNonBasicValueExpr = getAttributeValueExpr;

            switch (c.CondExpression.Operator)
            {
                case ConditionOperator.Equal:
                case ConditionOperator.Today:
                case ConditionOperator.Yesterday:
                case ConditionOperator.Tomorrow:
                case ConditionOperator.EqualUserId:
                    return TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.NotEqualUserId:
                    return Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.BeginsWith:
                case ConditionOperator.Like:
                    return TranslateConditionExpressionLike(c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.EndsWith:
                    return TranslateConditionExpressionEndsWith(c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.Contains:
                    return TranslateConditionExpressionContains(c, getNonBasicValueExpr, containsAttributeExpression);
                
                case ConditionOperator.NotEqual:
                    return Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));

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
                    return TranslateConditionExpressionGreaterThanOrEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.LessThan:
                    return TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.LessEqual:
                    return TranslateConditionExpressionLessThanOrEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.In:
                    return TranslateConditionExpressionIn(c, getNonBasicValueExpr, containsAttributeExpression);
                case ConditionOperator.NotIn:
                    return Expression.Not(TranslateConditionExpressionIn(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.On:
                    return TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.NotOn:
                    return Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.OnOrAfter:
                    return Expression.Or(
                               TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression),
                               TranslateConditionExpressionGreaterThan(c, getNonBasicValueExpr, containsAttributeExpression));
                case ConditionOperator.OnOrBefore:
                    return Expression.Or(
                                TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression));

                case ConditionOperator.Between:
                    if(c.CondExpression.Values.Count != 2)
                    {
                        throw new Exception("Between operator requires exactly 2 values.");
                    }
                    return TranslateConditionExpressionBetween(c, getNonBasicValueExpr, containsAttributeExpression);

                case ConditionOperator.NotBetween:
                    if (c.CondExpression.Values.Count != 2)
                    {
                        throw new Exception("Not-Between operator requires exactly 2 values.");
                    }
                    return Expression.Not(TranslateConditionExpressionBetween(c, getNonBasicValueExpr, containsAttributeExpression));

                
                    
                    
                default:
                    throw new PullRequestException(string.Format("Operator {0} not yet implemented for condition expression", c.CondExpression.Operator.ToString()));


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

        protected static Expression GetAppropiateTypedValueAndType(object value, Type attributeType)
        {
            if (attributeType == null)
                return GetAppropiateTypedValue(value);


            //Basic types conversions
            //Special case => datetime is sent as a string
            if (value is string)
            {
                int iValue;

                DateTime dtDateTimeConversion;
                if (attributeType.IsDateTime()  //Only convert to DateTime if the attribute's type was DateTime
                    && DateTime.TryParse(value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dtDateTimeConversion))
                {
                    return Expression.Constant(dtDateTimeConversion, typeof(DateTime));
                }
                else if (attributeType.IsOptionSet() && int.TryParse(value.ToString(), out iValue))
                {
                    return Expression.Constant(iValue, typeof(int));
                }
                else
                {
                    return GetCaseInsensitiveExpression(Expression.Constant(value, typeof(string)));
                }
            }
            else if (value is EntityReference)
            {
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

        protected static Expression GetAppropiateCastExpressionBasedOnType(Type t, Expression input, object value)
        {
            var typedExpression = GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(input, value, t);

            //Now, any value (entity reference, string, int, etc,... could be wrapped in an AliasedValue object
            //So let's add this
            var getValueFromAliasedValueExp = Expression.Call(Expression.Convert(input, typeof(AliasedValue)),
                                            typeof(AliasedValue).GetMethod("get_Value"));

            var exp = Expression.Condition(Expression.TypeIs(input, typeof(AliasedValue)),
                    GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(getValueFromAliasedValueExp, value, t),
                    typedExpression //Not an aliased value
                );

            return exp;
        }

        //protected static Expression GetAppropiateCastExpressionBasedOnValue(XrmFakedContext context, Expression input, object value)
        //{
        //    var typedExpression = GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(context, input, value, sEntityName, sAttributeName);

        //    //Now, any value (entity reference, string, int, etc,... could be wrapped in an AliasedValue object
        //    //So let's add this
        //    var getValueFromAliasedValueExp = Expression.Call(Expression.Convert(input, typeof(AliasedValue)),
        //                                    typeof(AliasedValue).GetMethod("get_Value"));

        //    var  exp = Expression.Condition(Expression.TypeIs(input, typeof(AliasedValue)),
        //            GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(context, getValueFromAliasedValueExp, value, sEntityName, sAttributeName),
        //            typedExpression //Not an aliased value
        //        );

        //    return exp;
        //}

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

        protected static Expression GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(Expression input, object value, Type attributeType)
        {
            if(attributeType != null)
                {

                    #if FAKE_XRM_EASY
                    if (attributeType == typeof(Microsoft.Xrm.Client.CrmEntityReference))
                            return GetAppropiateCastExpressionBasedGuid(input);
                    #endif
                    if (attributeType == typeof(Guid) || attributeType == typeof(EntityReference))
                        return GetAppropiateCastExpressionBasedGuid(input);
                    if (attributeType == typeof(int) || attributeType == typeof(Nullable<int>) || attributeType.IsOptionSet() )
                        return GetAppropiateCastExpressionBasedOnInt(input);
                    if (attributeType == typeof(decimal) || attributeType == typeof(Money))
                        return GetAppropiateCastExpressionBasedOnDecimal(input);
                    if (attributeType == typeof(bool) || attributeType == typeof(BooleanManagedProperty))
                        return GetAppropiateCastExpressionBasedOnBoolean(input);
                    if (attributeType == typeof(string))
                        return GetAppropiateCastExpressionBasedOnStringAndType(input, value, attributeType);
                    if(attributeType.IsDateTime())
                        return GetAppropiateCastExpressionBasedOnDateTime(input, value);

                    return GetAppropiateCastExpressionDefault(input, value); //any other type
                }
            
            return GetAppropiateCastExpressionBasedOnValueInherentType(input, value); //Dynamic entities
        }
        protected static Expression GetAppropiateCastExpressionBasedOnString(Expression input, object value)
        {
            var defaultStringExpression = GetCaseInsensitiveExpression(GetAppropiateCastExpressionDefault(input, value));

            DateTime dtDateTimeConversion;
            if (DateTime.TryParse(value.ToString(), out dtDateTimeConversion))
            {
                return Expression.Convert(input, typeof(DateTime));
            }

            int iValue;
            if(int.TryParse(value.ToString(), out iValue))
            {
                return Expression.Condition(Expression.TypeIs(input, typeof(OptionSetValue)),
                    GetToStringExpression<Int32>(GetAppropiateCastExpressionBasedOnInt(input)),
                    defaultStringExpression
                );
            }

            return defaultStringExpression; 
        }

        protected static Expression GetAppropiateCastExpressionBasedOnStringAndType(Expression input, object value, Type attributeType)
        {
            var defaultStringExpression = GetCaseInsensitiveExpression(GetAppropiateCastExpressionDefault(input, value));

            int iValue;
            if (attributeType.IsOptionSet() && int.TryParse(value.ToString(), out iValue))
            {
                return Expression.Condition(Expression.TypeIs(input, typeof(OptionSetValue)),
                    GetToStringExpression<Int32>(GetAppropiateCastExpressionBasedOnInt(input)),
                    defaultStringExpression
                );
            }

            return defaultStringExpression;
        }

        protected static Expression GetAppropiateCastExpressionBasedOnDateTime(Expression input, object value)
        {
            //Convert to DateTime if string
            DateTime dtDateTimeConversion;
            if (DateTime.TryParse(value.ToString(), out dtDateTimeConversion))
            {
                return Expression.Convert(input, typeof(DateTime));
            }

            return input; //return directly
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
        protected static Expression TranslateConditionExpressionEqual(XrmFakedContext context, TypedConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            object unaryOperatorValue = null;

            switch(c.CondExpression.Operator)
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
                case ConditionOperator.EqualUserId:
                case ConditionOperator.NotEqualUserId:
                    unaryOperatorValue = context.CallerId.Id;
                    break;
            }
            if (unaryOperatorValue != null)
            {
                //c.Values empty in this case
                expOrValues = Expression.Equal(
                                GetAppropiateCastExpressionBasedOnType(c.AttributeType, getAttributeValueExpr, unaryOperatorValue),
                                GetAppropiateTypedValueAndType(unaryOperatorValue, c.AttributeType));
            }
            else
            {
                foreach (object value in c.CondExpression.Values)
                {

                    expOrValues = Expression.Or(expOrValues, Expression.Equal(
                                GetAppropiateCastExpressionBasedOnType(c.AttributeType, getAttributeValueExpr, value),
                                GetAppropiateTypedValueAndType(value, c.AttributeType)));


                }
            }
            
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionIn(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {

                expOrValues = Expression.Or(expOrValues, Expression.Equal(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value),
                            GetAppropiateTypedValueAndType(value, tc.AttributeType)));


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

        protected static Expression TranslateConditionExpressionGreaterThanOrEqual(XrmFakedContext context, TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            return Expression.Or(
                                TranslateConditionExpressionEqual(context, tc, getAttributeValueExpr, containsAttributeExpr),
                                TranslateConditionExpressionGreaterThan(tc, getAttributeValueExpr, containsAttributeExpr));

        }
        protected static Expression TranslateConditionExpressionGreaterThan(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues,
                        Expression.GreaterThan(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value),
                            GetAppropiateTypedValueAndType(value, tc.AttributeType)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionLessThanOrEqual(XrmFakedContext context, TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            return Expression.Or(
                                TranslateConditionExpressionEqual(context, tc, getAttributeValueExpr, containsAttributeExpr),
                                TranslateConditionExpressionLessThan(tc, getAttributeValueExpr, containsAttributeExpr));

        }
        protected static Expression TranslateConditionExpressionLessThan(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues,
                        Expression.LessThan(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value),
                            GetAppropiateTypedValueAndType(value, tc.AttributeType)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionBetween(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            object value1, value2;
            value1 = c.Values[0];
            value2 = c.Values[1];

            //Between the range... 
            var exp = Expression.And(
                Expression.GreaterThanOrEqual(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value1),
                            GetAppropiateTypedValueAndType(value1, tc.AttributeType)),

                Expression.LessThanOrEqual(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value2),
                            GetAppropiateTypedValueAndType(value2, tc.AttributeType)));


            //and... attribute exists too
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                exp));
        }

        protected static Expression TranslateConditionExpressionNull(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            return Expression.Or(Expression.AndAlso(
                                    containsAttributeExpr,
                                    Expression.Equal(
                                    getAttributeValueExpr,
                                    Expression.Constant(null))),   //Attribute is null
                                 Expression.AndAlso(
                                    Expression.Not(containsAttributeExpr),
                                    Expression.Constant(true)));   //Or attribute is not defined (null)
        }

        protected static Expression TranslateConditionExpressionEndsWith(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString()).ToList() );
            var typedComputedCondition = new TypedConditionExpression(computedCondition);
            typedComputedCondition.AttributeType = tc.AttributeType;

            return TranslateConditionExpressionLike(typedComputedCondition, getAttributeValueExpr, containsAttributeExpr);
        }

        protected static Expression GetToStringExpression<T>(Expression e)
        {
            return Expression.Call(e, typeof(T).GetMethod("ToString", new Type[] { }));
        }
        protected static Expression GetCaseInsensitiveExpression(Expression e)
        {
            return Expression.Call(e,
                                typeof(string).GetMethod("ToLowerInvariant", new Type[] { }));
        }

        protected static Expression TranslateConditionExpressionLike(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

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

        protected static Expression TranslateConditionExpressionContains(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString() + "%").ToList());
            var computedTypedCondition = new TypedConditionExpression(computedCondition);
            computedTypedCondition.AttributeType = tc.AttributeType;

            return TranslateConditionExpressionLike(computedTypedCondition, getAttributeValueExpr, containsAttributeExpr);
        
        }

        protected static BinaryExpression TranslateMultipleConditionExpressions(QueryExpression qe, XrmFakedContext context, string sEntityName, List<ConditionExpression> conditions, LogicalOperator op, ParameterExpression entity)
        {
            BinaryExpression binaryExpression = null;  //Default initialisation depending on logical operator
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var c in conditions)
            {
                //Create a new typed expression 
                var typedExpression = new TypedConditionExpression(c);

                string sAttributeName = c.AttributeName;

                //Find the attribute type if using early bound entities
                if(context.ProxyTypesAssembly != null)
                {

#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365
                    if(c.EntityName != null) 
                        sEntityName = qe.GetEntityNameFromAlias(c.EntityName);
                    else
                    {
                        if (c.AttributeName.IndexOf(".") >= 0)
                        {
                            var alias = c.AttributeName.Split('.')[0];
                            sEntityName = qe.GetEntityNameFromAlias(alias);
                            sAttributeName = c.AttributeName.Split('.')[1];
                        }
                    }

#else
                    //CRM 2011
                    if(c.AttributeName.IndexOf(".") >= 0) {
                        var alias = c.AttributeName.Split('.')[0];
                        sEntityName = qe.GetEntityNameFromAlias(alias);
                        sAttributeName = c.AttributeName.Split('.')[1];
                    }
#endif

                    var earlyBoundType = context.FindReflectedType(sEntityName);
                    if (earlyBoundType != null)
                    {
                        typedExpression.AttributeType = context.FindReflectedAttributeType(earlyBoundType, sAttributeName);
                    }
                }
                

                //Build a binary expression  
                if (op == LogicalOperator.And)
                {
                    binaryExpression = Expression.And(binaryExpression, TranslateConditionExpression(context, typedExpression, entity));
                }
                else
                    binaryExpression = Expression.Or(binaryExpression, TranslateConditionExpression(context, typedExpression, entity));
            }

            return binaryExpression;
        }

        protected static BinaryExpression TranslateMultipleFilterExpressions(QueryExpression qe, XrmFakedContext context, string sEntityName, List<FilterExpression> filters, LogicalOperator op, ParameterExpression entity)
        {
            BinaryExpression binaryExpression = null;
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var f in filters)
            {
                var thisFilterLambda = TranslateFilterExpressionToExpression(qe, context, sEntityName, f, entity);

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

        protected static List<Expression> TranslateLinkedEntityFilterExpressionToExpression(QueryExpression qe, XrmFakedContext context, LinkEntity le, ParameterExpression entity)
        {
            //In CRM 2011, condition expressions are at the LinkEntity level without an entity name
            //From CRM 2013, condition expressions were moved to outside the LinkEntity object at the QueryExpression level,
            //with an EntityName alias attribute

            //If we reach this point, it means we are translating filters at the Link Entity level (2011),
            //Therefore we need to prepend the alias attribute because the code to generate attributes for Joins (JoinAttribute extension) is common across versions
            var linkedEntitiesQueryExpressions = new List<Expression>();

            if (le.LinkCriteria != null)
            {
                foreach (var ce in le.LinkCriteria.Conditions)
                {
                    var entityAlias = !string.IsNullOrEmpty(le.EntityAlias) ? le.EntityAlias : le.LinkToEntityName;
                    ce.AttributeName = entityAlias + "." + ce.AttributeName;
                }
            }

            //Translate this specific Link Criteria
            linkedEntitiesQueryExpressions.Add(TranslateFilterExpressionToExpression(qe, context, le.LinkToEntityName, le.LinkCriteria, entity));

            //Processed nested linked entities
            foreach (var nestedLinkedEntity in le.LinkEntities)
            {
                var listOfExpressions = TranslateLinkedEntityFilterExpressionToExpression(qe, context, nestedLinkedEntity, entity);
                linkedEntitiesQueryExpressions.AddRange(listOfExpressions);
            }

            return linkedEntitiesQueryExpressions;
        }

        protected static Expression TranslateQueryExpressionFiltersToExpression(XrmFakedContext context, QueryExpression qe, ParameterExpression entity)
        {
            var linkedEntitiesQueryExpressions = new List<Expression>();
            foreach(var le in qe.LinkEntities)
            {
                var listOfExpressions = TranslateLinkedEntityFilterExpressionToExpression(qe, context, le, entity);
                linkedEntitiesQueryExpressions.AddRange(listOfExpressions);
            }

            if(linkedEntitiesQueryExpressions.Count > 0 && qe.Criteria != null)
            {
                //Return the and of the two
                Expression andExpression = Expression.Constant(true);
                foreach(var e in linkedEntitiesQueryExpressions)
                {
                    andExpression = Expression.And(e, andExpression);

                }
                var feExpression = TranslateFilterExpressionToExpression(qe, context, qe.EntityName, qe.Criteria, entity);
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
                return TranslateFilterExpressionToExpression(qe, context, qe.EntityName, qe.Criteria, entity);
            }
        }
        protected static Expression TranslateFilterExpressionToExpression(QueryExpression qe, XrmFakedContext context, string sEntityName, FilterExpression fe, ParameterExpression entity)
        {
            if (fe == null) return Expression.Constant(true);

            BinaryExpression conditionsLambda = null;
            BinaryExpression filtersLambda = null;
            if (fe.Conditions != null && fe.Conditions.Count > 0)
            {
                conditionsLambda = TranslateMultipleConditionExpressions(qe, context, sEntityName, fe.Conditions.ToList(), fe.FilterOperator, entity);
            }

            //Process nested filters recursively
            if (fe.Filters != null && fe.Filters.Count > 0)
            {
                filtersLambda = TranslateMultipleFilterExpressions(qe, context, sEntityName, fe.Filters.ToList(), fe.FilterOperator, entity);
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