using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class LoseOpportunityRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is LoseOpportunityRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as LoseOpportunityRequest;

            // Check if OpportunityClose and Status were passed to request
            if (req.OpportunityClose != null &&
                req.Status != null)
            {
                // LoseOpportunityRequest.OpportunityClose.OpportunityId
                var opportunityReference = req.OpportunityClose.GetAttributeValue<EntityReference>("opportunityid");
                var opportunityId = opportunityReference.Id;

                // Get Opportunities (in good scenario, should return 1 record)
                var opportunities = (from op in ctx.CreateQuery("opportunity")
                                     where op.Id == opportunityId
                                     select op);

                // More than one if to check and give better feedback to user
                if (opportunities.Count() < 1) throw new Exception(string.Format("No Opportunity found with Id = {0}", opportunityId));
                else if (opportunities.Count() > 1) throw new Exception(string.Format("More than one Opportunity found with Id = {0}", opportunityId));
                else
                {
                    var opportunity = opportunities.FirstOrDefault();
                    opportunity.Attributes["statuscode"] = req.Status;

                    ctx.GetOrganizationService().Update(opportunity);

                    return new LoseOpportunityResponse();
                }
            }
            else
            {
                throw new Exception("OpportunityClose or Status was not passed to request.");
            }
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(LoseOpportunityRequest);
        }
    }
}