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

            if (string.IsNullOrWhiteSpace(req.LogicalName))
            {
                throw new Exception("A logical name property must be specified in the request");
            }

            // HasFlag -> used to verify flag matches --> to verify EntityFilters.Entity | EntityFilters.Attributes
            if (req.EntityFilters.HasFlag(Microsoft.Xrm.Sdk.Metadata.EntityFilters.Entity) ||
                req.EntityFilters.HasFlag(Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes))
            {
                if(!ctx.EntityMetadata.ContainsKey(req.LogicalName))
                {
                    throw new Exception($"Entity '{req.LogicalName}' is not found in the metadata cache");
                }

                var entityMetadata = ctx.GetEntityMetadataByName(req.LogicalName);

                var response = new RetrieveEntityResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", entityMetadata }
                        }
                };

                return response;
            }

            throw new Exception("At least EntityFilters.Entity or EntityFilters.Attributes must be present on EntityFilters of Request.");
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveEntityRequest);
        }
    }
}
