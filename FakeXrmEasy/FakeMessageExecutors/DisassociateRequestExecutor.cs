using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class DisassociateRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is DisassociateRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var disassociateRequest = request as DisassociateRequest;
            var service = ctx.GetFakedOrganizationService();

            if (disassociateRequest == null)
            {
                throw new Exception("Only disassociate request can be processed!");
            }

            var relationShipName = disassociateRequest.Relationship.SchemaName;
            var relationShip = ctx.GetRelationship(relationShipName);

            if (relationShip == null)
            {
                throw new Exception(string.Format("Relationship {0} does not exist in the metadata cache", relationShipName));
            }

            if (disassociateRequest.Target == null)
            {
                throw new Exception("Disassociation without target is invalid!");
            }

            foreach (var relatedEntity in disassociateRequest.RelatedEntities)
            {
                var query = new QueryExpression(relationShipName)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression(LogicalOperator.And)
                };

                query.Criteria.AddCondition(new ConditionExpression(relationShip.Entity1Attribute,
                    ConditionOperator.Equal, disassociateRequest.Target.Id));
                query.Criteria.AddCondition(new ConditionExpression(relationShip.Entity2Attribute,
                    ConditionOperator.Equal, relatedEntity.Id));

                var results = service.RetrieveMultiple(query);

                if (results.Entities.Count == 1)
                {
                    service.Delete(relationShipName, results.Entities.First().Id);
                }
            }

            return new DisassociateResponse();
        }
    }
}
