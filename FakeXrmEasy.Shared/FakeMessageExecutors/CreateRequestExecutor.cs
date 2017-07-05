using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class CreateRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is CreateRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var createRequest = (CreateRequest)request;

            var service = ctx.GetOrganizationService();

            var guid = service.Create(createRequest.Target);

            return new CreateResponse()
            {
                ResponseName = "Create",
                Results = new ParameterCollection { { "id", guid } }
            };
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(CreateRequest);
        }
    }
}
