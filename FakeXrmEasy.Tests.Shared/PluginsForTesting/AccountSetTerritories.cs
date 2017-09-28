using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public partial class AccountSetTerritories : BasePlugin
    {
        //public AccountSetTerritories()
        //{
        //}

        public AccountSetTerritories(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            RegisteredEvents.Add(new PluginEvent(eStage.PreOperation, "Create", "account", ExecutePluginLogic));
            RegisteredEvents.Add(new PluginEvent(eStage.PreOperation, "Update", "account", ExecutePluginLogic));
        }

        public void ExecutePluginLogic(IServiceProvider serviceProvider)
        {
            using (var localContext = new LocalPluginContext<Entity>(serviceProvider))
            {
                if (localContext.Depth > 1)
                {
                    localContext.Trace("Error: Context Depth is over 1. Quit the Plug-in process.");
                    return;
                }
            }
        }
    }
}