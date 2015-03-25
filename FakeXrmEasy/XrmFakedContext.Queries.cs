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

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmFakedContext
    {
        protected internal Type FindReflectedType(string sLogicalName)
        {
            Assembly assembly = this.ProxyTypesAssembly;
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


        public IQueryable<Entity> CreateQuery(string entityLogicalName)
        {
            return this.CreateQuery<Entity>(entityLogicalName);
        }

        public IQueryable<T> CreateQuery<T>() where T : Entity
        {
            Type typeParameter = typeof(T);

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

        public static IQueryable<Entity> TranslateLinkedEntityToLinq(XrmFakedContext context, LinkEntity le, IQueryable<Entity> query, ColumnSet previousColumnSet) {
            
            var leAlias = string.IsNullOrWhiteSpace(le.EntityAlias) ? le.LinkToEntityName : le.EntityAlias;
            context.EnsureEntityNameExistsInMetadata(le.LinkFromEntityName);
            context.EnsureEntityNameExistsInMetadata(le.LinkToEntityName);

            var inner = context.CreateQuery<Entity>(le.LinkToEntityName);

            switch (le.JoinOperator)
            {
                case JoinOperator.Inner:
                    query = query.Join(inner,
                                    outerKey => outerKey.KeySelector(le.LinkFromAttributeName),
                                    innerKey => innerKey.KeySelector(le.LinkToAttributeName),
                                    (outerEl, innerEl) => outerEl
                                                            .ProjectAttributes(previousColumnSet, context)
                                                            .JoinAttributes(innerEl, le.Columns, leAlias));

                    break;
                case JoinOperator.LeftOuter:
                    query = query.GroupJoin(inner,
                                    outerKey => outerKey.KeySelector(le.LinkFromAttributeName),
                                    innerKey => innerKey.KeySelector(le.LinkToAttributeName),
                                    (outerEl, innerElemsCol) => new { outerEl, innerElemsCol })
                                                .SelectMany(x => x.innerElemsCol.DefaultIfEmpty()
                                                            , (x, y) => x.outerEl
                                                                            .ProjectAttributes(previousColumnSet, context)
                                                                            .JoinAttributes(y, le.Columns, leAlias));


                    break;

            }

            //Process nested linked entities recursively
            foreach (LinkEntity nestedLinkedEntity in le.LinkEntities)
            {
                query = TranslateLinkedEntityToLinq(context, nestedLinkedEntity, query, le.Columns);
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
            var expTreeBody = TranslateFilterExpressionToExpression(qe.Criteria, entity);
            Expression<Func<Entity, bool>> lambda = Expression.Lambda<Func<Entity, bool>>(expTreeBody, entity);
            query = query.Where(lambda);

            //Project the attributes in the root column set  (must be applied after the where clause, not before!!)
            if (qe.ColumnSet != null && !qe.ColumnSet.AllColumns)
                query = query.Select(x => x.ProjectAttributes(qe.ColumnSet, context));

            //Sort results
            if (qe.Orders != null)
            {
                foreach (var order in qe.Orders)
                {
                    if (order.OrderType == OrderType.Ascending)
                        query = query.OrderBy(e => e.Attributes[order.AttributeName]);
                    else
                        query = query.OrderByDescending(e => e.Attributes[order.AttributeName]);
                }
            }
            return query;
        }

        //protected static Expression<Func<object, object, bool>> AreValuesEqual = (object o1, object o2) =>
        //{
        //    if (o1 is EntityReference && o2 is Guid)
        //    {
        //        return (o1 as EntityReference).Id.Equals((Guid) o2);
        //    }
        //    return o1 == o2;
        //};

        protected static Expression TranslateConditionExpression(ConditionExpression c, ParameterExpression entity)
        {
            Expression attributesProperty = Expression.Property(
                entity,
                "Attributes"
                );

            Expression containsAttributeExpression = Expression.Call(
                attributesProperty,
                typeof(AttributeCollection).GetMethod("ContainsKey", new Type[] { typeof(string) }),
                Expression.Constant(c.AttributeName)
                );

            Expression getAttributeValueExpr = Expression.Property(
                attributesProperty, "Item",
                Expression.Constant(c.AttributeName, typeof(string))
                );


            Expression getNonBasicValueExpr = getAttributeValueExpr;

            switch (c.Operator)
            {
                case ConditionOperator.Equal:
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
            }
            return Expression.Constant(value);
        }

        protected static Expression GetAppropiateCastExpressionBasedOnValue(Expression input, object value)
        {
            if (value is Guid)
                return GetAppropiateCastExpressionBasedGuid(input); //Could be compared against an EntityReference
            if (value is int)
                return GetAppropiateCastExpressionBasedOnInt(input); //Could be compared against an OptionSet
            if (value is decimal)
                return GetAppropiateCastExpressionBasedOnDecimal(input); //Could be compared against a Money
            if (value is bool)
                return GetAppropiateCastExpressionBasedOnBoolean(input); //Could be a BooleanManagedProperty

            //Other basic types conversions
            //Special case => datetime is sent as a string
            if (value is string)
            {
                DateTime dtDateTimeConversion;
                if (DateTime.TryParse(value.ToString(), out dtDateTimeConversion))
                {
                    return Expression.Convert(input, typeof(DateTime));
                }
            }
            return Expression.Convert(input, value.GetType());  //Default type conversion
        }

        protected static Expression GetAppropiateCastExpressionBasedGuid(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(EntityReference)),
                                Expression.Convert(
                                    Expression.Call(Expression.TypeAs(input, typeof(EntityReference)),
                                            typeof(EntityReference).GetMethod("get_Id")),
                                            typeof(Guid)),
                           Expression.Condition(Expression.TypeIs(input, typeof(Guid)),
                                        Expression.Convert(input, typeof(Guid)),
                                        Expression.Constant(Guid.Empty)));

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
            foreach (object value in c.Values)
            {

                expOrValues = Expression.Or(expOrValues, Expression.Equal(
                            GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr,value),
                            GetAppropiateTypedValue(value)));
                

            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

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
        protected static Expression TranslateConditionExpressionLike(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            Expression convertedValueToStr = Expression.Convert(getAttributeValueExpr, typeof(string));

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
                    convertedValueToStr,
                    typeof(string).GetMethod(sMethod, new Type[] { typeof(string) }),
                    Expression.Constant(value.ToString().Replace("%", "")) //Linq2CRM adds the percentage value to be executed as a LIKE operator, here we are replacing it to just use the appropiate method
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