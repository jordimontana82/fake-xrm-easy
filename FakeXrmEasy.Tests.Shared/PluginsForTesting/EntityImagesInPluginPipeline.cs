using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class EntityImagesInPluginPipeline : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context == null)
                throw new InvalidPluginExecutionException("Initialize IPluginExecutionContext fail.");

            if (!context.InputParameters.Contains("Target"))
            {
                throw new InvalidPluginExecutionException("Target not set.");
            }

            var service = (IOrganizationService)serviceProvider.GetService(typeof(IOrganizationService));

            // pass back pre/post entity images.
            foreach (var preImage in context.PreEntityImages)
            {
                Entity preImageEntity = preImage.Value;
                preImageEntity.Attributes.Remove("statecode");
                bool imageContainedId = preImageEntity.Attributes.Remove(string.Format("{0}id", preImageEntity.LogicalName));
                preImageEntity["preimagename"] = preImage.Key;
                preImageEntity["containedid"] = imageContainedId;
                preImageEntity["pluginstage"] = context.Stage;
                preImageEntity.Id = Guid.NewGuid();

                service.Create(preImageEntity);
            }

            foreach (var postImage in context.PostEntityImages)
            {
                Entity postImageEntity = postImage.Value;
                postImageEntity.Attributes.Remove("statecode");
                bool imageContainedId = postImageEntity.Attributes.Remove(string.Format("{0}id", postImageEntity.LogicalName));
                postImageEntity["postimagename"] = postImage.Key;
                postImageEntity["containedid"] = imageContainedId;
                postImageEntity["pluginstage"] = context.Stage;
                postImageEntity.Id = Guid.NewGuid();

                service.Create(postImageEntity);
            }
        }
    }
}
