using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests
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
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(context.UserId);

            var target = (Entity)context.InputParameters["Target"];

            target["unsecure"] = _unsecureConfiguration;
            target["secure"] = _secureConfiguration;
        }
    }
}