using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Crm;
using System.Reflection;

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
            
            context.Initialize(new List<Entity>() {  contact1, contact2 });

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
            var condition = new ConditionExpression("fullname", ConditionOperator.In, new string[] { "McDonald", "King"});
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void When_executing_a_query_expression_with_endswith_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.EndsWith, "2");
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
        }
        [Fact]
        public void When_executing_a_query_expression_with_beginswith_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "1 Contact"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "2 Contact"; contact2["firstname"] = "First 2";
            
            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.BeginsWith, "2");
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
        }
        [Fact]
        public void When_executing_a_query_expression_with_contains_operator_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "1 Contact"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "2 Contact"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Other"; contact3["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("fullname", ConditionOperator.Contains, "Contact");
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
        public void When_executing_a_query_expression_like_operator_is_case_insensitive()
        {
            var context = new XrmFakedContext { ProxyTypesAssembly = Assembly.GetExecutingAssembly() };
            var service = context.GetFakedOrganizationService();

            service.Create(new Contact { FirstName = "Jimmy" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.Like, "JIM%");

            Assert.Equal(1, service.RetrieveMultiple(qe).Entities.Count);
        }

        [Fact]
        public void When_executing_a_query_expression_equals_operator_is_case_insensitive()
        {
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();
            service.Create(new Contact { FirstName = "Jimmy" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.Equal, "jimmy");

            Assert.Equal(1, service.RetrieveMultiple(qe).Entities.Count);
        }

        [Fact]
        public void When_executing_a_query_expression_begins_with_operator_is_case_insensitive()
        {
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();
            service.Create(new Contact { FirstName = "Jimmy" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.BeginsWith, "jim");

            Assert.Equal(1, service.RetrieveMultiple(qe).Entities.Count);
        }

        [Fact]
        public void When_executing_a_query_expression_ends_with_operator_is_case_insensitive()
        {
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();
            service.Create(new Contact { FirstName = "JimmY" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.EndsWith, "y");

            Assert.Equal(1, service.RetrieveMultiple(qe).Entities.Count);
        }

        [Fact]
        public void When_executing_a_query_expression_attributes_returned_are_case_sensitive()
        {
            //So Where clauses shouldn't affect the Select clause
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();
            service.Create(new Contact { FirstName = "JimmY" });

            var qe = new QueryExpression("contact");
            qe.Criteria.AddCondition("firstname", ConditionOperator.EndsWith, "y");
            qe.ColumnSet = new ColumnSet(true);

            var entities = service.RetrieveMultiple(qe).Entities;
            Assert.Equal(1, entities.Count);
            Assert.Equal("JimmY", entities[0]["firstname"]);
        }
    }
}
