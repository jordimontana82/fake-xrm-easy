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

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmFakedContext
    {
        public IQueryable<T> CreateQuery<T>() where T : Entity
        {
            Type typeParameter = typeof(T);

            if (!Data.ContainsKey((typeParameter.Name.ToLower())))
            {
                throw new Exception(string.Format("The type {0} was not found", typeParameter.Name));
            }

            List<T> lst = new List<T>();
            foreach (var e in Data[typeParameter.Name.ToLower()].Values)
            {
                lst.Add((T)e);
            }

            return lst.AsQueryable<T>();
        }

        public IQueryable<Entity> CreateQueryFromEntityName(string s)
        {
            return Data[s].Values.AsQueryable();
        }

        public static IQueryable<Entity> TranslateQueryExpressionToLinq(XrmFakedContext context, QueryExpression qe)
        {
            if (qe == null) return null;

            //Start form the root entity and build a LINQ query to execute the query against the In-Memory context:
            context.EnsureEntityNameExistsInMetadata(qe.EntityName);
            var query = context.Data[qe.EntityName].Values.AsQueryable();

            //Add as many Joins as linked entities
            foreach (LinkEntity le in qe.LinkEntities)
            {
                var leAlias = string.IsNullOrWhiteSpace(le.EntityAlias) ? le.LinkToEntityName : le.EntityAlias;
                context.EnsureEntityNameExistsInMetadata(le.LinkFromEntityName);
                context.EnsureEntityNameExistsInMetadata(le.LinkToEntityName);
                var inner = context.Data[le.LinkToEntityName].Values.AsQueryable();

                switch (le.JoinOperator)
                {
                    case JoinOperator.Inner:
                        query = query.Join(inner,
                                        outerKey => outerKey.KeySelector(le.LinkFromAttributeName),
                                        innerKey => innerKey.KeySelector(le.LinkToAttributeName),
                                        (outerEl, innerEl) => outerEl
                                                                .ProjectAttributes(qe.ColumnSet)
                                                                .JoinAttributes(innerEl, le.Columns, leAlias));

                        break;
                    case JoinOperator.LeftOuter:
                        query = query.GroupJoin(inner,
                                        outerKey => outerKey.KeySelector(le.LinkFromAttributeName),
                                        innerKey => innerKey.KeySelector(le.LinkToAttributeName),
                                        (outerEl, innerElemsCol) => new { outerEl, innerElemsCol })
                                                    .SelectMany(x => x.innerElemsCol.DefaultIfEmpty()
                                                                , (x, y) => x.outerEl
                                                                                .ProjectAttributes(qe.ColumnSet)
                                                                                .JoinAttributes(y, le.Columns, leAlias));


                        break;

                }
            }

            // Compose the expression tree that represents the parameter to the predicate.
            ParameterExpression entity = Expression.Parameter(typeof(Entity));
            var expTreeBody = TranslateFilterExpressionToExpression(qe.Criteria, entity);
            Expression<Func<Entity, bool>> lambda = Expression.Lambda<Func<Entity, bool>>(expTreeBody, entity);
            query = query.Where(lambda);
            return query;
        }

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

            switch (c.Operator)
            {
                case ConditionOperator.Equal:
                    return TranslateConditionExpressionEqual(c, getAttributeValueExpr, containsAttributeExpression);

                case ConditionOperator.BeginsWith:
                case ConditionOperator.Like:
                case ConditionOperator.Contains:
                case ConditionOperator.EndsWith:
                    return TranslateConditionExpressionLike(c, getAttributeValueExpr, containsAttributeExpression);

                case ConditionOperator.NotEqual:
                    return Expression.Not(TranslateConditionExpressionEqual(c, getAttributeValueExpr, containsAttributeExpression));

                case ConditionOperator.DoesNotBeginWith:
                case ConditionOperator.DoesNotEndWith:
                case ConditionOperator.NotLike:
                case ConditionOperator.DoesNotContain:
                    return Expression.Not(TranslateConditionExpressionLike(c, getAttributeValueExpr, containsAttributeExpression));

                case ConditionOperator.Null:
                    return TranslateConditionExpressionNull(c, getAttributeValueExpr, containsAttributeExpression);

                case ConditionOperator.NotNull:
                    return Expression.Not(TranslateConditionExpressionNull(c, getAttributeValueExpr, containsAttributeExpression));

                case ConditionOperator.GreaterThan:
                    return TranslateConditionExpressionGreaterThan(c, getAttributeValueExpr, containsAttributeExpression);

                case ConditionOperator.GreaterEqual:
                    return Expression.Or(
                                TranslateConditionExpressionEqual(c, getAttributeValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionGreaterThan(c, getAttributeValueExpr, containsAttributeExpression));

                case ConditionOperator.LessThan:
                    return TranslateConditionExpressionLessThan(c, getAttributeValueExpr, containsAttributeExpression);

                case ConditionOperator.LessEqual:
                    return Expression.Or(
                                TranslateConditionExpressionEqual(c, getAttributeValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionLessThan(c, getAttributeValueExpr, containsAttributeExpression));

                default:
                    throw new PullRequestException(string.Format("Operator {0} not yet implemented for condition expression", c.Operator.ToString()));


            }
        }

        protected static Expression TranslateConditionExpressionEqual(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues, Expression.Equal(
                            Expression.Convert(getAttributeValueExpr, value.GetType()),
                            Expression.Constant(value)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            expOrValues);
        }

        protected static Expression TranslateConditionExpressionGreaterThan(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues,
                        Expression.GreaterThan(
                            Expression.Convert(getAttributeValueExpr, value.GetType()),
                            Expression.Constant(value)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            expOrValues);
        }

        protected static Expression TranslateConditionExpressionLessThan(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues,
                        Expression.LessThan(
                            Expression.Convert(getAttributeValueExpr, value.GetType()),
                            Expression.Constant(value)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            expOrValues);
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
            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            Expression convertedValueToStr = Expression.Convert(getAttributeValueExpr, typeof(string));

            foreach (object value in c.Values)
            {
                expOrValues = Expression.Or(expOrValues, Expression.Call(
                    convertedValueToStr,
                    typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                    Expression.Constant(value.ToString())
                ));
            }

            return Expression.AndAlso(
                            containsAttributeExpr,
                            expOrValues);
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
                    return Expression.Add(conditionsLambda, filtersLambda);
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