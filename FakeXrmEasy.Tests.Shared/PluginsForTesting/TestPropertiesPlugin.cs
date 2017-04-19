using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class TestPropertiesPlugin : IPlugin
    {
        public string Property { get; set; }


        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            Property = "Property Updated";
            
        }
    }
}