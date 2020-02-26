using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.DeleteRequestTests
{
    public class DeleteRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new DeleteRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_target_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new DeleteRequestExecutor();
            DeleteRequest req = new DeleteRequest() { Target = null };
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(req, context));
        }
    }
}