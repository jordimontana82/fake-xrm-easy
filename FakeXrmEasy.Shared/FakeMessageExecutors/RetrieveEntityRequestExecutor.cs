using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xrm.Sdk.Client;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveEntityRequestExecutor : IFakeMessageExecutor
    {
        public Dictionary<string, Dictionary<string, AttributeMetadata>> FakeAttributeMetadata = new Dictionary<string, Dictionary<string, AttributeMetadata>>();
        public Dictionary<string, EntityMetadata> FakeEntityMetadata = new Dictionary<string, EntityMetadata>();

        public void AddFakeEntityMetadata(string sEntityName, EntityMetadata metadata)
        {
            if (!FakeEntityMetadata.ContainsKey(sEntityName))
            {
                FakeEntityMetadata.Add(sEntityName, metadata);
                return;
            }

            FakeEntityMetadata[sEntityName] = metadata;
        }

        public void AddFakeAttributeMetadata(string sEntityName, string sAttributeName, AttributeMetadata metadata)
        {
            metadata.SchemaName = sAttributeName;

            if (!FakeAttributeMetadata.ContainsKey(sEntityName))
                FakeAttributeMetadata.Add(sEntityName, new Dictionary<string, AttributeMetadata>());

            if (!FakeAttributeMetadata[sEntityName].ContainsKey(sAttributeName))
            {
                FakeAttributeMetadata[sEntityName].Add(sAttributeName, metadata);
            }
            else
            {
                FakeAttributeMetadata[sEntityName][sAttributeName] = metadata;
            }
        }

        protected AttributeMetadata GetAttributeMetadataFor(string sEntityName, string sAttributeName, Type attributeType)
        {
            if (FakeAttributeMetadata.ContainsKey(sEntityName))
                if (FakeAttributeMetadata[sEntityName].ContainsKey(sAttributeName))
                    return FakeAttributeMetadata[sEntityName][sAttributeName];

            if (attributeType == typeof(string))
            {
                return new StringAttributeMetadata(sAttributeName);
            }


            //Default
            return new StringAttributeMetadata(sAttributeName);
        }

        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveEntityRequest;
        }
        public static Type GetEntityProxyType(string entityName, XrmFakedContext ctx)
        {
            //Find the reflected type in the proxy types assembly
            var assembly = ctx.ProxyTypesAssembly;
            var subClassType = assembly.GetTypes()
                    .Where(t => typeof(Entity).IsAssignableFrom(t))
                    .Where(t => t.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true).Length > 0)
                    .Where(t => ((EntityLogicalNameAttribute)t.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true)[0]).LogicalName.Equals(entityName.ToLower()))
                    .FirstOrDefault();

            if (subClassType == null)
            {
                throw new Exception(string.Format("Entity {0} was not found in the proxy types", entityName));
            }
            return subClassType;
        }
        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as RetrieveEntityRequest;

            if (ctx.ProxyTypesAssembly == null)
            {
                throw new Exception("A proxy types assembly must be used");
            }

            if (string.IsNullOrWhiteSpace(req.LogicalName))
            {
                throw new Exception("A logical name property must be specified in the request");
            }

            if (req.EntityFilters == Microsoft.Xrm.Sdk.Metadata.EntityFilters.Entity ||
                req.EntityFilters == Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes)
            {
                if(!FakeEntityMetadata.ContainsKey(req.LogicalName))
                {
                    throw new Exception("The specified entity name wasn't found in the metadata cache");
                }

                //Find the reflected type in the proxy types assembly
                var subClassType = GetEntityProxyType(req.LogicalName, ctx);


                //Get that type properties
                var attributes = subClassType
                    .GetProperties()
                    .Where(pi => pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true).Length > 0)
                    .ToList();


                var computedAttributeMetadataList = new List<AttributeMetadata>();

                foreach (var attr in attributes)
                {
                    var attrName = attr.Name;
                    var attributeType = attr.PropertyType;

                    if (attr != null && attr.Name != null)
                    {
                        AttributeMetadata attrMetadata = GetAttributeMetadataFor(req.LogicalName, attrName.ToLower(), attributeType);
                        computedAttributeMetadataList.Add(attrMetadata);
                    }
                }

                var entityMetadata = FakeEntityMetadata[req.LogicalName];

                //AttributeMetadata is internal set in a sealed class so... just doing this
                entityMetadata.GetType().GetProperty("Attributes").SetValue(entityMetadata, computedAttributeMetadataList.ToArray(), null);


                //entityMetadata["AttributeMetadata"] = computedAttributeMetadataList.ToArray();

                var response = new RetrieveEntityResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", entityMetadata }
                        }
                };

                return response;
            }

            throw new Exception("Entity Filter not supported");
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveEntityRequest);
        }
    }
}
