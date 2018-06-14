using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class DoNothingPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // literally does nothing.
        }
    }
}
