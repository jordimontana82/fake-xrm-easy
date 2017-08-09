using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public abstract partial class BasePlugin : IPlugin
    {
        protected class LocalPluginContext<T> : IDisposable where T : Entity
        {
            internal OrganizationServiceContext CrmContext { get; private set; }
            internal IServiceProvider ServiceProvider { get; private set; }
            internal IOrganizationServiceFactory ServiceFactory { get; private set; }
            internal IOrganizationService OrganizationService { get; private set; }
            internal IPluginExecutionContext PluginExecutionContext { get; private set; }
            internal ITracingService TracingService { get; private set; }
            internal eStage Stage { get { return (eStage)this.PluginExecutionContext.Stage; } }
            internal int Depth { get { return this.PluginExecutionContext.Depth; } }
            internal string MessageName { get { return this.PluginExecutionContext.MessageName; } }

            internal LocalPluginContext(IServiceProvider serviceProvider)
            {
                if (serviceProvider == null)
                    throw new ArgumentNullException("serviceProvider");

                // Obtain the tracing service from the service provider.
                this.TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

                // Obtain the execution context service from the service provider.
                this.PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                // Obtain the Organization Service factory service from the service provider
                this.ServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                // Use the factory to generate the Organization Service.
                this.OrganizationService = this.ServiceFactory.CreateOrganizationService(this.PluginExecutionContext.UserId);

                // Generate the CrmContext to use with LINQ etc
                this.CrmContext = new Microsoft.Xrm.Sdk.Client.OrganizationServiceContext(this.OrganizationService);
            }

            internal void Trace(string message)
            {
                if (string.IsNullOrWhiteSpace(message) || this.TracingService == null) return;

                if (this.PluginExecutionContext == null)
                    this.TracingService.Trace(message);
                else
                {
                    this.TracingService.Trace(
                        "{0}, Correlation Id: {1}, Initiating User: {2}",
                        message,
                        this.PluginExecutionContext.CorrelationId,
                        this.PluginExecutionContext.InitiatingUserId);
                }
            }

            public void Dispose()
            {
                if (this.CrmContext != null)
                    this.CrmContext.Dispose();
            }

            /// <summary>
            /// Returns the first registered 'Pre' image for the pipeline execution
            /// </summary>
            internal T PreImage
            {
                get
                {
                    if (this.PluginExecutionContext.PreEntityImages.Any())
                        return GetEntityAsType(this.PluginExecutionContext.PreEntityImages[this.PluginExecutionContext.PreEntityImages.FirstOrDefault().Key]);
                    return null;
                }
            }

            /// <summary>
            /// Returns the first registered 'Post' image for the pipeline execution
            /// </summary>
            internal T PostImage
            {
                get
                {
                    if (this.PluginExecutionContext.PostEntityImages.Any())
                        return GetEntityAsType(this.PluginExecutionContext.PostEntityImages[this.PluginExecutionContext.PostEntityImages.FirstOrDefault().Key]);
                    return null;
                }
            }

            /// <summary>
            /// Returns the 'Target' of the message if available
            /// This is an 'Entity' instead of the specified type in order to retain the same instance of the 'Entity' object. This allows for updates to the target in a 'Pre' stage that
            /// will get persisted during the transaction.
            /// </summary>
            internal Entity TargetEntity
            {
                get
                {
                    if (this.PluginExecutionContext.InputParameters.Contains("Target"))
                        return this.PluginExecutionContext.InputParameters["Target"] as Entity;
                    return null;
                }
            }

            /// <summary>
            /// Returns the 'Target' of the message as an EntityReference if available
            /// </summary>
            internal EntityReference TargetEntityReference
            {
                get
                {
                    if (this.PluginExecutionContext.InputParameters.Contains("Target"))
                        return this.PluginExecutionContext.InputParameters["Target"] as EntityReference;
                    return null;
                }
            }

            private T GetEntityAsType(Entity entity)
            {
                if (typeof(T) == entity.GetType())
                    return entity as T;
                else
                    return entity.ToEntity<T>();
            }
        }

        protected enum eStage
        {
            PreValidation = 10,
            PreOperation = 20,
            PostOperation = 40
        }

        protected class PluginEvent
        {
            /// <summary>
            /// Execution pipeline stage that the plugin should be registered against.
            /// </summary>
            public eStage Stage { get; set; }

            /// <summary>
            /// Logical name of the entity that the plugin should be registered against. Leave 'null' to register against all entities.
            /// </summary>
            public string EntityName { get; set; }

            /// <summary>
            /// Name of the message that the plugin should be triggered off of.
            /// </summary>
            public string MessageName { get; set; }

            /// <summary>
            /// Method that should be executed when the conditions of the Plugin Event have been met.
            /// </summary>
            public Action<IServiceProvider> PluginAction { get; set; }

            public PluginEvent(eStage stage, string messageName, string entityName, Action<IServiceProvider> pluginAction)
            {
                Stage = stage;
                MessageName = messageName;
                EntityName = entityName;
                PluginAction = pluginAction;
            }
        }

        private Collection<PluginEvent> registeredEvents;

        /// <summary>
        /// Gets the List of events that the plug-in should fire for. Each List
        /// </summary>
        protected Collection<PluginEvent> RegisteredEvents
        {
            get
            {
                if (this.registeredEvents == null)
                    this.registeredEvents = new Collection<PluginEvent>();
                return this.registeredEvents;
            }
        }

        /// <summary>
        /// Initializes a new instance of the BasePlugin class.
        /// </summary>
        internal BasePlugin(string unsecureConfig, string secureConfig)
        {
            this.UnsecureConfig = unsecureConfig;
            this.SecureConfig = secureConfig;
        }

        //internal BasePlugin()
        //{
        //    this.UnsecureConfig = null;
        //    this.SecureConfig = null;
        //}
        /// <summary>
        /// Un secure configuration specified during the registration of the plugin step
        /// </summary>
        public string UnsecureConfig { get; private set; }

        /// <summary>
        /// Secure configuration specified during the registration of the plugin step
        /// </summary>
        public string SecureConfig { get; private set; }

        /// <summary>
        /// Executes the plug-in.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        /// For improved performance, Microsoft Dynamics CRM caches plug-in instances.
        /// The plug-in's Execute method should be written to be stateless as the constructor
        /// is not called for every invocation of the plug-in. Also, multiple system threads
        /// could execute the plug-in at the same time. All per invocation state information
        /// is stored in the context. This means that you should not use global variables in plug-ins.
        /// </remarks>
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException("serviceProvider");

            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Entered {0}.Execute()", this.GetType().ToString()));
            try
            {
                // Iterate over all of the expected registered events to ensure that the plugin
                // has been invoked by an expected event
                var entityActions =
                    (from a in this.RegisteredEvents
                     where (
                 (int)a.Stage == pluginContext.Stage &&
                     (string.IsNullOrWhiteSpace(a.MessageName) ? true : a.MessageName.ToLowerInvariant() == pluginContext.MessageName.ToLowerInvariant()) &&
                     (string.IsNullOrWhiteSpace(a.EntityName) ? true : a.EntityName.ToLowerInvariant() == pluginContext.PrimaryEntityName.ToLowerInvariant())
                 )
                     select a.PluginAction);

                if (entityActions.Any())
                {
                    foreach (var entityAction in entityActions)
                    {
                        tracingService.Trace(string.Format(
                            CultureInfo.InvariantCulture,
                            "{0} is firing for Entity: {1}, Message: {2}, Method: {3}",
                            this.GetType().ToString(),
                            pluginContext.PrimaryEntityName,
                            pluginContext.MessageName,
                            entityAction.Method.Name));

                        entityAction.Invoke(serviceProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", ex.ToString()));
                throw;
            }
            finally
            {
                tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Exiting {0}.Execute()", this.GetType().ToString()));
            }
        }
    }
}