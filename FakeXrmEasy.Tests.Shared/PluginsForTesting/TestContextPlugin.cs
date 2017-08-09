using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class TestContextPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (string.IsNullOrEmpty(context.MessageName))
            {
                throw new InvalidPluginExecutionException("MessageName was null or empty!");
            }

            if (context.UserId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("User ID was empty GUID");
            }

            if (context.InitiatingUserId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Initiating User ID was empty GUID");
            }
        }
    }
}