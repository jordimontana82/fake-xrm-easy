using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class FakeContextMockTests
    {
        [Fact]
        public void Should_Execute_Mock_For_OrganizationRequests()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("Contact") { Id = Guid.NewGuid() };
            context.Initialize(new[] { e });
            context.AddExecutionMock<RetrieveEntityRequest>(RetrieveEntityMock);

            var inputs = new ParameterCollection
            {
                {"Target", e }
            };

            context.ExecutePluginWith<CustomMockPlugin>(inputs, new ParameterCollection(), new EntityImageCollection(), new EntityImageCollection());

            Assert.Equal("Successful", (string)e["response"]);
            Assert.DoesNotThrow(() => context.RemoveExecutionMock<RetrieveEntityRequest>());
        }

        public OrganizationResponse RetrieveEntityMock(OrganizationRequest req)
        {
            return new RetrieveEntityResponse {ResponseName = "Successful"};
        }
    }
}
