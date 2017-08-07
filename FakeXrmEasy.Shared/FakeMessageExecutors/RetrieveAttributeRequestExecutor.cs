using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveAttributeRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveAttributeRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as RetrieveAttributeRequest;

            if (string.IsNullOrWhiteSpace(req.EntityLogicalName))
            {
                throw new Exception("The EntityLogicalName property must be provided in this request");
            }

            if (string.IsNullOrWhiteSpace(req.LogicalName))
            {
                throw new Exception("The LogicalName property must be provided in this request");
            }

            var entityMetadata = ctx.GetEntityMetadataByName(req.EntityLogicalName);
            if(entityMetadata == null)
            {
                throw new Exception(string.Format("The entity metadata with logical name {0} wasn't initialized. Please use .InitializeMetadata", req.EntityLogicalName));
            }

            if(entityMetadata.Attributes == null)
            {
                throw new Exception(string.Format("The attribute {0} wasn't found in entity metadata with logical name {1}. ", req.LogicalName, req.EntityLogicalName));
            }

            var attributeMetadata = entityMetadata.Attributes
                                    .FirstOrDefault(a => a.LogicalName.Equals(req.LogicalName));

            if (attributeMetadata == null)
            {
                throw new Exception(string.Format("The attribute {0} wasn't found in entity metadata with logical name {1}. ", req.LogicalName, req.EntityLogicalName));
            }

            var response = new RetrieveAttributeResponse()
            {
                Results = new ParameterCollection
                {
                    { "AttributeMetadata", attributeMetadata }
                }
            };

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveAttributeRequest);
        }
    }
}