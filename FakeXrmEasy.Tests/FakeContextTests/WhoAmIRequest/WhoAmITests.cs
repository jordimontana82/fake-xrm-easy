using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

using Crm;
using Microsoft.Xrm.Sdk.Messages;
using System.Collections.ObjectModel;
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy.Tests.FakeContextTests.WhoAmIRequestTests
{
    public class WhoAmITests
    {
        [Fact]
        public static void When_a_who_am_i_request_is_invoked_the_caller_id_is_returned()
        {
            var context = new XrmFakedContext();
            context.CallerId = new EntityReference() { Id = Guid.NewGuid(), Name = "Super Faked User" };

            var service = context.GetFakedOrganizationService();
            WhoAmIRequest req = new WhoAmIRequest();

            var response = service.Execute(req) as WhoAmIResponse;
            Assert.Equal(response.UserId, context.CallerId.Id);
        }
    }
}
