using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Xunit;
using Crm;
using Microsoft.Crm.Sdk.Messages;
using System.Reflection;

namespace FakeXrmEasy.Tests.FakeContextTests.SetStateRequestTests
{
    public class SetStateRequestTests
    {
        [Fact]
        public void When_set_state_request_is_called_an_entity_is_updated()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetFakedOrganizationService();

            var c = new Contact() {
                Id = Guid.NewGuid()
            }; 
            context.Initialize(new[] { c });

            var request = new SetStateRequest
            {
                EntityMoniker = c.ToEntityReference(),
                State = new OptionSetValue(69), 
                Status = new OptionSetValue(6969),
            };

            var response = service.Execute(request);

            //Retrieve record after update
            var contact = (from con in context.CreateQuery<Contact>()
                           where con.Id == c.Id
                           select con).FirstOrDefault();

            Assert.Equal((int) contact.StateCode.Value, 69);
            Assert.Equal((int) contact.StatusCode.Value, 6969);
        }
    }
}
