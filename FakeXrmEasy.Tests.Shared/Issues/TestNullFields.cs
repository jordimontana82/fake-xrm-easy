using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.Tests.Issues
{
    public class TestNullFields
    {
        [Fact]
        public void TestRetrieve()
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
        public void TestRetrieveMultiple()
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
    }
}
