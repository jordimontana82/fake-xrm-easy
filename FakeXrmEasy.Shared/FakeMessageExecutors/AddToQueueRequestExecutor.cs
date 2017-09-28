using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
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

            if (target == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to queue without target");
            }

            if (destinationQueueId == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to queue without destination queue");
            }

            var service = ctx.GetOrganizationService();

            var createQueueItem = new Entity
            {
                LogicalName = "queueitem",
                Attributes = new AttributeCollection
                {
                    { "queueid", new EntityReference("queue", destinationQueueId) },
                    { "objectid", target }
                }
            };

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