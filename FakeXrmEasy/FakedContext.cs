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
    /// <summary>
    /// A fake context that stores In-Memory entites indexed by logical name and then Entity records, simulating
    /// how entities are persisted in Tables (with the logical name) and then the records themselves
    /// where the Primary Key is the Guid
    /// </summary>
    public class XrmFakedContext: IFakedContext
    {
        public Dictionary<string, Dictionary<Guid, Entity>> Data { get; set; }
        public XrmFakedContext()
        {
            Data = new Dictionary<string, Dictionary<Guid, Entity>>();
        }

        public void Build(XrmFakedContext data)
        {
            this.Data = data.Data;
        }

        /// <summary>
        /// Initializes the context with the provided entities
        /// </summary>
        /// <param name="col"></param>
        public void Initialize(IEnumerable<Entity> entities)
        {
            if (entities == null)
            {
                throw new InvalidOperationException("The entities parameter must be not null");
            }

            foreach (var e in entities)
            {
                AddEntity(e);
            }
        }

        public void EnsureEntityNameExistsInMetadata(string sEntityName)
        {
            if (!Data.ContainsKey(sEntityName))
            {
                throw new Exception(string.Format("Entity {0} does not exist in the metadata cache", sEntityName));
            };
        }
        protected void ValidateEntity(Entity e)
        {
            //Validate the entity
            if (string.IsNullOrWhiteSpace(e.LogicalName))
            {
                throw new InvalidOperationException("An entity must not have a null or empty LogicalName property.");
            }

            if (e.Id == Guid.Empty)
            {
                throw new InvalidOperationException("An entity with an empty Id can't be added");
            }
        }

        
        protected internal void AddEntity(Entity e)
        {
            ValidateEntity(e);

            //Add the entity collection
            if (!Data.ContainsKey(e.LogicalName))
            {
                Data.Add(e.LogicalName, new Dictionary<Guid, Entity>());
            }

            if (Data[e.LogicalName].ContainsKey(e.Id))
            {
                Data[e.LogicalName][e.Id] = e;
            }
            else
            {
                Data[e.LogicalName].Add(e.Id, e);
            }
        }

        /// <summary>
        /// Returns a faked organization service that works against this context
        /// </summary>
        /// <returns></returns>
        public IOrganizationService GetFakedOrganizationService()
        {
            return GetFakedOrganizationService(this);
        }

        /// <summary>
        /// Defines a faked organization service that intercepts CRUD operations to make them work against
        /// the faked context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IOrganizationService GetFakedOrganizationService(XrmFakedContext context)
        {
            if (context == null)
            {
                throw new InvalidOperationException("The faked context must not be null.");
            }

            var fakedService = A.Fake<IOrganizationService>();

            //Fake CRUD methods
            FakeRetrieve(context, fakedService);
            FakeCreate(context, fakedService);
            FakeUpdate(context, fakedService);
            FakeDelete(context, fakedService);
            FakeExecute(context, fakedService);

            return fakedService;
        }

        /// <summary>
        /// A fake retrieve method that will query the FakedContext to retrieve the specified
        /// entity and Guid, or null, if the entity was not found
        /// </summary>
        /// <param name="context">The faked context</param>
        /// <param name="fakedService">The faked service where the Retrieve method will be faked</param>
        /// <returns></returns>
        public static void FakeRetrieve(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Retrieve(A<string>._, A<Guid>._, A<ColumnSet>._))
                .ReturnsLazily((string entityName, Guid id, ColumnSet columnSet) =>
                {
                    if (string.IsNullOrWhiteSpace(entityName))
                    {
                        throw new InvalidOperationException("The entity logical name must not be null or empty.");
                    }

                    if (id == Guid.Empty)
                    {
                        throw new InvalidOperationException("The id must not be empty.");
                    }

                    if (columnSet == null)
                    {
                        throw new InvalidOperationException("The columnset parameter must not be null.");
                    }

                    if (!context.Data.ContainsKey(entityName))
                        throw new InvalidOperationException(string.Format("The entity logical name {0} is not valid.", entityName));

                    //Entity logical name exists, so , check if the requested entity exists
                    if(context.Data[entityName] != null
                        && context.Data[entityName].ContainsKey(id))
                    {
                        //Entity found => return only the subset of columns specified or all of them
                        if(columnSet.AllColumns)
                            return context.Data[entityName][id];
                        else
                        {
                            //Return the subset of columns requested only
                            var newEntity = new Entity(entityName);
                            newEntity.Id = id;

                            //Add attributes
                            var foundEntity = context.Data[entityName][id];
                            foreach (var column in columnSet.Columns)
                            {
                                newEntity[column] = foundEntity[column];
                            }
                            return newEntity;
                        }
                    }
                    else
                    {
                        //Entity not found in the context => return null
                        return null;
                    }
                });
        }

        /// <summary>
        /// Fakes the Create message
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        public static void FakeCreate(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Create(A<Entity>._))
                .ReturnsLazily((Entity e) =>
                {
                    if (e == null)
                    {
                        throw new InvalidOperationException("The entity must not be null");
                    }

                    if (e.Id != Guid.Empty)
                    {
                        throw new InvalidOperationException("The Id property must not be initialized");
                    }

                    if (string.IsNullOrWhiteSpace(e.LogicalName))
                    {
                        throw new InvalidOperationException("The LogicalName property must not be empty");
                    }

                    //Add entity to the context
                    e.Id = Guid.NewGuid();
                    context.AddEntity(e);

                    return e.Id;
                });

        }

        public static void FakeUpdate(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Update(A<Entity>._))
                .Invokes((Entity e) =>
                {
                    if (e == null)
                    {
                        throw new InvalidOperationException("The entity must not be null");
                    }

                    if (e.Id == Guid.Empty)
                    {
                        throw new InvalidOperationException("The Id property must not be empty");
                    }

                    if (string.IsNullOrWhiteSpace(e.LogicalName))
                    {
                        throw new InvalidOperationException("The LogicalName property must not be empty");
                    }

                    //The entity record must exist in the context
                    if(context.Data.ContainsKey(e.LogicalName) &&
                        context.Data[e.LogicalName].ContainsKey(e.Id))
                    {
                        //Now the entity is the one passed
                        context.Data[e.LogicalName][e.Id] = e;
                    }
                    else
                    {
                        //The entity record was not found, return a CRM-ish update error message
                        throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("{0} with Id {1} Does Not Exist", e.LogicalName, e.Id) );
                    }
                });

        }

        /// <summary>
        /// Fakes the delete method. Very similar to the Retrieve one
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        public static void FakeDelete(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Delete(A<string>._, A<Guid>._))
                .Invokes((string entityName, Guid id) =>
                {
                    if (string.IsNullOrWhiteSpace(entityName))
                    {
                        throw new InvalidOperationException("The entity logical name must not be null or empty.");
                    }

                    if (id == Guid.Empty)
                    {
                        throw new InvalidOperationException("The id must not be empty.");
                    }

                    if (!context.Data.ContainsKey(entityName))
                        throw new InvalidOperationException(string.Format("The entity logical name {0} is not valid.", entityName));

                    //Entity logical name exists, so , check if the requested entity exists
                    if (context.Data[entityName] != null
                        && context.Data[entityName].ContainsKey(id))
                    {
                        //Entity found => return only the subset of columns specified or all of them
                        context.Data[entityName].Remove(id);
                    }
                    else
                    {
                        //Entity not found in the context => throw not found exception
                        //The entity record was not found, return a CRM-ish update error message
                        throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(),
                            string.Format("{0} with Id {1} Does Not Exist", entityName, id));
                    }
                });
        }

        public IQueryable<T> CreateQuery<T>() where T: Entity
        {
            Type typeParameter = typeof(T);

            if (!Data.ContainsKey((typeParameter.Name.ToLower())))
            {
                throw new Exception(string.Format("The type {0} was not found", typeParameter.Name));
            }

            List<T> lst = new List<T>();
            foreach (var e in Data[typeParameter.Name.ToLower()].Values)
            {
                lst.Add((T) e);
            }

            return lst.AsQueryable<T>();
        }

        public IQueryable<Entity> CreateQueryFromEntityName(string s)
        {
            return Data[s].Values.AsQueryable();
        }

        /// <summary>
        /// Fakes the Execute method of the organization service.
        /// Not all the OrganizationRequest are going to be implemented, so stay tunned on updates!
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        public static void FakeExecute(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Execute(A<OrganizationRequest>._))
                .ReturnsLazily((OrganizationRequest req) =>
                {
                    if(req is RetrieveMultipleRequest) {
                        var request = req as RetrieveMultipleRequest;
                        if (request.Query is QueryExpression)
                        {
                            var linqQuery = TranslateQueryExpressionToLinq(context, request.Query as QueryExpression);
                            var response = new RetrieveMultipleResponse
                            {
                                Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(linqQuery.ToList()) }
                                 }
                            };
                            return response;
                        }
                    }
                    return new OrganizationResponse();
                });
        }

        public static void FakeRetrieveMultiple(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.RetrieveMultiple(A<QueryBase>._))
                .ReturnsLazily((QueryBase req) =>
                {
                    if (req is QueryExpression)
                    {
                        var query = req as QueryExpression;
                        var linqQuery = TranslateQueryExpressionToLinq(context, query as QueryExpression);
                        var response = new RetrieveMultipleResponse();
                        response.EntityCollection.Entities.AddRange(linqQuery.ToList());
                        return response.EntityCollection;
                        
                    }
                    throw new PullRequestException("Unexpected querybase for RetrieveMultiple");
                });
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
                                        (outerEl, innerElemsCol) => new {outerEl, innerElemsCol})
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

        public static Expression TranslateConditionExpression(ConditionExpression c, ParameterExpression entity)
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
                            getAttributeValueExpr,
                            Expression.Constant(value)));
            }
            return Expression.And(
                            containsAttributeExpr,
                            expOrValues);
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

            return Expression.And(
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

            return Expression.And(
                            containsAttributeExpr,
                            expOrValues);
        }
        public static BinaryExpression TranslateMultipleConditionExpressions(List<ConditionExpression> conditions, LogicalOperator op, ParameterExpression entity)
        {
            BinaryExpression binaryExpression = null;  //Default initialisation depending on logical operator
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var c in conditions) {
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

        public static BinaryExpression TranslateMultipleFilterExpressions(List<FilterExpression> filters, LogicalOperator op, ParameterExpression entity)
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

        public static Expression TranslateFilterExpressionToExpression(FilterExpression fe, ParameterExpression entity)
        {
            if (fe == null) return Expression.Constant(true);

            BinaryExpression conditionsLambda = null;
            BinaryExpression filtersLambda = null;
            if(fe.Conditions != null && fe.Conditions.Count > 0) {
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
