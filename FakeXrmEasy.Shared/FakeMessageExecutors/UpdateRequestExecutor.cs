using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class UpdateRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is UpdateRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var updateRequest = (UpdateRequest) request;

            var target = (Entity)request.Parameters["Target"];

            var service = ctx.GetOrganizationService();
            service.Update(target);

            return new UpdateResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(UpdateRequest);
        }
    }
}
