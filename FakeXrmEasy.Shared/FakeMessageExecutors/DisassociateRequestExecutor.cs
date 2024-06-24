using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

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
            var service = ctx.GetOrganizationService();

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
                var isFrom1to2 = disassociateRequest.Target.LogicalName == relationShip.Entity1LogicalName
                                      || relatedEntity.LogicalName != relationShip.Entity1LogicalName
                                      || String.IsNullOrWhiteSpace(disassociateRequest.Target.LogicalName);
                var fromAttribute = isFrom1to2 ? relationShip.Entity1Attribute : relationShip.Entity2Attribute;
                var toAttribute = isFrom1to2 ? relationShip.Entity2Attribute : relationShip.Entity1Attribute;

                var query = new QueryExpression(relationShip.IntersectEntity)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression(LogicalOperator.And)
                };

                query.Criteria.AddCondition(new ConditionExpression(fromAttribute,
                    ConditionOperator.Equal, disassociateRequest.Target.Id));
                query.Criteria.AddCondition(new ConditionExpression(toAttribute,
                    ConditionOperator.Equal, relatedEntity.Id));

                var results = service.RetrieveMultiple(query);

                if (results.Entities.Count == 1)
                {
                    service.Delete(relationShip.IntersectEntity, results.Entities.First().Id);
                }
            }

            return new DisassociateResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(DisassociateRequest);
        }
    }
}