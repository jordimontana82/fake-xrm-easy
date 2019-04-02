using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class UpsertRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is UpsertRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var upsertRequest = (UpsertRequest)request;
            bool recordCreated;

            var service = ctx.GetOrganizationService();

            if (ctx.Data.ContainsKey(upsertRequest.Target.LogicalName) &&
                ctx.Data[upsertRequest.Target.LogicalName].ContainsKey(upsertRequest.Target.Id))
            {
                recordCreated = false;
                service.Update(upsertRequest.Target);
            }
            else
            {
                recordCreated = true;
                service.Create(upsertRequest.Target);
            }
            
            var result = new UpsertResponse();
            result.Results.Add("RecordCreated", recordCreated);
            result.Results.Add("Target", upsertRequest.Target.ToEntityReference());
            return result;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(UpsertRequest);
        }
    }
}
#endif
