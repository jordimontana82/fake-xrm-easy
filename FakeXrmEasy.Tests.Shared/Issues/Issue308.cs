using System;
using System.Collections.Generic;
using System.Text;
using FakeItEasy;
using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue308
    {
        [Fact]
        public void Should_post_plugin_context_to_service_endpoint()
        {
            var endpointId = Guid.NewGuid();
            var fakedContext = new XrmFakedContext();

            var fakedServiceEndpointNotificationService = fakedContext.GetFakedServiceEndpointNotificationService();

            A.CallTo(() => fakedServiceEndpointNotificationService.Execute(A<EntityReference>._, A<IExecutionContext>._))
                .Returns("response");

            var plugCtx = fakedContext.GetDefaultPluginContext();

            var fakedPlugin =
                fakedContext
                    .ExecutePluginWithConfigurations<ServiceEndpointNotificationPlugin>(plugCtx, endpointId.ToString(), null );



            A.CallTo(() => fakedServiceEndpointNotificationService.Execute(A<EntityReference>._, A<IExecutionContext>._))
                .MustHaveHappened();

        }
    }
}
