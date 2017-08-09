#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class PickFromQueueRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is PickFromQueueRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var pickFromQueueRequest = (PickFromQueueRequest)request;

            var queueItemId = pickFromQueueRequest.QueueItemId;
            var workerid = pickFromQueueRequest.WorkerId;

            if ((queueItemId == Guid.Empty) || (workerid == Guid.Empty))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Expected non-empty Guid.");
            }

            var service = ctx.GetOrganizationService();

            var query = new QueryByAttribute("systemuser");
            query.Attributes.Add("systemuserid");
            query.Values.Add(workerid);

            var worker = service.RetrieveMultiple(query).Entities.FirstOrDefault();
            if (worker == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("Invalid workerid: {0} of type 8", workerid));
            }

            query = new QueryByAttribute("queueitem");
            query.Attributes.Add("queueitemid");
            query.Values.Add(queueItemId);

            var queueItem = service.RetrieveMultiple(query).Entities.FirstOrDefault();
            if (queueItem == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("queueitem With Id = {0} Does Not Exist", queueItemId));
            }

            if (pickFromQueueRequest.RemoveQueueItem)
            {
                service.Delete("queueitem", queueItemId);
            }
            else
            {
                var pickUpdateEntity = new Entity
                {
                    LogicalName = "queueitem",
                    Id = queueItem.Id,
                    Attributes = new AttributeCollection
                    {
                        { "workerid", worker.ToEntityReference() },
                        { "workeridmodifiedon", DateTime.Now },
                    }
                };

                service.Update(pickUpdateEntity);
            }

            return new PickFromQueueResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(PickFromQueueRequest);
        }
    }
}

#endif