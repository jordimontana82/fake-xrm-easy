#if FAKE_XRM_EASY

using Crm.Models;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.LinqTests
{
    public class MinClientContext
    {
        /* TO DO
        [Fact]
        public void TestMinimalClientModel_Linq_Issue114()
        {
            Guid userId = Guid.NewGuid();
            var fakedContext = new XrmFakedContext();
           fakedContext.Initialize(new List<Entity>() {
                new Crm.SystemUser() {Id = userId },
                new fake_minclient() { Id = Guid.NewGuid(), fake_name = "Jordi", OwnerId = new EntityReference (Crm.SystemUser.EntityLogicalName, userId) },
                new fake_minclient() { Id = Guid.NewGuid(), fake_name = "Mardi", ["statecode"] = 0 , OwnerId = new EntityReference (Crm.SystemUser.EntityLogicalName, userId) },
                new fake_minclient() { Id = Guid.NewGuid(), fake_name= "Mason", ["statecode"] = 1 , OwnerId = new EntityReference (Crm.SystemUser.EntityLogicalName, Guid.NewGuid())  }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (CrmContext ctx = new CrmContext(service))
            {
                var active = (from c in ctx.fake_minclientSet
                              where c.statecode == 1
                              select c).ToList();

                Assert.Equal(1, active.Count());

                var ownedByUser = (from c in ctx.fake_minclientSet
                              where c.OwnerId.Id == userId
                              select c).ToList();

                Assert.Equal(2, ownedByUser.Count());

                var query = ctx.fake_minclientSet.Where(c => c.fake_name != null);
                query = query.Where(c=> c.statecode == 0);
                var notActive= query.ToList();

                Assert.Equal(2, notActive.Count());
            }

            ////  A.CallTo(() => service.Execute(A<OrganizationRequest>.That.Matches(x => x is RetrieveMultipleRequest && ((RetrieveMultipleRequest)x).Query is QueryExpression))).MustHaveHappened();
        }

    */
    }
}

#endif