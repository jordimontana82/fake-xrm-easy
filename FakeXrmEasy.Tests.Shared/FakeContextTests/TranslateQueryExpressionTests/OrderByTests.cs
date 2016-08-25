using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (int) results.Entities[0]["new_orderbyfield"];

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

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
            var service = context.GetFakedOrganizationService();

            QueryExpression qry = new QueryExpression("contact");
            qry.ColumnSet = new ColumnSet(true);
            qry.AddOrder("new_orderbyfield", OrderType.Ascending);
            var results = service.RetrieveMultiple(qry);

            var firstResultValue = (bool)results.Entities[0]["new_orderbyfield"];

            Assert.Equal(false, firstResultValue);
        }
    }
}
