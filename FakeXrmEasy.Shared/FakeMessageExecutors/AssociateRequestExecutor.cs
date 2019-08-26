using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class AssociateRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is AssociateRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var associateRequest = request as AssociateRequest;
            var service = ctx.GetOrganizationService();

            if (associateRequest == null)
            {
                throw new Exception("Only associate request can be processed!");
            }

            var associateRelationship = associateRequest.Relationship;
            var relationShipName = associateRelationship.SchemaName;
            var fakeRelationShip = ctx.GetRelationship(relationShipName);

            if (fakeRelationShip == null)
            {
                throw new Exception(string.Format("Relationship {0} does not exist in the metadata cache", relationShipName));
            }

            if (associateRequest.Target == null)
            {
                throw new Exception("Association without target is invalid!");
            }

            foreach (var relatedEntityReference in associateRequest.RelatedEntities)
            {
                if (fakeRelationShip.RelationshipType == XrmFakedRelationship.enmFakeRelationshipType.ManyToMany)
                {
                    var isFrom1to2 = associateRequest.Target.LogicalName == fakeRelationShip.Entity1LogicalName
                                         || relatedEntityReference.LogicalName != fakeRelationShip.Entity1LogicalName
                                         || String.IsNullOrWhiteSpace(associateRequest.Target.LogicalName);
                    var fromAttribute = isFrom1to2 ? fakeRelationShip.Entity1Attribute : fakeRelationShip.Entity2Attribute;
                    var toAttribute = isFrom1to2 ? fakeRelationShip.Entity2Attribute : fakeRelationShip.Entity1Attribute;
                    var fromEntityName = isFrom1to2 ? fakeRelationShip.Entity1LogicalName : fakeRelationShip.Entity2LogicalName;
                    var toEntityName = isFrom1to2 ? fakeRelationShip.Entity2LogicalName : fakeRelationShip.Entity1LogicalName;

                    //Check records exist
                    var targetExists = ctx.CreateQuery(fromEntityName)
                                                .Where(e => e.Id == associateRequest.Target.Id)
                                                .FirstOrDefault() != null;

                    if (!targetExists)
                    {
                        throw new Exception(string.Format("{0} with Id {1} doesn't exist", fromEntityName, associateRequest.Target.Id.ToString()));
                    }

                    var relatedExists = ctx.CreateQuery(toEntityName)
                                                .Where(e => e.Id == relatedEntityReference.Id)
                                                .FirstOrDefault() != null;

                    if (!relatedExists)
                    {
                        throw new Exception(string.Format("{0} with Id {1} doesn't exist", toEntityName, relatedEntityReference.Id.ToString()));
                    }

                    var association = new Entity(fakeRelationShip.IntersectEntity)
                    {
                        Attributes = new AttributeCollection
                        {
                            { fromAttribute, associateRequest.Target.Id },
                            { toAttribute, relatedEntityReference.Id }
                        }
                    };

                    service.Create(association);
                }
                else
                {
                    //One to many
                    //Get entity to update
                    var entityToUpdate = new Entity(relatedEntityReference.LogicalName)
                    {
                        Id = relatedEntityReference.Id
                    };

                    entityToUpdate[fakeRelationShip.Entity2Attribute] = associateRequest.Target;
                    service.Update(entityToUpdate);
                }
            }

            return new AssociateResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(AssociateRequest);
        }
    }
}