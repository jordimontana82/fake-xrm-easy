#if FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Organization;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveCurrentOrganizationRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveCurrentOrganizationRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = (RetrieveCurrentOrganizationRequest)request;

            OrganizationDetail organizationDetail = new OrganizationDetail();
            organizationDetail.UrlName = "https://fakexrmeasy.crm.dynamics.com";
            organizationDetail.UniqueName = "FakeXrmEasy";
            organizationDetail.OrganizationVersion = "9.0.0.0";

            return new RetrieveCurrentOrganizationResponse
            {
                Results = new ParameterCollection
                {
                    { "Detail", organizationDetail }
                }
            };
            //throw new NotImplementedException();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveCurrentOrganizationRequest);
        }
    }
}
#endif