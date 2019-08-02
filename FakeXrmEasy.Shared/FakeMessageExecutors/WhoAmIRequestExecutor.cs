using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class WhoAmIRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is WhoAmIRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as WhoAmIRequest;

            var callerId = ctx.CallerId.Id;

            var results = new ParameterCollection {
              { "UserId", callerId }
            };

            var user = ctx.CreateQuery("systemuser")
                          .Where(u => u.Id == callerId)
                          .SingleOrDefault();

            if(user != null) {
              var orgId = user.GetAttributeValue<Guid?>("organizationid");
              results.Add("OrganizationId", orgId ?? Guid.Empty);
            }

            var response = new WhoAmIResponse
            {
                Results = results
            };
            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(WhoAmIRequest);
        }
    }
}