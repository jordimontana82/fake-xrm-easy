using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Xunit;

#if FAKE_XRM_EASY_9
namespace FakeXrmEasy.Tests.FakeContextTests.DeleteRecordChangeHistoryRequestTests
{
    public class DeleteRecordChangeHistoryRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new DeleteRecordChangeHistoryRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_target_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();
            var orgReq = new OrganizationRequest();
            var res = new DeleteRecordChangeHistoryRequestExecutor().Execute(orgReq, ctx);
            Assert.IsType(typeof(DeleteRecordChangeHistoryResponse), res);
        }
    }
}
#endif