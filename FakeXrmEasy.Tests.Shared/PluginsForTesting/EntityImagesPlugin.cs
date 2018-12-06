using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class EntityImagesPlugin : IPlugin
    {
        public EntityImageCollection PreImages { get; set; }
        public EntityImageCollection PostImages { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context == null)
                throw new InvalidPluginExecutionException("Initialize IPluginExecutionContext fail.");

            if (!context.InputParameters.Contains("Target")) {
                throw new InvalidPluginExecutionException("Target not set.");
            }

            // pass back pre/post entity images.
            Entity entity = (Entity)context.InputParameters["Target"];
            entity["PreEntityImages"] = context.PreEntityImages;
            entity["PostEntityImages"] = context.PostEntityImages;
        }
    }
}
