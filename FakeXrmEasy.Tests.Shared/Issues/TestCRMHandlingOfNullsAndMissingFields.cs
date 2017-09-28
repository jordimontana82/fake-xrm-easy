using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class TestCRMHandlingOfNullsAndMissingFields
    {
        [Fact]
        public void TestRetrieveWithNull()
        {
            Entity testEntity = new Entity("testentity");
            testEntity["field"] = null;
            testEntity.Id = Guid.NewGuid();

            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(
                new List<Entity>()
                {
                    testEntity
                }
            );

            Entity e = service.Retrieve("testentity", testEntity.Id, new ColumnSet("field"));
            Assert.False(e.Contains("field"));
        }

        [Fact]
        public void TestRetrieveMultipleWithNull()
        {
            Entity testEntity = new Entity("testentity");
            testEntity["field"] = null;
            testEntity.Id = Guid.NewGuid();

            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(
                new List<Entity>()
                {
                    testEntity
                }
            );

            QueryExpression contactQuery = new QueryExpression("testentity");
            contactQuery.ColumnSet = new ColumnSet("field");
            EntityCollection result = service.RetrieveMultiple(contactQuery);
            Assert.False(result.Entities[0].Contains("field"));
        }

        [Fact]
        public void TestRetrieveWithMissingField()
        {
            Entity testEntity = new Entity("testentity");
            testEntity.Id = Guid.NewGuid();

            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(
                new List<Entity>()
                {
                    testEntity
                }
            );

            Entity e = service.Retrieve("testentity", testEntity.Id, new ColumnSet("field"));
            Assert.False(e.Contains("field"));
        }

        [Fact]
        public void TestRetrieveMultipleWithMissingField()
        {
            Entity testEntity = new Entity("testentity");
            testEntity.Id = Guid.NewGuid();

            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(
                new List<Entity>()
                {
                    testEntity
                }
            );

            QueryExpression contactQuery = new QueryExpression("testentity");
            contactQuery.ColumnSet = new ColumnSet("field");
            EntityCollection result = service.RetrieveMultiple(contactQuery);
            Assert.False(result.Entities[0].Contains("field"));
        }

        [Fact]
        public void TestRetriveWithLinkEntityWithNullField()
        {
            List<Entity> initialEntities = new List<Entity>();
            Entity parentEntity = new Entity("parent");
            parentEntity["field"] = null;
            // So there seems to be a bug here that if an entity only contains null fields that this entity won't be returned in a link entity query
            // The other field is to get around that
            parentEntity["otherfield"] = 1;
            parentEntity.Id = Guid.NewGuid();
            initialEntities.Add(parentEntity);

            Entity childEntity = new Entity("child");
            childEntity["parent"] = parentEntity.ToEntityReference();
            childEntity.Id = Guid.NewGuid();
            initialEntities.Add(childEntity);

            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            context.Initialize(initialEntities);

            QueryExpression query = new QueryExpression("child");
            LinkEntity link = new LinkEntity("child", "parent", "parent", "parentid", JoinOperator.Inner);
            link.EntityAlias = "parententity";
            link.Columns = new ColumnSet("field");
            query.LinkEntities.Add(link);

            Entity result = service.RetrieveMultiple(query).Entities[0];

            Assert.False(result.Contains("parententity.field"));
        }
    }
}