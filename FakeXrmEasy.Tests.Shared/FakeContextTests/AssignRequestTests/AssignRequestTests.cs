using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.AssignRequestTests
{
    public class AssignRequestTests
    {
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