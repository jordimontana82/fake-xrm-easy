using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests
{
    public class PaginationTests
    {
        [Fact]
        public void When_a_query_expression_without_pagination_is_executed_all_records_are_returned()
        {
            var context = new XrmFakedContext();

            //Create 20 contacts
            var contactList = new List<Entity>();

            for (var i= 0; i < 20; i++) {
                contactList.Add(new Entity("contact") { Id = Guid.NewGuid() });
            }

            context.Initialize(contactList);

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count == 20);
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
