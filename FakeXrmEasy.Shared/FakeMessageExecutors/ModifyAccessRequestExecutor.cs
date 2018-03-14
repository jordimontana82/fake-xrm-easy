using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class ModifyAccessRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is ModifyAccessRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            ModifyAccessRequest req = (ModifyAccessRequest)request;
            ctx.AccessRightsRepository.ModifyAccessOn(req.Target, req.PrincipalAccess);
            return new ModifyAccessResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(ModifyAccessRequest);
        }
    }
}
