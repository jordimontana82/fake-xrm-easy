using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class DeleteRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is DeleteRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var deleteRequest = (DeleteRequest)request;

            var target = deleteRequest.Target;

            if (target == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not delete without target");
            }

            var service = ctx.GetFakedOrganizationService();
            service.Delete(target.LogicalName, target.Id);

            return new DeleteResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(DeleteRequest);
        }
    }
}
