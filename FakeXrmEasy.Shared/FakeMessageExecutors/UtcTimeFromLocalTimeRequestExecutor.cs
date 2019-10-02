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
            
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(req.LocalTime);

            ctx.ConvertToUtc(req.LocalTime);
            var res = new UtcTimeFromLocalTimeResponse();
            res["UtcTime"] = utcTime;
            return res;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(UtcTimeFromLocalTimeRequest);
        }
    }
}