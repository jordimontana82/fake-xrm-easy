using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using FakeXrmEasy.Extensions;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveAllEntitiesRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveAllEntitiesRequest;
        }
        
        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as RetrieveAllEntitiesRequest;

            if (req.EntityFilters.HasFlag(EntityFilters.Entity) ||
                req.EntityFilters.HasFlag(EntityFilters.Attributes) ||
                req.EntityFilters.HasFlag(EntityFilters.Privileges) ||
                req.EntityFilters.HasFlag(EntityFilters.Relationships))
            {
                var allEntities = ctx.EntityMetadata.Values.Select(x => x.Copy()).ToArray();
                foreach (var entityMetadata in allEntities)
                {
                    if (!req.EntityFilters.HasFlag(EntityFilters.Attributes))
                    {
                        entityMetadata.SetAttributeCollection(null);
                    }

                    if (!req.EntityFilters.HasFlag(EntityFilters.Privileges))
                    {
                        entityMetadata.SetSecurityPrivilegeCollection(null);
                    }

                    if (!req.EntityFilters.HasFlag(EntityFilters.Relationships))
                    {
                        entityMetadata.SetOneToManyRelationshipCollection(null);
                        entityMetadata.SetManyToOneRelationshipCollection(null);
                        entityMetadata.SetManyToManyRelationshipCollection(null);
                    }
                }

                var response = new RetrieveAllEntitiesResponse()
                {
                    Results = new ParameterCollection
                        {
                            { "EntityMetadata", allEntities }
                        }
                };

                return response;
            }

            throw new Exception("Entity Filter not supported");
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveAllEntitiesRequest);
        }
    }
}
