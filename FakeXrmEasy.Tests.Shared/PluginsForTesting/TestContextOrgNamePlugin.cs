using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class TestContextOrgNamePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            context.OutputParameters.Add("OrgName", context.OrganizationName);

            
        }
    }
}