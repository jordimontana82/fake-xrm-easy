using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class ConfigurationPlugin : IPlugin
    {
        private string _unsecureConfiguration;
        private string _secureConfiguration;

        public ConfigurationPlugin() : this(string.Empty, string.Empty) { }

        public ConfigurationPlugin(string unsecureConfiguration, string secureConfiguration)
        {
            _unsecureConfiguration = unsecureConfiguration;
            _secureConfiguration = secureConfiguration;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            var target = (Entity)context.InputParameters["Target"];

            target["unsecure"] = _unsecureConfiguration;
            target["secure"] = _secureConfiguration;
        }
    }
}