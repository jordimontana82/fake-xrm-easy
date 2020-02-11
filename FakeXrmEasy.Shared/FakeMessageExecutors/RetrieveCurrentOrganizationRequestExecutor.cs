using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveCurrentOrganizationRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
#if FAKE_XRM_EASY || FAKE_XRM_EASY_2013
            return false;
#else
            return request is RetrieveCurrentOrganizationRequest;
#endif
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013
            RetrieveCurrentOrganizationRequest createRequest = (RetrieveCurrentOrganizationRequest)request;

            var service = ctx.GetOrganizationService();

            return new RetrieveCurrentOrganizationResponse()
            {
                ResponseName = "RetrieveCurrentOrganization",
                Results = new ParameterCollection { { "Detail" , 
                new Microsoft.Xrm.Sdk.Organization.OrganizationDetail()
                {
                    OrganizationId = new Guid("59115200-433D-4E75-9351-0AC8602C1890"),
                    OrganizationVersion = "9.0.3.7",
                    UniqueName = "FakeXRM",
                    State = Microsoft.Xrm.Sdk.Organization.OrganizationState.Enabled,
                    FriendlyName = "FakeXRM",

                }} }
            };
#else
            throw new NotImplementedException("The Request RetrieveCurrentOrganizationRequest is not working on this CRM Version");
#endif
        }

        public Type GetResponsibleRequestType()
        {
#if FAKE_XRM_EASY || FAKE_XRM_EASY_2013
            throw new NotImplementedException("The Request RetrieveCurrentOrganizationRequest is not working on this CRM Version");
#else
            return typeof(RetrieveCurrentOrganizationRequest);
#endif
        }
    }
}
