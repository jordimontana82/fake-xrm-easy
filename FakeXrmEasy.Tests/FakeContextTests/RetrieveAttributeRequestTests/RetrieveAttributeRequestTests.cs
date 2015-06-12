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

            var response = service.Execute(req) as RetrieveAttributeResponse;
            Assert.True(true);
        }
    }
}
