using System;
using System.Collections.Generic;
using System.Text;
using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class FakeContextTestExecute
    {
        [Fact]
        public void When_Executing_Assign_Request_New_Owner_Should_Be_Assigned()
        {
            var oldOwner = new EntityReference("systemuser", Guid.NewGuid());
            var newOwner = new EntityReference("systemuser", Guid.NewGuid());

            var account = new Account
            {
                Id = Guid.NewGuid(),
                OwnerId = oldOwner
            };

            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            context.Initialize(new [] { account });

            var assignRequest = new AssignRequest
            {
                Target = account.ToEntityReference(),
                Assignee = newOwner
            };
            service.Execute(assignRequest);

            Assert.Equal(newOwner, account.OwnerId);
        }
    }
}
