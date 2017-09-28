using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveSharedPrincipalsAndAccessRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveSharedPrincipalsAndAccessRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            RetrieveSharedPrincipalsAndAccessRequest req = (RetrieveSharedPrincipalsAndAccessRequest)request;
            return ctx.AccessRightsRepository.RetrieveSharedPrincipalsAndAccess(req.Target);
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveSharedPrincipalsAndAccessRequest);
        }
    }
}