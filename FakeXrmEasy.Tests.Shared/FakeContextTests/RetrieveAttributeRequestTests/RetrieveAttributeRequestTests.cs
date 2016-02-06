using Xunit;
using Microsoft.Xrm.Sdk.Messages;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveAttributeRequestTests
{
    public class RetrieveAttributeTests
    {
        [Fact]
        public static void When_retrieve_attribute_request_is_called_an_exception_is_not_thrown()
        {
            var context = new XrmFakedContext();
            
            var service = context.GetFakedOrganizationService();
            RetrieveAttributeRequest req = new RetrieveAttributeRequest()
            {
                EntityLogicalName = "account",
                LogicalName = "name"
            };

            Assert.Throws<PullRequestException>(() => service.Execute(req));
        }
    }
}
