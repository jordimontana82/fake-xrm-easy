using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
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
            var service = context.GetOrganizationService();

            context.Initialize(new[] { account });

            var assignRequest = new AssignRequest
            {
                Target = account.ToEntityReference(),
                Assignee = newOwner
            };
            service.Execute(assignRequest);

            //retrieve account updated
            var updatedAccount = context.CreateQuery<Account>().FirstOrDefault();
            Assert.Equal(newOwner.Id, updatedAccount.OwnerId.Id);
        }
    }
}