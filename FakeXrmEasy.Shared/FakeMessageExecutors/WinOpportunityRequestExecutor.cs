using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class WinOpportunityRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is WinOpportunityRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(WinOpportunityRequest);
        }
    }
}