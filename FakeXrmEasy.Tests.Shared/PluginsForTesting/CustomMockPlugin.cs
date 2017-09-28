using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace FakeXrmEasy.Tests
{
    public class CustomMockPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(context.UserId);

            var request = new RetrieveEntityRequest
            {
                LogicalName = "Contact",
                EntityFilters = EntityFilters.All,
                RetrieveAsIfPublished = false
            };

            var response = (RetrieveEntityResponse)service.Execute(request);

            var target = ((Entity)context.InputParameters["Target"]);
            target["response"] = response.ResponseName;
        }
    }
}