using Crm;
using FakeItEasy;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
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
            var service = context.GetOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(null));
            Assert.Equal(ex.Message, "The entity must not be null");
        }

        [Fact]
        public void When_an_entity_is_updated_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();
            context.Initialize(new Entity("account") { Id = Guid.NewGuid() });

            var e = new Entity("account") { Id = Guid.Empty };

            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Update(e));
            Assert.Equal("account with Id 00000000-0000-0000-0000-000000000000 Does Not Exist", ex.Message);
        }

        [Fact]
        public void When_an_entity_is_updated_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var e = new Entity("") { Id = Guid.NewGuid() };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(e));
            Assert.Equal("The entity logical name must not be null or empty.", ex.Message);
        }

        [Fact]
        public void When_an_entity_is_updated_with_a_null_attribute_the_attribute_is_removed()
        {
            var context = new XrmFakedContext();
            var entity = new Account { Id = Guid.NewGuid() };
            entity.DoNotEMail = true;
            context.Initialize(entity);

            var update = new Account() { Id = entity.Id };
            update.DoNotEMail = null;

            var service = context.GetOrganizationService();
            service.Update(update);

            var updatedEntityAllAttributes = service.Retrieve(Account.EntityLogicalName, update.Id, new ColumnSet(true));
            var updatedEntityAllAttributesEarlyBound = updatedEntityAllAttributes.ToEntity<Account>();

            var updatedEntitySingleAttribute = service.Retrieve(Account.EntityLogicalName, update.Id, new ColumnSet(new string[] { "donotemail" }));
            var updatedEntitySingleAttributeEarlyBound = updatedEntityAllAttributes.ToEntity<Account>();

            Assert.Null(updatedEntityAllAttributesEarlyBound.DoNotEMail);
            Assert.False(updatedEntityAllAttributes.Attributes.ContainsKey("donotemail"));

            Assert.Null(updatedEntitySingleAttributeEarlyBound.DoNotEMail);
            Assert.False(updatedEntitySingleAttribute.Attributes.ContainsKey("donotemail"));
        }

        [Fact]
        public void When_updating_an_entity_the_context_should_reflect_changes()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

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

#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
        [Fact]
        public void When_updating_an_entity_by_alternate_key_the_context_should_reflect_changes()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var accountMetadata = new Microsoft.Xrm.Sdk.Metadata.EntityMetadata();
            accountMetadata.LogicalName = Account.EntityLogicalName;
            var alternateKeyMetadata = new Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata();
            alternateKeyMetadata.KeyAttributes = new string[] { "AccountNumber" };
            accountMetadata.SetFieldValue("_keys", new Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata[]
                 {
                 alternateKeyMetadata
                 });
            context.InitializeMetadata(accountMetadata);

            var e = new Entity("account");
            e["AccountNumber"] = 9000;
            e["name"] = "Before update";
            var guid = service.Create(e);

            Assert.Equal(context.Data["account"][guid]["name"], "Before update");

            //now update the name
            e = new Entity("account", "AccountNumber", 9000);
            e["name"] = "After update";
            service.Update(e);

            Assert.Equal(context.Data["account"][guid]["name"], "After update");
        }
#endif

#if FAKE_XRM_EASY_9
        [Fact]
        public void When_updating_an_optionsetvaluecollection_the_context_should_reflect_changes()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var e = new Entity("contact") { Id = Guid.Empty };
            e["new_multiselectattribute"] = new OptionSetValueCollection() { new OptionSetValue(1) };
            var guid = service.Create(e);

            Assert.Equal(context.Data["contact"][guid]["new_multiselectattribute"], new OptionSetValueCollection() { new OptionSetValue(1) });

            //now update the name
            e = new Entity("contact") { Id = guid };
            e["new_multiselectattribute"] = new OptionSetValueCollection() { new OptionSetValue(2), new OptionSetValue(3) };
            service.Update(e);

            Assert.Equal(context.Data["contact"][guid]["new_multiselectattribute"], new OptionSetValueCollection() { new OptionSetValue(2), new OptionSetValue(3) });
        }
#endif

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

            var service = context.GetOrganizationService();
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

            var service = context.GetOrganizationService();

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

            var service = context.GetOrganizationService();

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

            var service = context.GetOrganizationService();


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

            var service = context.GetOrganizationService();

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
        public void Should_Not_Raise_An_Exception_When_Updating_Status()
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
                        { "statecode", 0 }  //0 = Active, anything else: Inactive
                    }
                }
            });

            var accountToUpdate = new Account()
            {
                Id = entityId,
                Name = "FC Barcelona",
                ["statecode"] = new OptionSetValue(1)
            };

            service.Update(accountToUpdate);
            var updatedAccount = context.CreateQuery<Account>().FirstOrDefault();
            Assert.Equal(1, (int)updatedAccount.StateCode.Value);
        }

        [Fact]
        public void Should_Return_Updated_EntityReference_Name()
        {
            var userMetadata = new EntityMetadata() { LogicalName = "systemuser" };
            userMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");

            var user = new Entity() { LogicalName = "systemuser", Id = Guid.NewGuid() };
            user["fullname"] = "Fake XrmEasy";

            var context = new XrmFakedContext();
            context.InitializeMetadata(userMetadata);
            context.Initialize(user);

            context.CallerId = user.ToEntityReference();

            var account = new Entity() { LogicalName = "account" };

            var service = context.GetOrganizationService();
            var accountId = service.Create(account);

            user["fullname"] = "Good Job";
            service.Update(user);

            account = service.Retrieve("account", accountId, new ColumnSet(true));

            Assert.Equal("Good Job", account.GetAttributeValue<EntityReference>("ownerid").Name);
        }
    }
}