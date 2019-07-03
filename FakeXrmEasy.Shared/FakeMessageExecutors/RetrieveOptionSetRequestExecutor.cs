using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

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

			if (retrieveOptionSetRequest.Parameters.ContainsKey("MetadataId")) //ToDo: Implement retrieving option sets by Id
			{
				FakeOrganizationServiceFault.Throw(ErrorCodes.ObjectDoesNotExist, $"Could not find optionset with optionset id: {retrieveOptionSetRequest.MetadataId}");
			}

            var name = retrieveOptionSetRequest.Name;

            if (string.IsNullOrEmpty(name))
            {
                FakeOrganizationServiceFault.Throw(ErrorCodes.InvalidArgument, "Name is required when optionSet id is not specified");
            }

			if (!ctx.OptionSetValuesMetadata.ContainsKey(name))
            {
				FakeOrganizationServiceFault.Throw(ErrorCodes.ObjectDoesNotExist, string.Format("An OptionSetMetadata with the name {0} does not exist.", name));
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