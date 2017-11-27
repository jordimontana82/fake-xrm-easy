using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveMultiple
{
    public class RetrieveMultipleTests
    {
        /// <summary>
        /// Tests that paging works correctly
        /// </summary>
        [Fact]
        public void TestPaging()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service =  context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();
            int excessNumberOfRecords = 50;

            context.MaxRetrieveCount = 1000;
            for (int i = 0; i < context.MaxRetrieveCount + excessNumberOfRecords; i++)
            {
                Entity e = new Entity("entity");
                e.Id = Guid.NewGuid();
                initialEntities.Add(e);
            }
            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(context.MaxRetrieveCount, result.Entities.Count);
            Assert.True(result.MoreRecords);
            Assert.NotNull(result.PagingCookie);

            query.PageInfo = new PagingInfo()
            {
                PagingCookie = result.PagingCookie,
                PageNumber = 2,
            };
            result = service.RetrieveMultiple(query);
            Assert.Equal(excessNumberOfRecords, result.Entities.Count);
            Assert.False(result.MoreRecords);
        }

        /// <summary>
        /// Tests that top count works correctly
        /// </summary>
        [Fact]
        public void TestTop()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service =  context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            for (int i = 0; i < 10; i++)
            {
                Entity e = new Entity("entity");
                e.Id = Guid.NewGuid();
                initialEntities.Add(e);
            }
            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.TopCount = 5;
            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(query.TopCount, result.Entities.Count);
            Assert.False(result.MoreRecords);
        }

        /// <summary>
        /// Tests that an empty result set doesn't cause an error and that more records is correctly set to false
        /// </summary>
        [Fact]
        public void TestEmptyResultSet()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service =  context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["retrieve"] = false;
            initialEntities.Add(e);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.Criteria.AddCondition("retrieve", ConditionOperator.Equal, true);
            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(0, result.Entities.Count);
            Assert.False(result.MoreRecords);
        }

        /// <summary>
        /// Tests that a query with a filter on a link entity works the second time it is used (this was due to a shallow copy issue) 
        /// </summary>
        [Fact]
        public void TestMultiplePagesWithLinkedEntity()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service =  context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();
            int excessNumberOfRecords = 50;

            context.MaxRetrieveCount = 1000;
            for (int i = 0; i < context.MaxRetrieveCount + excessNumberOfRecords; i++)
            {
                Entity second = new Entity("second");
                second.Id = Guid.NewGuid();
                second["filter"] = true;
                initialEntities.Add(second);
                Entity first = new Entity("entity");
                first.Id = Guid.NewGuid();
                first["secondid"] = second.ToEntityReference();
                initialEntities.Add(first);
            }
            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            LinkEntity link = new LinkEntity("entity", "second", "secondid", "secondid", JoinOperator.Inner);
            link.EntityAlias = "second";
            link.LinkCriteria.AddCondition("filter", ConditionOperator.Equal, true);
            query.LinkEntities.Add(link);
            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(context.MaxRetrieveCount, result.Entities.Count);
            Assert.True(result.MoreRecords);
            Assert.NotNull(result.PagingCookie);

            query.PageInfo = new PagingInfo()
            {
                PagingCookie = result.PagingCookie,
                PageNumber = 2,
            };
            result = service.RetrieveMultiple(query);
            Assert.Equal(excessNumberOfRecords, result.Entities.Count);
            Assert.False(result.MoreRecords);
        }

        /// <summary>
        /// Tests that a link's criteria aren't changed by the query (this was a buggy behavior due to a shallow copy)
        /// </summary>
        [Fact]
        public void TestLinkCriteriaAreNotChanged()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service =  context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity second = new Entity("second");
            second.Id = Guid.NewGuid();
            second["filter"] = true;
            Entity first = new Entity("entity");
            first.Id = Guid.NewGuid();
            first["secondid"] = second.ToEntityReference();
            initialEntities.Add(first);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            LinkEntity link = new LinkEntity("entity", "second", "secondid", "secondid", JoinOperator.Inner);
            link.EntityAlias = "second";
            link.LinkCriteria.AddCondition("filter", ConditionOperator.Equal, true);
            query.LinkEntities.Add(link);
            service.RetrieveMultiple(query);
            Assert.Equal("filter", query.LinkEntities[0].LinkCriteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Equal, query.LinkEntities[0].LinkCriteria.Conditions[0].Operator);
            Assert.Equal(true, query.LinkEntities[0].LinkCriteria.Conditions[0].Values[0]);
        }

        /// <summary>
        /// Tests that if we ask for a non-existant page we don't get anything back and an error doesn't occur
        /// </summary>
        [Fact]
        public void TestAskingForEmptyPage()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service =  context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity first = new Entity("entity");
            first.Id = Guid.NewGuid();
            initialEntities.Add(first);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.PageInfo = new PagingInfo() { PageNumber = 2, Count = 20 };
            Assert.Equal(0, service.RetrieveMultiple(query).Entities.Count);
        }
    }
}
