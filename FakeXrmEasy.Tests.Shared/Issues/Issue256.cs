#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace FakeXrmEasy.Tests.Issues
{
    public class Issue256
    {
        [Fact]
        public void TestSetup_LeftOuterJoinWithConditions()
        {
            var contactWithAccountFive = new Entity
            {
                LogicalName = "contact",
                Id = Guid.NewGuid(),
            };

            var contactWithAccountTen = new Entity
            {
                LogicalName = "contact",
                Id = Guid.NewGuid(),
            };

            var accountFive = new Entity
            {
                LogicalName = "account",
                Id = Guid.NewGuid(),
            };
            accountFive["primarycontactid"] = contactWithAccountFive.ToEntityReference();
            accountFive["accountnumber"] = "5";

            var accountTen = new Entity
            {
                LogicalName = "account",
                Id = Guid.NewGuid(),
            };
            accountTen["primarycontactid"] = contactWithAccountTen.ToEntityReference();
            accountTen["accountnumber"] = "10";

            // Find all contacts
            var query = new QueryExpression
            {
                EntityName = "contact",
                ColumnSet = new ColumnSet(true),
            };

            var context = new XrmFakedContext();
            context.Initialize(new List<Entity> { contactWithAccountFive, accountFive, contactWithAccountTen, accountTen });

            // Link in the accounts
            var accountLink = new LinkEntity
            {
                LinkFromEntityName = "contact",
                LinkFromAttributeName = "contactid",

                LinkToEntityName = "account",
                LinkToAttributeName = "primarycontactid",

                JoinOperator = JoinOperator.LeftOuter,
                Columns = new ColumnSet(true),
                EntityAlias = "Account",
            };

            // Only link to account 5
            accountLink.LinkCriteria.AddCondition(new ConditionExpression("accountnumber", ConditionOperator.Equal, "5"));
            query.LinkEntities.Add(accountLink);

            
            var outerJoinContacts = context.GetOrganizationService().RetrieveMultiple(query);

            // Should return our 2 contacts as it was an outer join. Instead it only returns the one contact with account 5.
            Assert.Equal(2, outerJoinContacts.Entities.Count);

            // Now we'll only return contacts with no linked acccounts. This should return only our contactWithAccountTen
            query.Criteria.AddCondition(new ConditionExpression("Account", "accountid", ConditionOperator.Null));


            var outerJoinContactsWithAccountIdNull = context.GetOrganizationService().RetrieveMultiple(query);

            // Should return our 1 contact who was not linked with account 5, instead it returns nothing
            Assert.Equal(1, outerJoinContactsWithAccountIdNull.Entities.Count);
        }
    }
}
#endif