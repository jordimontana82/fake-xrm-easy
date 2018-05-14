using System;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class ServiceEndpointNotificationPlugin : IPlugin
    {
        private readonly Guid _serviceEndpointId;

        public ServiceEndpointNotificationPlugin(string unSecureConfig, string secureConfig)
        {
            if (String.IsNullOrEmpty(unSecureConfig) || !Guid.TryParse(unSecureConfig, out _serviceEndpointId))
            {
                throw new InvalidPluginExecutionException("Service endpoint ID should be passed as config.");
            }
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the organization service from the service provider
            IOrganizationService service = (IOrganizationService)
                serviceProvider.GetService(typeof(IOrganizationService));

            // Obtain the tracing service from the service provider
            ITracingService tracingService = (ITracingService)
                serviceProvider.GetService(typeof(ITracingService));

            // Obtain the service endpoint notificaito nservice from the service provider
            IServiceEndpointNotificationService cloudService = (IServiceEndpointNotificationService)
                serviceProvider.GetService(typeof(IServiceEndpointNotificationService));

            try
            {
                tracingService.Trace("Posting the execution context.");
                string response = cloudService.Execute(new EntityReference("serviceendpoint", _serviceEndpointId), context);
                if (!String.IsNullOrEmpty(response))
                {
                    tracingService.Trace("Response = {0}", response);
                }
                tracingService.Trace("Done.");
            }
            catch (Exception e)
            {
                tracingService.Trace("Exception: {0}", e.ToString());
                throw;
            }
        }
    }
}
