using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.AddToQueueRequestTests
{
    public class AddToQueueRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new AddToQueueRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_a_request_is_called_New_Queueitem_Is_Created()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var email = new Entity
            {
                LogicalName = Crm.Email.EntityLogicalName,
                Id = Guid.NewGuid(),
            };

            var queue = new Entity
            {
                LogicalName = Crm.Queue.EntityLogicalName,
                Id = Guid.NewGuid(),
            };

            context.Initialize(new[]
            {
                queue, email
            });

            var executor = new AddToQueueRequestExecutor();

            var req = new AddToQueueRequest
            {
                DestinationQueueId = queue.Id,
                Target = email.ToEntityReference(),
            };

            executor.Execute(req, context);

            var queueItem = context.Data[Crm.QueueItem.EntityLogicalName].Values.Single();

            Assert.Equal(queue.ToEntityReference(), queueItem.GetAttributeValue<EntityReference>("queueid"));
            Assert.Equal(email.ToEntityReference(), queueItem.GetAttributeValue<EntityReference>("objectid"));
        }
    }
}