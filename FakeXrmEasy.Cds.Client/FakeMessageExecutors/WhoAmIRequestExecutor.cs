using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

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

            var response = new WhoAmIResponse
            {
                Results = new ParameterCollection
                                { { "UserId", ctx.CallerId.Id } }
            };
            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(WhoAmIRequest);
        }
    }
}