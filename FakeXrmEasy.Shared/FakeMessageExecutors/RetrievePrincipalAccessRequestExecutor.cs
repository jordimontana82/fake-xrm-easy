using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.Linq;


namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrievePrincipalAccessRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrievePrincipalAccessRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            RetrievePrincipalAccessRequest req = (RetrievePrincipalAccessRequest)request;
            return ctx.AccessRightsRepository.RetrievePrincipalAccess(req.Target, req.Principal);
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrievePrincipalAccessRequest);
        }
    }
}
