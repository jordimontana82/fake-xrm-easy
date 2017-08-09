using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveOptionSetRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveOptionSetRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var retrieveOptionSetRequest = (RetrieveOptionSetRequest)request;

            var name = retrieveOptionSetRequest.Name;

            if (string.IsNullOrEmpty(name))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not retrieve option set without a name.");
            }

            if (!ctx.OptionSetValuesMetadata.ContainsKey(name))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("An OptionSetMetadata with the name {0} does not exist.", name));
            }

            var optionSetMetadata = ctx.OptionSetValuesMetadata[name];

            var response = new RetrieveOptionSetResponse()
            {
                Results = new ParameterCollection
                        {
                            { "OptionSetMetadata", optionSetMetadata }
                        }
            };

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveOptionSetRequest);
        }
    }
}