﻿using FakeItEasy;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.Permissions;
using FakeXrmEasy.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeXrmEasy
{
    /// <summary>
    /// A fake context that stores In-Memory entites indexed by logical name and then Entity records, simulating
    /// how entities are persisted in Tables (with the logical name) and then the records themselves
    /// where the Primary Key is the Guid
    /// </summary>
    public partial class XrmFakedContext : IXrmContext
    {
        protected internal IOrganizationService Service { get; set; }

        private IServiceEndpointNotificationService _serviceEndpointNotificationService;

        private readonly Lazy<XrmFakedTracingService> _tracingService = new Lazy<XrmFakedTracingService>(() => new XrmFakedTracingService());

        /// <summary>
        /// All proxy type assemblies available on mocked database.
        /// </summary>
        private List<Assembly> ProxyTypesAssemblies { get; set; }

        protected internal XrmFakedTracingService TracingService => _tracingService.Value;

        protected internal bool Initialised { get; set; }

        public Dictionary<string, Dictionary<Guid, Entity>> Data { get; set; }

        /// <summary>
        /// Specify which assembly is used to search for early-bound proxy
        /// types when used within simulated CRM context.
        ///
        /// If you want to specify multiple different assemblies for early-bound
        /// proxy types please use <see cref="EnableProxyTypes(Assembly)"/>
        /// instead.
        /// </summary>
        public Assembly ProxyTypesAssembly
        {
            get
            {
                // TODO What we should do when ProxyTypesAssemblies contains multiple assemblies? One shouldn't throw exceptions from properties.
                return ProxyTypesAssemblies.FirstOrDefault();
            }
            set
            {
                ProxyTypesAssemblies = new List<Assembly>();
                if (value != null)
                {
                    ProxyTypesAssemblies.Add(value);
                }
            }
        }

        /// <summary>
        /// Sets the user to assign the CreatedBy and ModifiedBy properties when entities are added to the context.
        /// All requests will be executed on behalf of this user
        /// </summary>
        public EntityReference CallerId { get; set; }

        public EntityReference BusinessUnitId { get; set; }

        public delegate OrganizationResponse ServiceRequestExecution(OrganizationRequest req);

        /// <summary>
        /// Probably should be replaced by FakeMessageExecutors, more generic, which can use custom interfaces rather than a single method / delegate
        /// </summary>
        private Dictionary<Type, ServiceRequestExecution> ExecutionMocks { get; set; }

        private Dictionary<Type, IFakeMessageExecutor> FakeMessageExecutors { get; set; }

        private Dictionary<string, IFakeMessageExecutor> GenericFakeMessageExecutors { get; set; }

        private Dictionary<string, XrmFakedRelationship> Relationships { get; set; }


        public IEntityInitializerService EntityInitializerService { get; set; }
        public IAccessRightsRepository AccessRightsRepository { get; set; }

        public int MaxRetrieveCount { get; set; }

        public EntityInitializationLevel InitializationLevel { get; set; }

        public XrmFakedContext()
        {
            MaxRetrieveCount = 5000;

            AttributeMetadataNames = new Dictionary<string, Dictionary<string, string>>();
            Data = new Dictionary<string, Dictionary<Guid, Entity>>();
            ExecutionMocks = new Dictionary<Type, ServiceRequestExecution>();
            OptionSetValuesMetadata = new Dictionary<string, OptionSetMetadata>();
            StatusAttributeMetadata = new Dictionary<string, StatusAttributeMetadata>();

            FakeMessageExecutors = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IFakeMessageExecutor)))
                .Select(t => Activator.CreateInstance(t) as IFakeMessageExecutor)
                .ToDictionary(t => t.GetResponsibleRequestType(), t => t);

            GenericFakeMessageExecutors = new Dictionary<string, IFakeMessageExecutor>();

            Relationships = new Dictionary<string, XrmFakedRelationship>();

            EntityInitializerService = new DefaultEntityInitializerService();

            AccessRightsRepository = new AccessRightsRepository();

            SystemTimeZone = TimeZoneInfo.Local;
            DateBehaviour = DefaultDateBehaviour();

            EntityMetadata = new Dictionary<string, EntityMetadata>();

            InitializationLevel = EntityInitializationLevel.Default;

            ProxyTypesAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// Initializes the context with the provided entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Initialize(IEnumerable<Entity> entities)
        {
            if (Initialised)
            {
                throw new Exception("Initialize should be called only once per unit test execution and XrmFakedContext instance.");
            }

            if (entities == null)
            {
                throw new InvalidOperationException("The entities parameter must be not null");
            }

            foreach (var e in entities)
            {
                AddEntityWithDefaults(e, true);
            }

            Initialised = true;
        }

        public void Initialize(Entity e)
        {
            this.Initialize(new List<Entity>() { e });
        }

        /// <summary>
        /// Enables support for the early-cound types exposed in a specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// An assembly containing early-bound entity types.
        /// </param>
        /// <remarks>
        /// See issue #334 on GitHub. This has quite similar idea as is on SDK method
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.xrm.sdk.client.organizationserviceproxy.enableproxytypes.
        /// </remarks>
        public void EnableProxyTypes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (ProxyTypesAssemblies.Contains(assembly))
            {
                throw new InvalidOperationException($"Proxy types assembly { assembly.GetName().Name } is already enabled.");
            }

            ProxyTypesAssemblies.Add(assembly);
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
            ExecutionMocks.Remove(typeof(T));
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

        public void AddGenericFakeMessageExecutor(string message, IFakeMessageExecutor executor)
        {
            if (!GenericFakeMessageExecutors.ContainsKey(message))
                GenericFakeMessageExecutors.Add(message, executor);
            else
                GenericFakeMessageExecutors[message] = executor;
        }

        public void RemoveGenericFakeMessageExecutor(string message)
        {
            if (GenericFakeMessageExecutors.ContainsKey(message))
                GenericFakeMessageExecutors.Remove(message);
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

        public void AddAttributeMapping(string sourceEntityName, string sourceAttributeName, string targetEntityName, string targetAttributeName)
        {
            if (string.IsNullOrWhiteSpace(sourceEntityName))
                throw new ArgumentNullException("sourceEntityName");
            if (string.IsNullOrWhiteSpace(sourceAttributeName))
                throw new ArgumentNullException("sourceAttributeName");
            if (string.IsNullOrWhiteSpace(targetEntityName))
                throw new ArgumentNullException("targetEntityName");
            if (string.IsNullOrWhiteSpace(targetAttributeName))
                throw new ArgumentNullException("targetAttributeName");

            var entityMap = new Entity
            {
                LogicalName = "entitymap",
                Id = Guid.NewGuid(),
                ["targetentityname"] = targetEntityName,
                ["sourceentityname"] = sourceEntityName
            };

            var attributeMap = new Entity
            {
                LogicalName = "attributemap",
                Id = Guid.NewGuid(),
                ["entitymapid"] = new EntityReference("entitymap", entityMap.Id),
                ["targetattributename"] = targetAttributeName,
                ["sourceattributename"] = sourceAttributeName
            };

            AddEntityWithDefaults(entityMap);
            AddEntityWithDefaults(attributeMap);
        }

        public virtual IOrganizationService GetOrganizationService()
        {
            if (this is XrmRealContext)
            {
                Service = GetOrganizationService();
                return Service;
            }
            return GetFakedOrganizationService(this);
        }

        /// <summary>
        /// Deprecated. Use GetOrganizationService instead
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use GetOrganizationService instead")]
        public IOrganizationService GetFakedOrganizationService()
        {
            return GetFakedOrganizationService(this);
        }

        protected IOrganizationService GetFakedOrganizationService(XrmFakedContext context)
        {
            if (context.Service != null)
            {
                return context.Service;
            }

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
            context.Service = fakedService;

            return context.Service;
        }

        /// <summary>
        /// Fakes the Execute method of the organization service.
        /// Not all the OrganizationRequest are going to be implemented, so stay tunned on updates!
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        public static void FakeExecute(XrmFakedContext context, IOrganizationService fakedService)
        {
            OrganizationResponse response = null;
            Func<OrganizationRequest, OrganizationResponse> execute = (req) =>
            {
                if (context.ExecutionMocks.ContainsKey(req.GetType()))
                    return context.ExecutionMocks[req.GetType()].Invoke(req);

                if (context.FakeMessageExecutors.ContainsKey(req.GetType())
                    && context.FakeMessageExecutors[req.GetType()].CanExecute(req))
                    return context.FakeMessageExecutors[req.GetType()].Execute(req, context);

                if (req.GetType() == typeof(OrganizationRequest)
                    && context.GenericFakeMessageExecutors.ContainsKey(req.RequestName))
                    return context.GenericFakeMessageExecutors[req.RequestName].Execute(req, context);

                throw PullRequestException.NotImplementedOrganizationRequest(req.GetType());
            };

            A.CallTo(() => fakedService.Execute(A<OrganizationRequest>._))
                .Invokes((OrganizationRequest req) => response = execute(req))
                .ReturnsLazily((OrganizationRequest req) => response);
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
            EntityCollection entities = null;
            Func<QueryBase, EntityCollection> retriveMultiple = (QueryBase req) =>
            {
                var request = new RetrieveMultipleRequest { Query = req };

                var executor = new RetrieveMultipleRequestExecutor();
                var response = executor.Execute(request, context) as RetrieveMultipleResponse;

                return response.EntityCollection;
            };

            //refactored from RetrieveMultipleExecutor
            A.CallTo(() => fakedService.RetrieveMultiple(A<QueryBase>._))
                .Invokes((QueryBase req) => entities = retriveMultiple(req))
                .ReturnsLazily((QueryBase req) => entities);
        }

        public IServiceEndpointNotificationService GetFakedServiceEndpointNotificationService()
        {
            return _serviceEndpointNotificationService ??
                   (_serviceEndpointNotificationService = A.Fake<IServiceEndpointNotificationService>());
        }
#if FAKE_XRM_EASY_9
        public IEntityDataSourceRetrieverService GetFakedEntityDataSourceRetrieverService()
        {
            var service = A.Fake<IEntityDataSourceRetrieverService>();
            A.CallTo(() => service.RetrieveEntityDataSource())
                .ReturnsLazily(() => EntityDataSourceRetriever);
            return service;
        }
#endif
    }
}