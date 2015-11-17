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
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Reflection;
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy
{
    /// <summary>
    /// A fake context that stores In-Memory entites indexed by logical name and then Entity records, simulating
    /// how entities are persisted in Tables (with the logical name) and then the records themselves
    /// where the Primary Key is the Guid
    /// </summary>
    public partial class XrmFakedContext: IXrmFakedContext
    {
        protected Dictionary<string, Dictionary<string, string>> AttributeMetadata { get; set; }

        public Dictionary<string, Dictionary<Guid, Entity>> Data { get; set; }

        public Assembly ProxyTypesAssembly { get; set; }
        

        /// <summary>
        /// Sets the user to assign the CreatedBy and ModifiedBy properties when entities are added to the context.
        /// All requests will be executed on behalf of this user
        /// </summary>
        public EntityReference CallerId { get; set; }


        public XrmFakedContext()
        {
            AttributeMetadata = new Dictionary<string, Dictionary<string, string>>();
            Data = new Dictionary<string, Dictionary<Guid, Entity>>();
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
        protected IOrganizationService GetFakedOrganizationService(XrmFakedContext context)
        {
            //if (context == null) //Impossible to reproduce as this method is protected and called from an instance
            //{
            //    throw new InvalidOperationException("The faked context must not be null.");
            //}

            var fakedService = A.Fake<IOrganizationService>();

            //Fake CRUD methods
            FakeRetrieve(context, fakedService);
            FakeCreate(context, fakedService);
            FakeUpdate(context, fakedService);
            FakeDelete(context, fakedService);
            
            //Fake / Intercept Retrieve Multiple Requests
            FakeRetrieveMultiple(context, fakedService);

            //Fake / Intercept other requests
            FakeExecute(context, fakedService);

            return fakedService;
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
                        else if (request.Query is QueryByAttribute)
                        {
                            //We instantiate a QueryExpression to be executed as we have the implementation done already
                            var query = request.Query as QueryByAttribute;
                            var qe = new QueryExpression(query.EntityName);

                            qe.ColumnSet = query.ColumnSet;
                            qe.Criteria = new FilterExpression();
                            for (var i = 0; i < query.Attributes.Count; i++)
                            {
                                qe.Criteria.AddCondition(new ConditionExpression(query.Attributes[i], ConditionOperator.Equal, query.Values[i]));
                            }

                            //QueryExpression now done... execute it!
                            var linqQuery = TranslateQueryExpressionToLinq(context, qe as QueryExpression);
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
                    else if (req is WhoAmIRequest)
                    {
                        var request = req as WhoAmIRequest;

                        var response = new WhoAmIResponse
                        {
                            Results = new ParameterCollection
                                { { "UserId", context.CallerId.Id } }
                                 
                        };
                        return response;
                    }
                    else if (req is RetrieveAttributeRequest)
                    {
                        return FakeRetrieveAttributeRequest(context, fakedService, req as RetrieveAttributeRequest);
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
                        var response = new RetrieveMultipleResponse
                        {
                            Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(linqQuery.ToList()) }
                                 }
                        };
                        return response.EntityCollection;
                        
                    }
                    else if (req is QueryByAttribute)
                    {
                        //We instantiate a QueryExpression to be executed as we have the implementation done already
                        var query = req as QueryByAttribute;
                        var qe = new QueryExpression(query.EntityName);

                        qe.ColumnSet = query.ColumnSet;
                        qe.Criteria = new FilterExpression();
                        for(var i=0; i < query.Attributes.Count; i++) {
                            qe.Criteria.AddCondition(new ConditionExpression(query.Attributes[i],ConditionOperator.Equal,query.Values[i]));
                        }
                        
                        //QueryExpression now done... execute it!
                        var linqQuery = TranslateQueryExpressionToLinq(context, qe as QueryExpression);
                        var response = new RetrieveMultipleResponse
                        {
                            Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(linqQuery.ToList()) }
                                 }
                        };
                        return response.EntityCollection;
                    }
                    throw new PullRequestException("Unexpected querybase for RetrieveMultiple");
                });
        }

        
    }
}
