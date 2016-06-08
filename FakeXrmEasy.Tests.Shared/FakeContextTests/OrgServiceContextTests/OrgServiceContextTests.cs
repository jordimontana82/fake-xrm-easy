using Crm;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace FakeXrmEasy.Tests.FakeContextTests.OrgServiceContextTests
{
    public class OrgServiceContextTests
    {

        [Fact]
        public void When_calling_context_add_and_save_changes_entity_is_added_to_the_faked_context()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            using(var ctx = new XrmServiceContext(service))
            {
                ctx.AddObject(new Account() { Name = "Test account" });
                ctx.SaveChanges();

                var account = ctx.CreateQuery<Account>()
                            .ToList()
                            .FirstOrDefault();

                Assert.Equal("Test account", account.Name);
            }
        }
    }
}
