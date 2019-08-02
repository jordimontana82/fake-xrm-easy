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
              var buId = GetBusinessUnitId(user);
              results.Add("BusinessUnitId", buId);

              var orgId = GetOrganizationId(ctx, user, buId);
              results.Add("OrganizationId", orgId);
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

        private static Guid GetBusinessUnitId(Entity user) {
          var buRef = user.GetAttributeValue<EntityReference>("businessunitid");
          var buId = buRef != null ? buRef.Id : Guid.Empty;
          return buId;
        }

        private static Guid GetOrganizationId(XrmFakedContext ctx, Entity user, Guid buId) {
          var orgId = user.GetAttributeValue<Guid?>("organizationid") ?? Guid.Empty;
          if(orgId == Guid.Empty) {
            var bu = ctx.CreateQuery("businessunit")
                        .Where(b => b.Id == buId)
                        .SingleOrDefault();
            var orgRef = bu.GetAttributeValue<EntityReference>("organizationid");
            orgId = orgRef?.Id ?? Guid.Empty;
          }

          return orgId;
        }

    }
}