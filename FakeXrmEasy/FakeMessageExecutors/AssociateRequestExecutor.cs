using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

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

            if (associateRequest == null)
            {
                throw new Exception("Only associate request can be processed!");
            }

            var relationShipName = associateRequest.Relationship.SchemaName;
            var relationShip = ctx.GetRelationship(relationShipName);

            if (relationShip == null)
            {
                throw new Exception(string.Format("Relationship {0} does not exist in the metadata cache", relationShipName));
            }

            if (associateRequest.Target == null)
            {
                throw new Exception("Association without target is invalid!");
            }

            foreach (var relatedEntity in associateRequest.RelatedEntities)
            {
                var association = new Entity(relationShip.IntersectEntity)
                {
                    Attributes = new AttributeCollection
                        {
                            { relationShip.Entity1Attribute, associateRequest.Target.Id },
                            { relationShip.Entity2Attribute, relatedEntity.Id }
                        }
                };

                association.Id = Guid.NewGuid();
                ctx.AddEntity(association);
            }

            return new AssociateResponse ();
        }
    }
}
