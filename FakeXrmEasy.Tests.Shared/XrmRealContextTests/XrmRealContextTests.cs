using Crm;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Xunit;

#if FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365
using Xunit.Sdk;
#endif

namespace FakeXrmEasy.Tests.XrmRealContextTests
{
    public class XrmRealContextTests
    {
        [Fact]
        public void Should_connect_to_CRM()
        {
            var ctx = new XrmRealContext();
            var ex = Record.Exception(() => ctx.GetOrganizationService());
            Assert.Null(ex);
        }

        [Fact]
        public void Should_connect_to_CRM_with_given_ConnectionString()
        {
            var ctx = new XrmRealContext("myfirstconnectionstring");
            Assert.Equal("myfirstconnectionstring", ctx.ConnectionStringName);
        }

        [Fact]
        public void Should_connect_to_CRM_with_given_OrganizationService()
        {
            var ctx = new XrmRealContext();
            var organizationService = ctx.GetOrganizationService();
            var ctx2 = new XrmRealContext(organizationService);
            Assert.Equal(organizationService, ctx2.GetOrganizationService());
        }

        [Fact]
        public void Should_not_initialize_records_when_using_a_real_CRM_instance()
        {
            var ctx = new XrmRealContext();
            ctx.Initialize(new List<Entity>
            {
                new Account { Id = Guid.NewGuid() }
            });
            Assert.Equal(0, ctx.Data.Count);
        }

        [Fact]
        public void Should_generate_plugin_context_from_a_serialised_profile()
        {
            var ctx = new XrmRealContext();
            //var serialised = @"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";
            var serialised = @"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";

            var ex = Record.Exception(() => ctx.GetContextFromSerialisedCompressedProfile(serialised));
            Assert.Null(ex);

            var pluginProfile = ctx.GetContextFromSerialisedCompressedProfile(serialised);
            Assert.NotEqual(0, pluginProfile.Stage);
        }
    }
}