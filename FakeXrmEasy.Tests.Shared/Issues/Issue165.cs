using System;
using System.Collections.Generic;
using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue165
    {
        [Fact]
        public void TestMultipleUnaliasedJoins()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var account = new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["firstname"] = "Test"
            };

            var secondAccount = new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["firstname"] = "secondTest"
            };

            var contact = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["parent"] = account.ToEntityReference(),
                ["otherparent"] = secondAccount.ToEntityReference()
            };

            context.Initialize(new List<Entity>() { account, secondAccount, contact });

            QueryExpression query = new QueryExpression("contact");

            var firstLink = new LinkEntity("contact", "account", "parent", "accountid", JoinOperator.LeftOuter)
            {
                Columns = new ColumnSet("firstname")
            };
            query.LinkEntities.Add(firstLink);

            var secondLink = new LinkEntity("contact", "account", "otherparent", "accountid", JoinOperator.LeftOuter)
            {
                Columns = new ColumnSet("firstname")
            };
            query.LinkEntities.Add(secondLink);

            var result = service.RetrieveMultiple(query);
            Entity resultingEntity = result.Entities[0];
            Assert.Equal(2, resultingEntity.Attributes.Count);
            Assert.Equal("Test", ((AliasedValue)resultingEntity["account1.firstname"]).Value);
            Assert.Equal("secondTest", ((AliasedValue)resultingEntity["account2.firstname"]).Value);
        }
    }
}
