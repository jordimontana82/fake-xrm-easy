using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.WhoAmIRequestTests
{
    public class WhoAmITests
    {
        [Fact]
        public static void When_a_who_am_i_request_is_invoked_the_caller_id_is_returned()
        {
            var context = new XrmFakedContext();
            context.CallerId = new EntityReference() { Id = Guid.NewGuid(), Name = "Super Faked User" };

            var service = context.GetOrganizationService();
            WhoAmIRequest req = new WhoAmIRequest();

            var response = service.Execute(req) as WhoAmIResponse;
            Assert.Equal(response.UserId, context.CallerId.Id);
        }
    }
}