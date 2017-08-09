using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class RetrieveServicesPlugin : IPlugin
    {
        /// <summary>
        /// A plug-in that auto generates an account number when an
        /// account is created.
        /// </summary>
        /// <remarks>Register this plug-in on the Create message, account entity,
        /// and pre-operation stage.
        /// </remarks>
        //<snippetAccountNumberPlugin2>
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
        }
    }
}