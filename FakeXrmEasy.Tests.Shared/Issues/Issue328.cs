using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue328
    {
        [Fact]
        public void DistinctBug()
        {
            var a1 = new Entity("a")
            {
                Id = Guid.NewGuid(),
                Attributes = {
                    { "distinct_value_field", "same value" },
                }
            };

            var a2 = new Entity("a")
            {
                Id = Guid.NewGuid(),
                Attributes = {
                    { "distinct_value_field", "same value" },
                }
            };

            var queryNonDistinct = new QueryExpression("a")
            {
                Distinct = false,
                ColumnSet = new ColumnSet("distinct_value_field"),
            };

            var queryDistinct = new QueryExpression("a")
            {
                Distinct = true,
                ColumnSet = new ColumnSet("distinct_value_field"),
            };

            var context = new XrmFakedContext();
            context.Initialize(new List<Entity> { a1, a2 });
            var service = context.GetOrganizationService();

            var nonDistinctResult = service.RetrieveMultiple(queryNonDistinct);
            nonDistinctResult.Entities.ToList().ForEach(e => Debug.WriteLine($"Id: {e.Id} distinct_value_field: {e.GetAttributeValue<string>("distinct_value_field")}"));
            Assert.Equal(2, nonDistinctResult.Entities.Count);

            var distinctResult = service.RetrieveMultiple(queryDistinct);
            distinctResult.Entities.ToList().ForEach(e => Debug.WriteLine($"Id: {e.Id} distinct_value_field: {e.GetAttributeValue<string>("distinct_value_field")}"));
            Assert.Equal(1, distinctResult.Entities.Count);
        }
    }
}
