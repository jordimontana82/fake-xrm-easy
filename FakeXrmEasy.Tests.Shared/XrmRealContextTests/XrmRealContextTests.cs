using Crm;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.XrmRealContextTests
{
    public class XrmRealContextTests
    {
        [Fact]
        public void Should_connect_to_CRM()
        {
            var ctx = new XrmRealContext();
            Assert.DoesNotThrow(() => ctx.GetOrganizationService());
        }

        [Fact]
        public void Should_not_initialize_records_when_using_a_real_CRM_instance()
        {
            var ctx = new XrmRealContext();
            ctx.Initialize(new List<Entity>
            {
                new Account() { Id = Guid.NewGuid() }
            });
            Assert.Equal(0, ctx.Data.Count);
        }
    }
}
