using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class QualifyLeadRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is QualifyLeadRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as QualifyLeadRequest;

            var orgService = ctx.GetOrganizationService();

            if (req.LeadId == null) throw new Exception("Lead Id must be set in request.");

            var leads = (from l in ctx.CreateQuery("lead")
                         where l.Id == req.LeadId.Id
                         select l);

            var leadsCount = leads.Count();

            if (leadsCount != 1) throw new Exception(string.Format("Number of Leads by given LeadId should be 1. Instead it is {0}.", leadsCount));

            // Made here to get access to CreatedEntities collection
            var response = new QualifyLeadResponse();
            response["CreatedEntities"] = new EntityReferenceCollection();

            // Create Account
            if (req.CreateAccount) // ParentAccount
            {
                var account = new Entity("account")
                {
                    Id = Guid.NewGuid()
                };
                account.Attributes["originatingleadid"] = req.LeadId;
                orgService.Create(account);
                response.CreatedEntities.Add(account.ToEntityReference());
            }

            // Create Contact
            if (req.CreateContact)
            {
                var contact = new Entity("contact")
                {
                    Id = Guid.NewGuid()
                };
                contact.Attributes["originatingleadid"] = req.LeadId;
                orgService.Create(contact);
                response.CreatedEntities.Add(contact.ToEntityReference());
            }

            // Create Opportunity
            if (req.CreateOpportunity)
            {
                var opportunity = new Entity("opportunity")
                {
                    Id = Guid.NewGuid()
                };
                opportunity.Attributes["originatingleadid"] = req.LeadId;
                orgService.Create(opportunity);
                response.CreatedEntities.Add(opportunity.ToEntityReference());
            }

            // Actual Lead
            var lead = leads.First();
            lead.Attributes["statuscode"] = req.Status.Value;
            orgService.Update(lead);

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(QualifyLeadRequest);
        }
    }
}