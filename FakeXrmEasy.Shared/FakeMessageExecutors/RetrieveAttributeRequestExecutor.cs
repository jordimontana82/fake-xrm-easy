using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveAttributeRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveAttributeRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            throw PullRequestException.NotImplementedOrganizationRequest(request.GetType());
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveAttributeRequest);
        }
    }
}
