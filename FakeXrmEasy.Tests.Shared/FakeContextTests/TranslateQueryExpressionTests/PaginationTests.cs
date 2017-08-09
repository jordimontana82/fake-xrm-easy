using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests
{
    public class PaginationTests
    {
        [Fact]
        public void When_a_query_expression_without_pagination_is_executed_a_maximum_of_maxretrievecount_records_are_returned()
        {
            var context = new XrmFakedContext();

            //Create 20 contacts
            var contactList = new List<Entity>();

            for (var i = 0; i < context.MaxRetrieveCount + 1; i++)
            {
                contactList.Add(new Entity("contact") { Id = Guid.NewGuid() });
            }

            context.Initialize(contactList);

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count == context.MaxRetrieveCount);
        }

        [Fact]
        public void When_a_query_expression_with_pagination_is_executed_with_the_requested_page_size()
        {
            var context = new XrmFakedContext();

            //Create 20 contacts
            var contactList = new List<Entity>();

            for (var i = 0; i < context.MaxRetrieveCount; i++)
            {
                contactList.Add(new Entity("contact") { Id = Guid.NewGuid() });
            }

            context.Initialize(contactList);

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.TopCount = 100;

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count == 100);
        }

        [Fact]
        public void When_a_query_expression_with_topcount_is_executed_only_a_number_of_records_equals_to_the_page_size_is_returned()
        {
            var context = new XrmFakedContext();

            //Create 20 contacts
            var contactList = new List<Entity>();

            for (var i = 0; i < 20; i++)
            {
                contactList.Add(new Entity("contact") { Id = Guid.NewGuid() });
            }

            context.Initialize(contactList);

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.TopCount = 10;
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count == 10);
        }
    }
}