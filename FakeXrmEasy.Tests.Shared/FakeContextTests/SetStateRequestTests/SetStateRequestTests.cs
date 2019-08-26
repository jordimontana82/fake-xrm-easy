using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.SetStateRequestTests
{
    public class SetStateRequestTests
    {
        [Fact]
        public void When_set_state_request_is_called_an_entity_is_updated()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var c = new Contact()
            {
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

            Assert.Equal((int)contact.StateCode.Value, 69);
            Assert.Equal((int)contact.StatusCode.Value, 6969);
        }

        [Fact]
        public void Should_set_a_statecode_by_default_when_an_entity_record_is_added_to_the_context()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var c = new Contact()
            {
                Id = Guid.NewGuid()
            };
            context.Initialize(new[] { c });

            //Retrieve record after update
            var contact = (from con in context.CreateQuery<Contact>()
                           where con.Id == c.Id
                           select con).FirstOrDefault();

            Assert.Equal((int)contact.StateCode.Value, 0); //Active
        }

        [Fact]
        public void Should_not_override_a_statecode_already_initialized()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var c = new Contact()
            {
                Id = Guid.NewGuid(),
            };

            c["statecode"] = new OptionSetValue(69); //As the StateCode is read only in the early bound entity, this is the only way of updating it

            context.Initialize(new[] { c });

            //Retrieve record after update
            var contact = (from con in context.CreateQuery<Contact>()
                           where con.Id == c.Id
                           select con).FirstOrDefault();

            Assert.Equal((int)contact.StateCode.Value, 69); //Set
        }
    }
}