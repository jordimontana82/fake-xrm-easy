#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
#if FAKE_XRM_EASY_DOTNETCORE
using FakeXrmEasy.DotNetCore;
#else
using System.ServiceModel;
#endif

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RemoveFromQueueRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RemoveFromQueueRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var removeFromQueueRequest = (RemoveFromQueueRequest)request;

            var queueItemId = removeFromQueueRequest.QueueItemId;
            if (queueItemId == Guid.Empty)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Cannot remove without queue item.");
            }

            var service = ctx.GetOrganizationService();
            service.Delete("queueitem", queueItemId);

            return new RemoveFromQueueResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RemoveFromQueueRequest);
        }
    }
}
#endif