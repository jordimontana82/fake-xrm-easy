using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using System.Reflection;
using Crm;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestCreate
    {
        [Fact]
        public void When_a_null_entity_is_created_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Create(null));
            Assert.Equal(ex.Message, "The entity must not be null");
        }

        [Fact]
        public void When_an_entity_is_created_with_a_non_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("account") { Id = Guid.NewGuid() };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Create(e));
            Assert.Equal(ex.Message, "The Id property must not be initialized");
        }

        [Fact]
        public void When_an_entity_is_created_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("") { Id = Guid.Empty };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Create(e));
            Assert.Equal(ex.Message, "The LogicalName property must not be empty");
        }

        [Fact]
        public void When_adding_an_entity_the_returned_guid_must_not_be_empty_and_the_context_should_have_it()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            
            var e = new Entity("account") { Id = Guid.Empty };
            var guid = service.Create(e);

            Assert.True(guid != Guid.Empty);
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 1);
        }

        [Fact]
        public void When_Querying_Using_LinQ_Results_Should_Appear()
        {
            var context = new XrmFakedContext();

            var account = new Account
            {
                Id = Guid.NewGuid()
            };

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Attributes = new AttributeCollection
                {
                    { "accountid", account.ToEntityReference() }
                }
            };

            context.Initialize(new Entity[] { account, contact });

            var contactResult = context.CreateQuery<Contact>().SingleOrDefault(con => con.Id == contact.Id);
            Assert.NotNull(contactResult);
        }




    }
}
