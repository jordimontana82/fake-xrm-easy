using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.Tests.Extensions
{
    public class QueryExpressionExtensionsTests
    {
        [Fact]
        public void TestClone()
        {
            QueryExpression query = new QueryExpression("entity");
            LinkEntity link = new LinkEntity("entity", "second", "secondid", "secondid", JoinOperator.Inner);
            link.EntityAlias = "second";
            link.LinkCriteria.AddCondition("filter", ConditionOperator.Equal, true);
            query.LinkEntities.Add(link);

            QueryExpression cloned = query.Clone();
            cloned.LinkEntities[0].LinkCriteria.Conditions[0].AttributeName = "otherfield";

            cloned.LinkEntities[0].LinkCriteria.Conditions[0].AttributeName = "link.field";
            Assert.Equal("entity", query.EntityName);
            Assert.Equal("filter", query.LinkEntities[0].LinkCriteria.Conditions[0].AttributeName );
        }
    }
}
