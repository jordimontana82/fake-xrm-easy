using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using FakeXrmEasy;
using Xunit;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestTranslateQueryExpression
    {
        [Fact]
        public void When_translating_a_null_query_expression_the_linq_query_is_also_null()
        {
            var context = new XrmFakedContext();
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, null);
            Assert.True(result == null);
        }

        [Fact]
        public void When_translating_a_query_from_a_non_existing_entity_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            IQueryable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = guid1 },
                new Entity("account") { Id = guid2 },
                new Entity("contact") { Id = guid3 }
            }.AsQueryable();

            context.Initialize(data);
            var qe = new QueryExpression() { EntityName = "nonexistingentityname"};
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe));
        }

        [Fact]
        public void When_executing_a_query_expression_with_a_simple_join_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var account1 = new Entity("account") { Id = Guid.NewGuid() };
            account1["name"] = "Account 1";
            var account2 = new Entity("account") { Id = Guid.NewGuid() };
            account2["name"] = "Account 2";
            var contact = new Entity("contact") { Id = Guid.NewGuid() };
            contact["accountid"] = new EntityReference() { Id = account1.Id, LogicalName = "account" };
            contact["fullname"] = "Contact full name";
            context.Initialize(new List<Entity>() { account1, account2, contact});

      
            var qe = new QueryExpression() { EntityName = "account" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "accountid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "fullname" })
                }
            );
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 1);
            var entityResult = result.FirstOrDefault();

            Assert.True(entityResult.Attributes.Count == 2);
            Assert.True(entityResult["name"].Equals(account1["name"]));
            Assert.True((entityResult["contact.fullname"] as AliasedValue).Value.Equals(contact["fullname"]));

        }
        [Fact]
        public void When_executing_a_query_expression_with_a_left_join_all_left_hand_side_elements_are_returned()
        {
            //2 account and 1 contact, contact is associated to Account 1 only, but the two accounts are returned
            //First row is the join of account 1 and contact attributes, whereas second row is just the account attribtues
            var context = new XrmFakedContext();
            var account1 = new Entity("account") { Id = Guid.NewGuid() };
            account1["name"] = "Account 1";
            var account2 = new Entity("account") { Id = Guid.NewGuid() };
            account2["name"] = "Account 2";
            var contact = new Entity("contact") { Id = Guid.NewGuid() };
            contact["accountid"] = new EntityReference() { Id = account1.Id, LogicalName = "account" };
            contact["fullname"] = "Contact full name";
            context.Initialize(new List<Entity>() { account1, account2, contact });

            var qe = new QueryExpression() { EntityName = "account" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "accountid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.LeftOuter,
                    Columns = new ColumnSet(new string[] { "fullname" })
                }
            );
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var entityResult = result.FirstOrDefault(); //First account

            Assert.True(entityResult.Attributes.Count == 2);
            Assert.True(entityResult["name"].Equals(account1["name"]));
            Assert.True((entityResult["contact.fullname"] as AliasedValue).Value.Equals(contact["fullname"]));

            var lastEntity = result.LastOrDefault(); // Second account
            Assert.True(lastEntity["name"].Equals(account2["name"]));
        }
    }
}
