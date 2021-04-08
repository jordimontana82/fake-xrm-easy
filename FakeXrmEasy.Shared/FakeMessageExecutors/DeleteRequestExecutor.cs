using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
#if !FAKE_XRM_EASY_DOTNETCORE
using System.ServiceModel;
#else
using FakeXrmEasy.DotNetCore;
#endif

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

            var targetId = ctx.GetRecordUniqueId(target);

            var service = ctx.GetOrganizationService();
            service.Delete(target.LogicalName, targetId);

            return new DeleteResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(DeleteRequest);
        }
    }
}