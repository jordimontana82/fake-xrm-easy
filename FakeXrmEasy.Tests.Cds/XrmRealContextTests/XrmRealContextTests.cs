﻿using Crm;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Xunit;

#if FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
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
    }
}