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
            
            var service = ctx.GetFakedOrganizationService();

            var id = service.Create(createRequest.Target);

            var response = new CreateResponse()
            {
                Results = new ParameterCollection
                                 {
                                    { "id", id }
                                 }
            };
            return response;

        }
    }
}
