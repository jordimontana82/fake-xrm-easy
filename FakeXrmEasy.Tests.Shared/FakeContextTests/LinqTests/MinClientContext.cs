#if FAKE_XRM_EASY
using Crm.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.LinqTests
{
    public class MinClientContext
    {

        [Fact]
        public void TestMinimalClientModel_Linq()
        {
          
            var fakedContext = new XrmFakedContext();
           fakedContext.Initialize(new List<Entity>() {
                new fake_minclient() { Id = Guid.NewGuid(), fake_name = "Jordi" },
                new fake_minclient() { Id = Guid.NewGuid(), fake_name = "Mardi", ["statecode"] = 0 },
                new fake_minclient() { Id = Guid.NewGuid(), fake_name= "Mason", ["statecode"] = 1 }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (CrmContext ctx = new CrmContext(service))
            {
                var active = (from c in ctx.fake_minclientSet
                              where c.statecode.Equals(1)
                              select c).ToList();

                Assert.Equal(1, active.Count());

                var notActive= (from c in ctx.fake_minclientSet
                               where c.statecode.Equals(0)
                               select c).ToList();

                Assert.Equal(2, notActive.Count());

                
            }
          
            ////  A.CallTo(() => service.Execute(A<OrganizationRequest>.That.Matches(x => x is RetrieveMultipleRequest && ((RetrieveMultipleRequest)x).Query is QueryExpression))).MustHaveHappened();
        }

    }
}
#endif
