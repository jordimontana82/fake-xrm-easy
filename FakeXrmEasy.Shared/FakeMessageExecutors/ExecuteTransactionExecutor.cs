#if FAKE_XRM_EASY_2016

using System;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class ExecuteTransactionExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is ExecuteTransactionRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var executeTransactionRequest = (ExecuteTransactionRequest)request;
            var response = new ExecuteTransactionResponse { ["Responses"] = new OrganizationResponseCollection() };

            var service = ctx.GetFakedOrganizationService();

            foreach (var r in executeTransactionRequest.Requests)
            {
                var result = service.Execute(r);

                if (executeTransactionRequest.ReturnResponses.HasValue && executeTransactionRequest.ReturnResponses.Value)
                {
                    response.Responses.Add(result);        
                }
            }
            return response;
        }
    }
}
#endif