using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakeXrmEasy.Metadata
{
    internal class MetadataGenerator
    {
        public static IEnumerable<EntityMetadata> FromEarlyBoundEntities(Assembly earlyBoundEntitiesAssembly)
        {
            List<EntityMetadata> entityMetadatas = new List<EntityMetadata>();
            foreach (var earlyBoundEntity in earlyBoundEntitiesAssembly.GetTypes())
            {
                var entityLogicalNameAttribute = (EntityLogicalNameAttribute)Attribute.GetCustomAttribute(earlyBoundEntity, typeof(EntityLogicalNameAttribute));
                if (entityLogicalNameAttribute == null) continue;

                EntityMetadata metadata = new EntityMetadata();
                CrmSvcUtilMetadataGenerator.SetMetadata(metadata, earlyBoundEntity, entityLogicalNameAttribute);
                entityMetadatas.Add(metadata);
            }
            return entityMetadatas;
        }
    }
}
