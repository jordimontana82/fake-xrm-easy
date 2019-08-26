using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests
{
    public class ConditionExpressionTests
    {
        [Fact]
        public void When_executing_a_query_expression_with_a_not_implemented_operator_pull_request_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.LastXFiscalPeriods, "Contact 1");
            qe.Criteria.AddCondition(condition);

            Assert.Throws<PullRequestException>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList());
        }

        [Fact]
        public void When_executing_a_query_expression_with_equals_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 1");
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void When_executing_a_query_expression_with_in_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "McDonald"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "King"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "King"; contact2["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.In, new string[] { "McDonald", "King" });
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 2);
        }


        [Fact]
        public void When_executing_a_query_expression_with_null_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "1 Contact";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = null;
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() };

            context.Initialize(new List<Entity>() { contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.Null);
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void When_executing_a_query_expression_with_a_not_null_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "1 Contact";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = null;
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() };

            context.Initialize(new List<Entity>() { contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.NotNull);
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void When_executing_a_query_expression_with_a_null_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "1 Contact";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = null;
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() };

            context.Initialize(new List<Entity>() { contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.Null);
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 2);
        }


        [Fact]
        public void When_executing_a_query_expression_equals_operator_is_case_insensitive()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();
            service.Create(new Contact { FirstName = "Jimmy" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.Equal, "jimmy");

            Assert.Equal(1, service.RetrieveMultiple(qe).Entities.Count);
        }


        [Fact]
        public void When_executing_a_query_expression_attributes_returned_are_case_sensitive()
        {
            //So Where clauses shouldn't affect the Select clause
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();
            service.Create(new Contact { FirstName = "JimmY" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.EndsWith, "y");
            qe.ColumnSet = new ColumnSet(true);

            var entities = service.RetrieveMultiple(qe).Entities;
            Assert.Equal(1, entities.Count);
            Assert.Equal("JimmY", entities[0]["firstname"]);
        }

        [Fact]
        public void When_executing_a_query_expression_with_null_operator_and_early_bound_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var account1 = new Account() { Id = Guid.NewGuid(), Name = "1 Test" };
            var account2 = new Account() { Id = Guid.NewGuid(), Name = "2 Test" };
            var account3 = new Account() { Id = Guid.NewGuid(), Name = "3 Test" };
            var account4 = new Account() { Id = Guid.NewGuid(), Name = "4 Test" };
            var account5 = new Account() { Id = Guid.NewGuid(), Name = "5 Test" };
            var account6 = new Account() { Id = Guid.NewGuid(), Name = "6 Test" };
            var account7 = new Account() { Id = Guid.NewGuid() };
            var account8 = new Account() { Id = Guid.NewGuid(), Name = null };
            var account9 = new Account() { Id = Guid.NewGuid(), Name = "Another name" };

            List<Account> initialAccs = new List<Account>() {
                account1, account2, account3, account4, account5, account6, account7, account8, account9
            };

            context.Initialize(initialAccs);

            QueryExpression query = new QueryExpression()
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression("name", ConditionOperator.Null)
                    }
                }
            };

            EntityCollection ec = service.RetrieveMultiple(query);
            Assert.True(ec.Entities.Count == 2);
        }

#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9

        [Fact]
        public void ConditionExpression_Test()
        {
            var firstContact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith",
                EMailAddress1 = "SmiJo@witness.co.uk"
            };

            var secondContact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mary",
                LastName = "Bloody"
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.Initialize(new Entity[] { firstContact, secondContact });

            var query = new QueryExpression()
            {
                EntityName = Contact.EntityLogicalName,
                Criteria = new FilterExpression()
                {
                    Conditions = {
                        new ConditionExpression(Contact.EntityLogicalName /* without entityname test passes */, "firstname", ConditionOperator.Equal, "John")
                    }
                }
            };

            var result = fakedService.RetrieveMultiple(query).Entities;

            Assert.Equal(1, result.Count());
        }

#endif
    }
}