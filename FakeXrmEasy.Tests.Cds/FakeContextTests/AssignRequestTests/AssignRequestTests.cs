using System;
using System.Collections.Generic;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;
using Xunit;
using Crm;

namespace FakeXrmEasy.Tests.FakeContextTests.AssignRequestTests
{
    public class AssignRequestTests
    {
        [Fact]
        public void When_Assigned_Team_As_Owner_OwningTeam_Is_Set()
        {
            var context = new XrmFakedContext();

            var user1 = new SystemUser { Id = Guid.NewGuid(), FirstName = "User1" };
            var team1 = new Team { Id = Guid.NewGuid(), Name = "Team1" };
            var account1 = new Account { Id = Guid.NewGuid(), Name = "Acc1" };

            context.Initialize(new List<Entity> {
                user1, team1, account1
            });

            var executor = new AssignRequestExecutor();
            AssignRequest req = new AssignRequest() { Target = account1.ToEntityReference(), Assignee = team1.ToEntityReference() };
            executor.Execute(req, context);

            var acc_Fresh = context.GetOrganizationService().Retrieve(account1.LogicalName, account1.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            Assert.Equal(team1.Id, acc_Fresh.GetAttributeValue<EntityReference>("owningteam").Id);
            Assert.Null(acc_Fresh.GetAttributeValue<EntityReference>("owninguser"));
        }

        [Fact]
        public void When_Assigned_User_As_Owner_OwningUser_Is_Set()
        {
            var context = new XrmFakedContext();

            var user1 = new SystemUser { Id = Guid.NewGuid(), FirstName = "User1" };
            var team1 = new Team { Id = Guid.NewGuid(), Name = "Team1" };
            var account1 = new Account { Id = Guid.NewGuid(), Name = "Acc1" };

            context.Initialize(new List<Entity> {
                user1, team1, account1
            });

            var executor = new AssignRequestExecutor();
            AssignRequest req = new AssignRequest() { Target = account1.ToEntityReference(), Assignee = user1.ToEntityReference() };
            executor.Execute(req, context);

            var acc_Fresh = context.GetOrganizationService().Retrieve(account1.LogicalName, account1.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            Assert.Equal(user1.Id, acc_Fresh.GetAttributeValue<EntityReference>("owninguser").Id);
            Assert.Null(acc_Fresh.GetAttributeValue<EntityReference>("owningteam"));
        }

        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new AssignRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_target_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new AssignRequestExecutor();
            AssignRequest req = new AssignRequest() { Target = null };
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(req, context));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_assignee_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new AssignRequestExecutor();
            AssignRequest req = new AssignRequest() { Target = new EntityReference(), Assignee = null };
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(req, context));
        }
    }
}