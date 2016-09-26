using System;
using System.Collections.Generic;
using System.Text;
using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveTests
{
    public class RetrieveRequestTests
    {
        [Fact]
        public void Should_Not_Fail_On_Retrieving_Entity_With_Entity_Collection_Attributes()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetFakedOrganizationService();

            var party = new ActivityParty
            {
                PartyId = new EntityReference("systemuser", Guid.NewGuid())
            };

            var email = new Email
            {
                Id = Guid.NewGuid(),
                To = new[] { party }
            };

            service.Create(email);

            Assert.DoesNotThrow(() => service.Retrieve(email.LogicalName, email.Id, new ColumnSet(true)));
        }
    }
}
