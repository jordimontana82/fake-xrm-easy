using Crm;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestUpdate
    {
        [Fact]
        public void When_a_null_entity_is_updated_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(null));
            Assert.Equal(ex.Message, "The entity must not be null");
        }

        [Fact]
        public void When_an_entity_is_updated_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("account") { Id = Guid.Empty };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(e));
            Assert.Equal(ex.Message, "The Id property must not be empty");
        }

        [Fact]
        public void When_an_entity_is_updated_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("") { Id = Guid.NewGuid() };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(e));
            Assert.Equal(ex.Message, "The LogicalName property must not be empty");
        }

        [Fact]
        public void When_updating_an_entity_the_context_should_reflect_changes()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("account") { Id = Guid.Empty };
            e["name"] = "Before update";
            var guid = service.Create(e);

            Assert.Equal(context.Data["account"][guid]["name"], "Before update");

            //now update the name
            e = new Entity("account") { Id = guid };
            e["name"] = "After update";
            service.Update(e);

            Assert.Equal(context.Data["account"][guid]["name"], "After update");
        }

        [Fact]
        public void When_update_is_invoked_with_non_existing_entity_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var nonExistingGuid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            var service = context.GetFakedOrganizationService();
            var update = new Entity("account") { Id = nonExistingGuid };
            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Update(update));

            Assert.Equal(ex.Message, string.Format("account with Id {0} Does Not Exist", nonExistingGuid));
        }

        [Fact]
        public void When_updating_an_entity_an_unchanged_attribute_remains_the_same()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var existingAccount = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };
            context.Initialize(new List<Entity>()
            {
                existingAccount
            });

            var service = context.GetFakedOrganizationService();

            //Create a new entity class to update the name
            var accountToUpdate = new Account() { Id = existingAccount.Id };
            accountToUpdate.Name = "Super Great Customer Name Updated!";

            //Update the entity in the context
            service.Update(accountToUpdate);

            //Make sure existing entity still maintains AccountNumber property
            var account = context.CreateQuery<Account>().FirstOrDefault();
            Assert.Equal(account.AccountNumber, "69");
        }

        [Fact]
        public void When_updating_an_entity_only_one_entity_is_updated()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var existingAccount = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };
            var otherExistingAccount = new Account() { Id = Guid.NewGuid(), Name = "Devil Customer", AccountNumber = "666" };

            context.Initialize(new List<Entity>()
            {
                existingAccount, otherExistingAccount
            });

            var service = context.GetFakedOrganizationService();

            //Create a new entity class to update the first account
            var accountToUpdate = new Account() { Id = existingAccount.Id };
            accountToUpdate.Name = "Super Great Customer Name Updated!";

            //Update the entity in the context
            service.Update(accountToUpdate);

            //Make other account wasn't updated
            var account = context.CreateQuery<Account>().Where(e => e.Id == otherExistingAccount.Id).FirstOrDefault();
            Assert.Equal(account.Name, "Devil Customer");
        }

        [Fact]
        public void When_updating_an_entity_using_organization_context_changes_should_be_saved()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var existingAccount = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };

            context.Initialize(new List<Entity>()
            {
                existingAccount
            });

            var service = context.GetFakedOrganizationService();

            using (var ctx = new OrganizationServiceContext(service))
            {
                existingAccount.Name = "Super Great Customer Name Updated!";

                ctx.Attach(existingAccount);
                ctx.UpdateObject(existingAccount);
                ctx.SaveChanges();
            }

            //Make other account wasn't updated
            var account = context.CreateQuery<Account>().Where(e => e.Id == existingAccount.Id).FirstOrDefault();
            Assert.Equal(account.Name, "Super Great Customer Name Updated!");
        }

        [Fact]
        public void When_updating_a_not_existing_entity_using_organization_context_exception_should_be_thrown()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var existingAccount = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };

            var service = context.GetFakedOrganizationService();

            using (var ctx = new OrganizationServiceContext(service))
            {
                existingAccount.Name = "Super Great Customer Name Updated!";

                ctx.Attach(existingAccount);
                ctx.UpdateObject(existingAccount);
                Assert.Throws<SaveChangesException>(() => ctx.SaveChanges());
            }
        }

        [Fact]
        public void Should_Not_Change_Context_Objects_Without_Update()
        {
            var entityId = Guid.NewGuid();
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            context.Initialize(new[] {
                new Entity ("account")
                {
                    Id = entityId,
                    Attributes = new AttributeCollection
                    {
                        { "accountname", "Adventure Works" }
                    }
                }
            });

            var firstRetrieve = service.Retrieve("account", entityId, new ColumnSet(true));
            var secondRetrieve = service.Retrieve("account", entityId, new ColumnSet(true));

            firstRetrieve["accountname"] = "Updated locally";

            Assert.Equal("Updated locally", firstRetrieve["accountname"]);
            Assert.Equal("Adventure Works", secondRetrieve["accountname"]);
        }

        [Fact]
        public void Should_Not_Change_Context_Early_Bound_Objects_Without_Update()
        {
            var entityId = Guid.NewGuid();
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            context.Initialize(new[] {
                new Account()
                {
                    Id = entityId,
                    Attributes = new AttributeCollection
                    {
                        { "accountname", "Adventure Works" }
                    }
                }
            });

            var firstRetrieve = service.Retrieve("account", entityId, new ColumnSet(true));
            var secondRetrieve = service.Retrieve("account", entityId, new ColumnSet(true));

            firstRetrieve["accountname"] = "Updated locally";

            Assert.Equal("Updated locally", firstRetrieve["accountname"]);
            Assert.Equal("Adventure Works", secondRetrieve["accountname"]);
        }

        [Fact]
        public void Should_Not_Change_Context_Objects_Without_Update_And_Retrieve_Multiple()
        {
            var entityId = Guid.NewGuid();
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            context.Initialize(new[] {
                new Account
                {
                    Id = entityId,
                    Name = "Adventure Works"
                }
            });

            Account firstRetrieve, secondRetrieve = null;
            using (var ctx = new XrmServiceContext(service))
            {
                firstRetrieve = ctx.CreateQuery<Account>()
                                    .Where(a => a.AccountId == entityId)
                                    .FirstOrDefault();
            }

            using (var ctx = new XrmServiceContext(service))
            {
                secondRetrieve = ctx.CreateQuery<Account>()
                                    .Where(a => a.AccountId == entityId)
                                    .FirstOrDefault();
            }

            firstRetrieve.Name = "Updated locally";

            Assert.False(firstRetrieve == secondRetrieve);
            Assert.Equal("Updated locally", firstRetrieve.Name);
            Assert.Equal("Adventure Works", secondRetrieve.Name);
        }

        [Fact]
        public void Should_Raise_An_Exception_When_Updating_An_Inactive_Record()
        {
            var entityId = Guid.NewGuid();
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            context.Initialize(new[] {
                new Account()
                {
                    Id = entityId,
                    Attributes = new AttributeCollection
                    {
                        { "statecode", 1 }  //0 = Active, anything else: Inactive
                    }
                }
            });

            var accountToUpdate = new Account() { Id = entityId, Name = "FC Barcelona" };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Update(accountToUpdate));
        }
    }
}