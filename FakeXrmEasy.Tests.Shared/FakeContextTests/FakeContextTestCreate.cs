using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestCreate
    {
        [Fact]
        public void When_a_null_entity_is_created_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Create(null));
            Assert.Equal(ex.Message, "The entity must not be null");
        }

        [Fact]
        public void When_an_entity_is_created_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var e = new Entity("") { Id = Guid.Empty };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Create(e));
            Assert.Equal(ex.Message, "The LogicalName property must not be empty");
        }

        [Fact]
        public void When_adding_an_entity_the_returned_guid_must_not_be_empty_and_the_context_should_have_it()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

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
            var service = context.GetOrganizationService();

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
            var service = context.GetOrganizationService();
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
            var service = context.GetOrganizationService();
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
        public void When_Creating_With_A_StateCode_Property_Exception_Is_Thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();
            var accId = Guid.NewGuid();

            var account = new Account
            {
                Name = "TestAcc",
                Id = accId
            };
            account["statecode"] = 2;

            Assert.Throws<InvalidOperationException>(() => service.Create(account));
        }

        [Fact]
        public void When_Creating_Using_Organization_Context_Record_Should_Be_Created()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var account = new Account() { Id = Guid.NewGuid(), Name = "Super Great Customer", AccountNumber = "69" };

            var service = context.GetOrganizationService();

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

            var service = context.GetOrganizationService();

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

            IOrganizationService service = context.GetOrganizationService();
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

            IOrganizationService service = context.GetOrganizationService();
            var id = service.Create(e);

            //Retrieve the record created
            var record = (from r in context.CreateQuery("new_myentity")
                          select r).FirstOrDefault();

            Assert.True(record.Attributes.ContainsKey("new_myentityid"));
            Assert.Equal(id, record["new_myentityid"]);
        }

        [Fact]
        public void When_creating_a_record_using_early_bound_entities_and_proxytypes_primary_key_should_be_populated()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));
            var c = new Contact();
            c.Id = Guid.NewGuid();

            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(new List<Entity>() { c });

            //Retrieve the record created
            var contact = (from con in context.CreateQuery<Contact>()
                           select con).FirstOrDefault();

            Assert.True(contact.Attributes.ContainsKey("contactid"));
            Assert.Equal(c.Id, contact["contactid"]);
        }

        [Fact]
        public void When_related_entities_are_used_without_relationship_info_exception_is_raised()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var order = new SalesOrder();

            var orderItems = new EntityCollection(new List<Entity>()
            {
                new SalesOrderDetail(),
                new SalesOrderDetail()
            });

            // Add related order items so it can be created in one request
            order.RelatedEntities.Add(new Relationship
            {
                PrimaryEntityRole = EntityRole.Referenced,
                SchemaName = "order_details"
            }, orderItems);

            var request = new CreateRequest
            {
                Target = order
            };

            var exception = Record.Exception(() => service.Execute(request));

            Assert.IsType<Exception>(exception);
            Assert.Equal(exception.Message, "Relationship order_details does not exist in the metadata cache");
        }

        [Fact]
        public void When_related_entities_and_relationship_are_used_child_entities_are_created()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            ctx.AddRelationship("order_details",
                new XrmFakedRelationship()
                {
                    Entity1LogicalName = SalesOrder.EntityLogicalName,  //Referenced
                    Entity1Attribute = "salesorderid",              //Pk
                    Entity2LogicalName = SalesOrderDetail.EntityLogicalName,
                    Entity2Attribute = "salesorderid",              //Lookup attribute
                    RelationshipType = XrmFakedRelationship.enmFakeRelationshipType.OneToMany
                });

            var order = new SalesOrder();

            var orderItems = new EntityCollection(new List<Entity>()
            {
                new SalesOrderDetail(),
                new SalesOrderDetail()
            });

            // Add related order items so it can be created in one request
            order.RelatedEntities.Add(new Relationship
            {
                PrimaryEntityRole = EntityRole.Referenced,
                SchemaName = "order_details"
            }, orderItems);

            var request = new CreateRequest
            {
                Target = order
            };

            var id = (service.Execute(request) as CreateResponse).id;
            var createdOrderDetails = ctx.CreateQuery<SalesOrderDetail>().ToList();

            Assert.Equal(createdOrderDetails.Count, 2);
            Assert.Equal(createdOrderDetails[0].SalesOrderId.Id, id);
            Assert.Equal(createdOrderDetails[1].SalesOrderId.Id, id);
        }

        [Fact]
        public void Shouldnt_store_references_to_variables_but_actual_clones()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            //create an account and then retrieve it with no changes
            Entity newAccount = new Entity("account");
            newAccount["name"] = "New Account";

            newAccount.Id = service.Create(newAccount);

            Entity retrievedAccount = service.Retrieve("account", newAccount.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            Assert.True(retrievedAccount.Attributes.Contains("name"));

            //do the same as above, but this time clear the attributes - see that when retrieved, the retrieved entity does not contain the name attribute
            Entity newAccount1 = new Entity("account");
            newAccount1["name"] = "New Account1";

            newAccount1.Id = service.Create(newAccount1);
            newAccount1.Attributes.Clear();

            Entity retrievedAccount1 = service.Retrieve("account", newAccount1.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            Assert.True(retrievedAccount1.Attributes.Contains("name"));

            //third time around, change the name to something new, the retrieved entity should not reflect this change
            Entity newAccount2 = new Entity("account");
            newAccount2["name"] = "New Account2";

            newAccount2.Id = service.Create(newAccount2);
            newAccount2["name"] = "Changed name";

            Entity retrievedAccount2 = service.Retrieve("account", newAccount2.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            Assert.True(retrievedAccount2["name"].ToString() == "New Account2", $"'{retrievedAccount2["name"]}' was not the expected result");
        }

        [Fact]
        public void Shouldnt_modify_objects_passed_to_the_service() // *PLEASE_READ* This test is correct?
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));
            var account = new Account { Id = Guid.NewGuid(), Name = "Test account" };

            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(new List<Entity>() { account });

            //Retrieve the record created
            Contact c = new Contact
            {
                ParentCustomerId = account.ToEntityReference(),
                LastName = "Duck",
            };
            foreach (var name in new[] { "Huey", "Dewey", "Louie"})
            {
                c.FirstName = name;
                service.Create(c);
            }

            var createdContacts = context.CreateQuery<Contact>().ToList();

            Assert.Equal(Guid.Empty, c.Id);
            Assert.Null(c.ContactId);
            Assert.Null(c.CreatedOn);

            Assert.Equal(3, createdContacts.Count);
        }

        [Fact]
        public void When_Creating_Without_Default_Attributes_They_Should_Be_Set_By_Default()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var account = new Account
            {
                Name = "test"
            };

            service.Create(account);
            var createdAccount = context.CreateQuery<Account>().FirstOrDefault();

            Assert.True(createdAccount.Attributes.ContainsKey("createdon"));
            Assert.True(createdAccount.Attributes.ContainsKey("createdby"));
            Assert.True(createdAccount.Attributes.ContainsKey("modifiedon"));
            Assert.True(createdAccount.Attributes.ContainsKey("modifiedby"));
            Assert.True(createdAccount.Attributes.ContainsKey("statecode"));
        }

        [Fact]
        public void When_Creating_Without_Default_Attributes_They_Should_Be_Set_By_Default_With_Early_Bound()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var account = new Account
            {
                Name = "test"
            };

            service.Create(account);
            var createdAccount = context.CreateQuery<Account>().FirstOrDefault();

            Assert.True(createdAccount.Attributes.ContainsKey("createdon"));
            Assert.True(createdAccount.Attributes.ContainsKey("createdby"));
            Assert.True(createdAccount.Attributes.ContainsKey("modifiedon"));
            Assert.True(createdAccount.Attributes.ContainsKey("modifiedby"));
            Assert.True(createdAccount.Attributes.ContainsKey("statecode"));
        }

        [Fact]
        public void When_creating_a_record_overridencreatedon_should_override_created_on()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var now = DateTime.Now.Date;

            var account = new Account()
            {
                OverriddenCreatedOn = now,
                ["createdon"] = now.AddDays(-1)
            };

            service.Create(account);

            var createdAccount = ctx.CreateQuery<Account>().FirstOrDefault();
            Assert.Equal(now, createdAccount.CreatedOn);
        }
    }
}