using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class GrantAccessRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is GrantAccessRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            GrantAccessRequest req = (GrantAccessRequest)request;
            ctx.AccessRightsRepository.GrantAccessTo(req.Target, req.PrincipalAccess);
            return new GrantAccessResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(GrantAccessRequest);
        }
    }
}