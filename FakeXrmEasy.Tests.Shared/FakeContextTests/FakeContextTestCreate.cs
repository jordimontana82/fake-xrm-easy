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
using Microsoft.Xrm.Sdk.Client;

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
        public void When_Creating_Without_Id_It_should_Be_set_Automatically()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var account = new Account
            {
                Name = "TestAcc"
            };

            account.Id = service.Create(account);

            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact]
        public void When_Creating_With_Id_It_should_Be_set()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();
            var accId = Guid.NewGuid();

            var account = new Account
            {
                Name = "TestAcc",
                Id = accId
            };

            var createdId = service.Create(account);

            Assert.Equal(accId, createdId);
        }

        [Fact]
        public void When_Creating_With_Already_Existing_Id_Exception_Should_Be_Thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();
            var accId = Guid.NewGuid();

            var account = new Account
            {
                Name = "TestAcc",
                Id = accId
            };
            service.Create(account);

            Assert.Throws<InvalidOperationException>(() => service.Create(account));
        }

        [Fact]
        public void When_Creating_Using_Organization_Context_Record_Should_Be_Created()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var account = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };

            var service = context.GetFakedOrganizationService();

            using (var ctx = new OrganizationServiceContext(service))
            {
                ctx.AddObject(account);
                ctx.SaveChanges();
            }

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account.Id, new ColumnSet(true)));
        }

        [Fact]
        public void When_Creating_Using_Organization_Context_Without_Saving_Changes_Record_Should_Not_Be_Created()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var account = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };

            var service = context.GetFakedOrganizationService();

            using (var ctx = new OrganizationServiceContext(service))
            {
                ctx.AddObject(account);

                var retrievedAccount = ctx.CreateQuery<Account>().SingleOrDefault(acc => acc.Id == account.Id);
                Assert.Null(retrievedAccount);
            }
        }

        [Fact]
        public void When_creating_a_record_using_early_bound_entities_primary_key_should_be_populated()
        {
            var context = new XrmFakedContext();

            var c = new Contact();

            IOrganizationService service = context.GetFakedOrganizationService();
            var id = service.Create(c);

            //Retrieve the record created
            var contact = (from con in context.CreateQuery<Contact>()
                          select con).FirstOrDefault();

            Assert.True(contact.Attributes.ContainsKey("contactid"));
            Assert.Equal(id, contact["contactid"]);
        }

        [Fact]
        public void When_creating_a_record_using_dynamic_entities_primary_key_should_be_populated()
        {
            var context = new XrmFakedContext();

            Entity e = new Entity("new_myentity");

            IOrganizationService service = context.GetFakedOrganizationService();
            var id = service.Create(e);

            //Retrieve the record created
            var record = (from r in context.CreateQuery("new_myentity")
                          select r).FirstOrDefault();

            Assert.True(record.Attributes.ContainsKey("new_myentityid"));
            Assert.Equal(id, record["new_myentityid"]);

        }
    }
}
