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
    }
}
