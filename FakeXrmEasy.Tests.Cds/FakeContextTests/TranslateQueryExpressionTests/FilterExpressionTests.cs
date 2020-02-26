using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests
{
    public class FilterExpressionTests
    {
        [Fact]
        public void When_executing_a_query_expression_with_2_filters_combined_with_an_or_filter_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);

            var filter1 = new FilterExpression();
            filter1.AddCondition(new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 1"));

            var filter2 = new FilterExpression();
            filter2.AddCondition(new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 2"));

            qe.Criteria = new FilterExpression(LogicalOperator.Or);
            qe.Criteria.AddFilter(filter1);
            qe.Criteria.AddFilter(filter2);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count == 2);
        }

        [Fact]
        public void When_executing_a_query_expression_with_1_filters_combined_with_1_condition_and_or_filter_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.Or);
            qe.Criteria.AddCondition(new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 1"));

            var filter1 = new FilterExpression();
            filter1.AddCondition(new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 2"));

            qe.Criteria.AddFilter(filter1);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count == 2);
        }
    }
}