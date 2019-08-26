using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests
{
    public class OrderByTests
    {
        [Fact]
        public void When_ordering_by_money_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["lastname"] = "Bloggs";
            contact1.Attributes["new_somefield"] = new Money(12345); // (decimal)678910

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["lastname"] = "Bloggs";
            contact2.Attributes["new_somefield"] = new Money(678910); // (decimal)678910

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_somefield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = results.Entities[0]["new_somefield"] as Money;

            Assert.Equal(12345M, firstResultValue.Value);
        }

        [Fact]
        public void When_ordering_by_money_fields_descending_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["lastname"] = "Bloggs";
            contact1.Attributes["new_somefield"] = new Money(12345); // (decimal)678910

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["lastname"] = "Bloggs";
            contact2.Attributes["new_somefield"] = new Money(678910); // (decimal)678910

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_somefield", OrderType.Descending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = results.Entities[0]["new_somefield"] as Money;

            Assert.Equal(678910M, firstResultValue.Value);
        }

        [Fact]
        public void When_ordering_by_entity_reference_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["lastname"] = "Bloggs";
            contact1.Attributes["new_somefield"] = new EntityReference()
            {
                Id = Guid.NewGuid(),
                Name = "Jordi"
            };

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["lastname"] = "Bloggs";
            contact2.Attributes["new_somefield"] = new EntityReference()
            {
                Id = Guid.NewGuid(),
                Name = "Skuba"
            };

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_somefield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = results.Entities[0]["new_somefield"] as EntityReference;

            Assert.Equal("Jordi", firstResultValue.Name);
        }

        [Fact]
        public void When_ordering_by_entity_reference_fields_descending_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["lastname"] = "Bloggs";
            contact1.Attributes["new_somefield"] = new EntityReference()
            {
                Id = Guid.NewGuid(),
                Name = "Jordi"
            };

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["lastname"] = "Bloggs";
            contact2.Attributes["new_somefield"] = new EntityReference()
            {
                Id = Guid.NewGuid(),
                Name = "Skuba"
            };

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_somefield", OrderType.Descending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = results.Entities[0]["new_somefield"] as EntityReference;

            Assert.Equal("Skuba", firstResultValue.Name);
        }

        [Fact]
        public void When_ordering_by_optionsetvalue_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_optionsetfield"] = new OptionSetValue(1);

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_optionsetfield"] = new OptionSetValue(2);

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_optionsetfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = results.Entities[0]["new_optionsetfield"] as OptionSetValue;

            Assert.Equal(1, firstResultValue.Value);
        }

        [Fact]
        public void When_ordering_by_int_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = 69;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = 6969;

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (int)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(69, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_datetime_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            var now = DateTime.UtcNow;
            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = now;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = now.AddDays(1);

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (DateTime)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(now, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_guid_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            var g1 = new Guid(12, 0, 0, new byte[] {  new byte(), new byte(), new byte(), new byte(),
                                                      new byte(), new byte(), new byte(), new byte()  });
            var g2 = new Guid(24, 0, 0, new byte[] {  new byte(), new byte(), new byte(), new byte(),
                                                      new byte(), new byte(), new byte(), new byte()  });

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = g1;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = g2;

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (Guid)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(g1, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_decimal_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = 69.69m;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = 6969.69m;

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (decimal)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(69.69m, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_double_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = 69.69;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = 6969.69;

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (double)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(69.69, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_float_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = 69.69f;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = 6969.69f;

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (float)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(69.69f, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_boolean_fields_expected_result_is_returned()
        {
            List<Entity> contactList = new List<Entity>();

            Entity contact1 = new Entity("contact");
            contact1.Id = Guid.NewGuid();
            contact1.Attributes["firstname"] = "Fred";
            contact1.Attributes["new_orderbyfield"] = false;

            Entity contact2 = new Entity("contact");
            contact2.Id = Guid.NewGuid();
            contact2.Attributes["firstname"] = "Jo";
            contact2.Attributes["new_orderbyfield"] = true;

            contactList.Add(contact2);
            contactList.Add(contact1);

            var context = new XrmFakedContext();
            context.Initialize(contactList);
            var service = context.GetOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (bool)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(false, firstResultValue);
        }

        [Fact]
        public void When_ordering_by_2_columns_simultaneously_right_result_is_returned_asc_desc()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var account11 = new Account() { Id = Guid.NewGuid(), Name = "11", ImportSequenceNumber = 1, NumberOfEmployees = 1 };
            var account12 = new Account() { Id = Guid.NewGuid(), Name = "12", ImportSequenceNumber = 1, NumberOfEmployees = 2 };
            var account21 = new Account() { Id = Guid.NewGuid(), Name = "21", ImportSequenceNumber = 2, NumberOfEmployees = 1 };
            var account22 = new Account() { Id = Guid.NewGuid(), Name = "22", ImportSequenceNumber = 2, NumberOfEmployees = 2 };
            var account31 = new Account() { Id = Guid.NewGuid(), Name = "31", ImportSequenceNumber = 3, NumberOfEmployees = 1 };
            var account32 = new Account() { Id = Guid.NewGuid(), Name = "32", ImportSequenceNumber = 3, NumberOfEmployees = 2 };

            List<Account> initialAccs = new List<Account>() {
                account12, account22, account21, account32, account11, account31
            };

            ctx.Initialize(initialAccs);

            QueryExpression query = new QueryExpression()
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(true),
                Orders =
                {
                    new OrderExpression("importsequencenumber", OrderType.Ascending),
                    new OrderExpression("numberofemployees", OrderType.Descending)
                }
            };

            EntityCollection ec = service.RetrieveMultiple(query);
            var names = ec.Entities.Select(e => e.ToEntity<Account>().Name).ToList();

            Assert.True(names[0].Equals("12"), "Test 12 failed");
            Assert.True(names[1].Equals("11"), "Test 11 failed");
            Assert.True(names[2].Equals("22"), "Test 22 failed");
            Assert.True(names[3].Equals("21"), "Test 21 failed");
            Assert.True(names[4].Equals("32"), "Test 32 failed");
            Assert.True(names[5].Equals("31"), "Test 31 failed");
        }

        [Fact]
        public void When_ordering_by_2_columns_simultaneously_right_result_is_returned_asc_asc()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var account11 = new Account() { Id = Guid.NewGuid(), Name = "11", ImportSequenceNumber = 1, NumberOfEmployees = 1 };
            var account12 = new Account() { Id = Guid.NewGuid(), Name = "12", ImportSequenceNumber = 1, NumberOfEmployees = 2 };
            var account21 = new Account() { Id = Guid.NewGuid(), Name = "21", ImportSequenceNumber = 2, NumberOfEmployees = 1 };
            var account22 = new Account() { Id = Guid.NewGuid(), Name = "22", ImportSequenceNumber = 2, NumberOfEmployees = 2 };
            var account31 = new Account() { Id = Guid.NewGuid(), Name = "31", ImportSequenceNumber = 3, NumberOfEmployees = 1 };
            var account32 = new Account() { Id = Guid.NewGuid(), Name = "32", ImportSequenceNumber = 3, NumberOfEmployees = 2 };

            List<Account> initialAccs = new List<Account>() {
                account12, account22, account21, account32, account11, account31
            };

            ctx.Initialize(initialAccs);

            QueryExpression query = new QueryExpression()
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(true),
                Orders =
                {
                    new OrderExpression("importsequencenumber", OrderType.Ascending),
                    new OrderExpression("numberofemployees", OrderType.Ascending)
                }
            };

            EntityCollection ec = service.RetrieveMultiple(query);
            var names = ec.Entities.Select(e => e.ToEntity<Account>().Name).ToList();

            Assert.True(names[0].Equals("11"));
            Assert.True(names[1].Equals("12"));
            Assert.True(names[2].Equals("21"));
            Assert.True(names[3].Equals("22"));
            Assert.True(names[4].Equals("31"));
            Assert.True(names[5].Equals("32"));
        }

        [Fact]
        public void When_ordering_by_2_columns_simultaneously_right_result_is_returned_desc_desc()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var account11 = new Account() { Id = Guid.NewGuid(), Name = "11", ImportSequenceNumber = 1, NumberOfEmployees = 1 };
            var account12 = new Account() { Id = Guid.NewGuid(), Name = "12", ImportSequenceNumber = 1, NumberOfEmployees = 2 };
            var account21 = new Account() { Id = Guid.NewGuid(), Name = "21", ImportSequenceNumber = 2, NumberOfEmployees = 1 };
            var account22 = new Account() { Id = Guid.NewGuid(), Name = "22", ImportSequenceNumber = 2, NumberOfEmployees = 2 };
            var account31 = new Account() { Id = Guid.NewGuid(), Name = "31", ImportSequenceNumber = 3, NumberOfEmployees = 1 };
            var account32 = new Account() { Id = Guid.NewGuid(), Name = "32", ImportSequenceNumber = 3, NumberOfEmployees = 2 };

            List<Account> initialAccs = new List<Account>() {
                account12, account22, account21, account32, account11, account31
            };

            ctx.Initialize(initialAccs);

            QueryExpression query = new QueryExpression()
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(true),
                Orders =
                {
                    new OrderExpression("importsequencenumber", OrderType.Descending),
                    new OrderExpression("numberofemployees", OrderType.Descending)
                }
            };

            EntityCollection ec = service.RetrieveMultiple(query);
            var names = ec.Entities.Select(e => e.ToEntity<Account>().Name).ToList();

            Assert.True(names[0].Equals("32"));
            Assert.True(names[1].Equals("31"));
            Assert.True(names[2].Equals("22"));
            Assert.True(names[3].Equals("21"));
            Assert.True(names[4].Equals("12"));
            Assert.True(names[5].Equals("11"));
        }

        [Fact]
        public void When_ordering_by_2_columns_simultaneously_right_result_is_returned_desc_asc()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var account11 = new Account() { Id = Guid.NewGuid(), Name = "11", ImportSequenceNumber = 1, NumberOfEmployees = 1 };
            var account12 = new Account() { Id = Guid.NewGuid(), Name = "12", ImportSequenceNumber = 1, NumberOfEmployees = 2 };
            var account21 = new Account() { Id = Guid.NewGuid(), Name = "21", ImportSequenceNumber = 2, NumberOfEmployees = 1 };
            var account22 = new Account() { Id = Guid.NewGuid(), Name = "22", ImportSequenceNumber = 2, NumberOfEmployees = 2 };
            var account31 = new Account() { Id = Guid.NewGuid(), Name = "31", ImportSequenceNumber = 3, NumberOfEmployees = 1 };
            var account32 = new Account() { Id = Guid.NewGuid(), Name = "32", ImportSequenceNumber = 3, NumberOfEmployees = 2 };

            List<Account> initialAccs = new List<Account>() {
                account12, account22, account21, account32, account11, account31
            };

            ctx.Initialize(initialAccs);

            QueryExpression query = new QueryExpression()
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(true),
                Orders =
                {
                    new OrderExpression("importsequencenumber", OrderType.Descending),
                    new OrderExpression("numberofemployees", OrderType.Ascending)
                }
            };

            EntityCollection ec = service.RetrieveMultiple(query);
            var names = ec.Entities.Select(e => e.ToEntity<Account>().Name).ToList();

            Assert.True(names[0].Equals("31"));
            Assert.True(names[1].Equals("32"));
            Assert.True(names[2].Equals("21"));
            Assert.True(names[3].Equals("22"));
            Assert.True(names[4].Equals("11"));
            Assert.True(names[5].Equals("12"));
        }

        [Fact]
        public void When_ordering_column_is_not_in_column_set_ordering_is_still_correct()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity secondEntity = new Entity("entity");
            secondEntity.Id = Guid.NewGuid();
            secondEntity["int"] = 2;
            secondEntity["text"] = "second";
            initialEntities.Add(secondEntity);

            Entity firstEntity = new Entity("entity");
            firstEntity.Id = Guid.NewGuid();
            firstEntity["int"] = 1;
            firstEntity["text"] = "first";
            initialEntities.Add(firstEntity);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.ColumnSet = new ColumnSet("text");
            query.AddOrder("int", OrderType.Ascending);

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(firstEntity.Id, result.Entities[0].Id);
            Assert.Equal(secondEntity.Id, result.Entities[1].Id);
        }

        [Fact]
        public void When_ordering_an_entity_reference_column_with_null_names_order_is_correct()
        {
            XrmFakedContext context = new XrmFakedContext();
            var service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            var erNull = new EntityReference("account", Guid.NewGuid());
            erNull.Name = null;

            var erAnotherNull = new EntityReference("account", Guid.NewGuid());
            erAnotherNull.Name = null;

            var er1 = new EntityReference("account", Guid.NewGuid());
            er1.Name = "first";

            var er2 = new EntityReference("account", Guid.NewGuid());
            er2.Name = "second";

            Entity secondEntity = new Entity("entity");
            secondEntity.Id = Guid.NewGuid();
            secondEntity["lookup"] = er2;
            initialEntities.Add(secondEntity);

            Entity firstEntity = new Entity("entity");
            firstEntity.Id = Guid.NewGuid();
            firstEntity["lookup"] = er1;
            initialEntities.Add(firstEntity);

            Entity nullEntity = new Entity("entity");
            nullEntity.Id = Guid.NewGuid();
            nullEntity["lookup"] = erNull;
            initialEntities.Add(nullEntity);

            Entity anotherNullEntity = new Entity("entity");
            anotherNullEntity.Id = Guid.NewGuid();
            anotherNullEntity["lookup"] = erAnotherNull;
            initialEntities.Add(anotherNullEntity);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.ColumnSet = new ColumnSet("text");
            query.AddOrder("lookup", OrderType.Ascending);

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(nullEntity.Id, result.Entities[0].Id);
            Assert.Equal(anotherNullEntity.Id, result.Entities[1].Id);
            Assert.Equal(firstEntity.Id, result.Entities[2].Id);
            Assert.Equal(secondEntity.Id, result.Entities[3].Id);
        }
    }
}