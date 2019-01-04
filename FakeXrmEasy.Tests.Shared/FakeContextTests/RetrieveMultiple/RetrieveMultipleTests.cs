using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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
            IOrganizationService service = context.GetOrganizationService();
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

            List<Entity> allRecords = new List<Entity>();
            QueryExpression query = new QueryExpression("entity");
            EntityCollection result = service.RetrieveMultiple(query);
            allRecords.AddRange(result.Entities);
            Assert.Equal(context.MaxRetrieveCount, result.Entities.Count);
            Assert.True(result.MoreRecords);
            Assert.NotNull(result.PagingCookie);

            query.PageInfo = new PagingInfo()
            {
                PagingCookie = result.PagingCookie,
                PageNumber = 2,
            };
            result = service.RetrieveMultiple(query);
            allRecords.AddRange(result.Entities);
            Assert.Equal(excessNumberOfRecords, result.Entities.Count);
            Assert.False(result.MoreRecords);

            foreach (Entity e in initialEntities)
            {
                Assert.True(allRecords.Any(r => r.Id == e.Id));
            }
        }

        /// <summary>
        /// Tests that top count works correctly
        /// </summary>
        [Fact]
        public void TestTop()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
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
            IOrganizationService service = context.GetOrganizationService();
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
            IOrganizationService service = context.GetOrganizationService();
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
            IOrganizationService service = context.GetOrganizationService();
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
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity first = new Entity("entity");
            first.Id = Guid.NewGuid();
            initialEntities.Add(first);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.PageInfo = new PagingInfo() { PageNumber = 2, Count = 20 };
            Assert.Equal(0, service.RetrieveMultiple(query).Entities.Count);
        }

        /// <summary>
        /// Tests that if distinct is asked for that a distinct number of entities is returned
        /// </summary>
        [Fact]
        public void TestThatDistinctWorks()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity first = new Entity("entity");
            first.Id = Guid.NewGuid();
            first["field"] = "value";
            initialEntities.Add(first);

            Entity related = new Entity("related");
            related.Id = Guid.NewGuid();
            related["entityid"] = first.ToEntityReference();
            related["include"] = true;
            initialEntities.Add(related);

            Entity secondRelated = new Entity("related");
            secondRelated.Id = Guid.NewGuid();
            secondRelated["entityid"] = first.ToEntityReference();
            secondRelated["include"] = true;
            initialEntities.Add(secondRelated);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.ColumnSet = new ColumnSet("field");
            query.Distinct = true;

            LinkEntity link = new LinkEntity("entity", "related", "entityid", "entityid", JoinOperator.Inner);
            link.LinkCriteria.AddCondition("include", ConditionOperator.Equal, true);

            query.LinkEntities.Add(link);

            Assert.Equal(1, service.RetrieveMultiple(query).Entities.Count);
        }

        /// <summary>
        /// Tests that if distinct is asked for and fields are pulled in from the link entities that the correct 
        /// records are returned
        /// </summary>
        [Fact]
        public void TestThatDistinctWorksWithLinkEntityFields()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity first = new Entity("entity");
            first.Id = Guid.NewGuid();
            first["field"] = "value";
            initialEntities.Add(first);

            Entity related = new Entity("related");
            related.Id = Guid.NewGuid();
            related["entityid"] = first.ToEntityReference();
            related["include"] = true;
            related["linkfield"] = "value";
            initialEntities.Add(related);

            Entity secondRelated = new Entity("related");
            secondRelated.Id = Guid.NewGuid();
            secondRelated["entityid"] = first.ToEntityReference();
            secondRelated["include"] = true;
            secondRelated["linkfield"] = "other value";
            initialEntities.Add(secondRelated);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.ColumnSet = new ColumnSet("field");
            query.Distinct = true;

            LinkEntity link = new LinkEntity("entity", "related", "entityid", "entityid", JoinOperator.Inner);
            link.LinkCriteria.AddCondition("include", ConditionOperator.Equal, true);
            link.Columns = new ColumnSet("linkfield");

            query.LinkEntities.Add(link);

            Assert.Equal(2, service.RetrieveMultiple(query).Entities.Count);
        }

        /// <summary>
        /// Tests that if PageInfo's ReturnTotalRecordCount sets total record count.
        /// </summary>
        [Fact]
        public void TestThatPageInfoTotalRecordCountWorks()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["retrieve"] = true;
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["retrieve"] = true;
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["retrieve"] = false;
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.PageInfo.ReturnTotalRecordCount = true;
            query.Criteria.AddCondition("retrieve", ConditionOperator.Equal, true);
            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(2, result.Entities.Count);
            Assert.Equal(2, result.TotalRecordCount);
            Assert.False(result.MoreRecords);
        }

        /// <summary>
        /// Tests that if PageInfo's ReturnTotalRecordCount works correctly with paging 
        /// </summary>
        [Fact]
        public void TestThatPageInfoTotalRecordCountWorksWithPaging()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            for (int i = 0; i < 100; i++)
            {
                Entity e = new Entity("entity");
                e.Id = Guid.NewGuid();
                initialEntities.Add(e);
            }

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("entity");
            query.PageInfo.ReturnTotalRecordCount = true;
            query.PageInfo.PageNumber = 1;
            query.PageInfo.Count = 10;

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(10, result.Entities.Count);
            Assert.Equal(100, result.TotalRecordCount);
            Assert.True(result.MoreRecords);

            query.PageInfo.PageNumber++;
            query.PageInfo.Count = 20;
            query.PageInfo.PagingCookie = result.PagingCookie;

            result = service.RetrieveMultiple(query);
            Assert.Equal(20, result.Entities.Count);
            Assert.Equal(100, result.TotalRecordCount);
            Assert.True(result.MoreRecords);
        }
    }
}
