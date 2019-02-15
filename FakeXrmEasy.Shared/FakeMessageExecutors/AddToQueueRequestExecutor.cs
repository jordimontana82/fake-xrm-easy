using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class AddToQueueRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is AddToQueueRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var addToQueueRequest = (AddToQueueRequest)request;

            var target = addToQueueRequest.Target;
            var destinationQueueId = addToQueueRequest.DestinationQueueId;
            var queueItemProperties = addToQueueRequest.QueueItemProperties;

            if (target == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to queue without target");
            }

            if (destinationQueueId == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to queue without destination queue");
            }

            var service = ctx.GetOrganizationService();

            // CRM updates existing queue item if one already exists for a given objectid
            var existingQueueItem = service.RetrieveMultiple(new QueryExpression
            {
                EntityName = "queueitem",
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                        {
                            new ConditionExpression("objectid", ConditionOperator.Equal, target.Id)
                        }
                }
            }).Entities.FirstOrDefault();

            var createQueueItem = existingQueueItem ?? new Entity
            {
                LogicalName = "queueitem",
                // QueueItemProperties are used for initializing new queueitems
                Attributes = queueItemProperties?.Attributes
            };

            createQueueItem["queueid"] = new EntityReference("queue", destinationQueueId);
            createQueueItem["objectid"] = target;

            var guid = service.Create(createQueueItem);

            return new AddToQueueResponse()
            {
                ResponseName = "AddToQueue",
                Results = new ParameterCollection { { "QueueItemId", guid } }
            };
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(AddToQueueRequest);
        }
    }
}