using System;
using System.ServiceModel;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class BulkDeleteRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is BulkDeleteRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var bulkDeleteRequest = (BulkDeleteRequest)request;
           
            if (string.IsNullOrEmpty(bulkDeleteRequest.JobName))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not Bulk delete without JobName");
            }
            if (bulkDeleteRequest.QuerySet == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not Bulk delete without QuerySet");
            }
            if (bulkDeleteRequest.CCRecipients == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not Bulk delete without CCRecipients");
            }
            if (bulkDeleteRequest.ToRecipients == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not Bulk delete without ToRecipients");
            }

            var service = ctx.GetOrganizationService();

            foreach (QueryExpression queryExpression in bulkDeleteRequest.QuerySet)
            {
                EntityCollection recordsToDelete = service.RetrieveMultiple(queryExpression);
                foreach (Entity record in recordsToDelete.Entities)
                {
                    service.Delete(record.LogicalName, record.Id);
                }
            }
            
            return new BulkDeleteResponse()
            {
                ResponseName = "BulkDeleteResponse"
            };
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(BulkDeleteRequest);
        }
    }
}
