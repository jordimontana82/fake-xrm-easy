using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class ExecuteMultipleRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is ExecuteMultipleRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var executeMultipleRequest = (ExecuteMultipleRequest)request;

            if (executeMultipleRequest.Settings == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "You need to pass a value for 'Settings' in execute multiple request");
            }

            if (executeMultipleRequest.Requests == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "You need to pass a value for 'Requests' in execute multiple request");
            }

            var service = ctx.GetFakedOrganizationService();

            var response = new ExecuteMultipleResponse();
            response.Results["Responses"] = new ExecuteMultipleResponseItemCollection();

            for (var i = 0; i < executeMultipleRequest.Requests.Count; i++)
            {
                var executeRequest = executeMultipleRequest.Requests[i];

                try
                {
                    OrganizationResponse resp = service.Execute(executeRequest);

                    if (executeMultipleRequest.Settings.ReturnResponses)
                    {
                        response.Responses.Add(new ExecuteMultipleResponseItem
                        {
                            RequestIndex = i,
                            Response = resp
                        });
                    }
                }
                catch (Exception ex)
                {
                    if (!response.IsFaulted)
                    {
                        response.Results["IsFaulted"] = true;
                    }

                    response.Responses.Add(new ExecuteMultipleResponseItem
                    {
                        Fault = new OrganizationServiceFault { Message = ex.Message },
                        RequestIndex = i
                    });

                    if (!executeMultipleRequest.Settings.ContinueOnError)
                    {
                        break;
                    }
                }
            }

            // Implement response behaviour as in https://msdn.microsoft.com/en-us/library/jj863631.aspx
            if (executeMultipleRequest.Settings.ReturnResponses)
            {
                response.Results["response.Responses"] = response.Responses;
            }
            else if (response.Responses.Any(resp => resp.Fault != null))
            {
                var failures = new ExecuteMultipleResponseItemCollection();

                failures.AddRange(response.Responses.Where(resp => resp.Fault != null));

                response.Results["response.Responses"] = failures;
            }

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(ExecuteMultipleRequest);
        }
    }
}