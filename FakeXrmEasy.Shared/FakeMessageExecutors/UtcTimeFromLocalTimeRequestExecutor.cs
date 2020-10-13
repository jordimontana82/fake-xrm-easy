using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class UtcTimeFromLocalTimeRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is UtcTimeFromLocalTimeRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as UtcTimeFromLocalTimeRequest;
            
            var res = new UtcTimeFromLocalTimeResponse();
            res["UtcTime"] = TimeZoneInfo.ConvertTimeToUtc(req.LocalTime);
            return res;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(UtcTimeFromLocalTimeRequest);
        }
    }
}