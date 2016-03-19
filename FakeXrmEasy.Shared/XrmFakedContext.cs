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
using FakeXrmEasy.FakeMessageExecutors;

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

        public delegate OrganizationResponse ServiceRequestExecution(OrganizationRequest req);

        /// <summary>
        /// Probably should be replaced by FakeMessageExecutors, more generic, which can use custom interfaces rather than a single method / delegate
        /// </summary>
        private Dictionary<Type, ServiceRequestExecution> ExecutionMocks { get; set; }

        private Dictionary<Type, IFakeMessageExecutor> FakeMessageExecutors { get; set; }

        private Dictionary<string, XrmFakedRelationship> Relationships { get; set; }

        public XrmFakedContext()
        {
            AttributeMetadata = new Dictionary<string, Dictionary<string, string>>();
            Data = new Dictionary<string, Dictionary<Guid, Entity>>();
            ExecutionMocks = new Dictionary<Type, ServiceRequestExecution>();
            FakeMessageExecutors = new Dictionary<Type, IFakeMessageExecutor>();

            //Adding default execution fakes
            AddFakeMessageExecutor<WhoAmIRequest>(new WhoAmIRequestExecutor());
            AddFakeMessageExecutor<RetrieveMultipleRequest>(new RetrieveMultipleRequestExecutor());
            AddFakeMessageExecutor<RetrieveAttributeRequest>(new RetrieveAttributeRequestExecutor());
            AddFakeMessageExecutor<SetStateRequest>(new SetStateRequestExecutor());
            AddFakeMessageExecutor<AssociateRequest>(new AssociateRequestExecutor());
            AddFakeMessageExecutor<DisassociateRequest>(new DisassociateRequestExecutor());
            AddFakeMessageExecutor<CreateRequest>(new CreateRequestExecutor());
            AddFakeMessageExecutor<UpdateRequest>(new UpdateRequestExecutor());
            AddFakeMessageExecutor<DeleteRequest>(new DeleteRequestExecutor());
            AddFakeMessageExecutor<AssignRequest>(new AssignRequestExecutor());
#if FAKE_XRM_EASY_2016
            AddFakeMessageExecutor<ExecuteTransactionRequest>(new ExecuteTransactionExecutor());
#endif

            Relationships = new Dictionary<string, XrmFakedRelationship>();
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

        public void AddExecutionMock<T>(ServiceRequestExecution mock) where T : OrganizationRequest
        {
            if (!ExecutionMocks.ContainsKey(typeof(T)))
                ExecutionMocks.Add(typeof(T), mock);
            else
                ExecutionMocks[typeof(T)] = mock;
        }

        public void RemoveExecutionMock<T>() where T : OrganizationRequest
        {
            ExecutionMocks.Remove(typeof (T));
        }

        public void AddFakeMessageExecutor<T>(IFakeMessageExecutor executor) where T : OrganizationRequest
        {
            if (!FakeMessageExecutors.ContainsKey(typeof(T)))
                FakeMessageExecutors.Add(typeof(T), executor);
            else
                FakeMessageExecutors[typeof(T)] = executor;
        }

        public void RemoveFakeMessageExecutor<T>() where T : OrganizationRequest
        {
            FakeMessageExecutors.Remove(typeof(T));
        }

        public void AddRelationship(string schemaname, XrmFakedRelationship relationship)
        {
            Relationships.Add(schemaname, relationship);
        }

        public void RemoveRelationship(string schemaname)
        {
            Relationships.Remove(schemaname);
        }

        public XrmFakedRelationship GetRelationship(string schemaName)
        {
            if (Relationships.ContainsKey(schemaName))
            {
                return Relationships[schemaName];
            }

            return null;
        }

        /// <summary>
        /// Returns a faked organization service that works against this context
        /// </summary>
        /// <returns></returns>
        public IOrganizationService GetFakedOrganizationService()
        {
            return GetFakedOrganizationService(this);
        }

        public OrganizationServiceProxy GetFakedOrganizationServiceProxy()
        {
            return GetFakedOrganizationServiceProxy(this);
        }

        public OrganizationServiceProxy GetFakedOrganizationServiceProxy(XrmFakedContext context)
        {
            var fakedServiceProxy = A.Fake<OrganizationServiceProxy>();

            //Fake CRUD methods
            FakeRetrieve(context, fakedServiceProxy);
            FakeCreate(context, fakedServiceProxy);
            FakeUpdate(context, fakedServiceProxy);
            FakeDelete(context, fakedServiceProxy);

            //Fake / Intercept Retrieve Multiple Requests
            FakeRetrieveMultiple(context, fakedServiceProxy);

            //Fake / Intercept other requests
            FakeExecute(context, fakedServiceProxy);
            FakeAssociate(context, fakedServiceProxy);
            FakeDisassociate(context, fakedServiceProxy);

            return fakedServiceProxy;
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
            FakeAssociate(context, fakedService);
            FakeDisassociate(context, fakedService);

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

                    if (context.ExecutionMocks.ContainsKey(req.GetType()))
                    {
                        return context.ExecutionMocks[req.GetType()].Invoke(req);
                    }
                    if (context.FakeMessageExecutors.ContainsKey(req.GetType()))
                    {
                        return context.FakeMessageExecutors[req.GetType()].Execute(req, context);
                    }

                    throw PullRequestException.NotImplementedOrganizationRequest(req.GetType());
                });
        }

        public static void FakeAssociate(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Associate(A<string>._, A<Guid>._, A<Relationship>._, A<EntityReferenceCollection>._))
                .Invokes((string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection entityCollection) =>
                {

                    if (context.FakeMessageExecutors.ContainsKey(typeof(AssociateRequest)))
                    {
                        var request = new AssociateRequest()
                        {
                            Target = new EntityReference() { Id = entityId, LogicalName = entityName },
                            Relationship = relationship,
                            RelatedEntities = entityCollection
                        };
                        context.FakeMessageExecutors[typeof(AssociateRequest)].Execute(request, context);
                    }
                    else 
                        throw PullRequestException.NotImplementedOrganizationRequest(typeof(AssociateRequest));
                });
        }

        public static void FakeDisassociate(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Disassociate(A<string>._, A<Guid>._, A<Relationship>._, A<EntityReferenceCollection>._))
                .Invokes((string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection entityCollection) =>
                {

                    if (context.FakeMessageExecutors.ContainsKey(typeof(DisassociateRequest)))
                    {
                        var request = new DisassociateRequest()
                        {
                            Target = new EntityReference() { Id = entityId, LogicalName = entityName },
                            Relationship = relationship,
                            RelatedEntities = entityCollection
                        };
                        context.FakeMessageExecutors[typeof(DisassociateRequest)].Execute(request, context);
                    }
                    else
                        throw PullRequestException.NotImplementedOrganizationRequest(typeof(DisassociateRequest));
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
