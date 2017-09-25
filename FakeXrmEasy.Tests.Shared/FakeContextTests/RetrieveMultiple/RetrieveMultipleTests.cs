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
    }
}
