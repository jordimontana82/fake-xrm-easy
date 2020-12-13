using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.QueryTranslationTests
{
    public class ProjectionTests
    {
        private XrmFakedContext FakeContext;
        private IOrganizationService Service;
        private Account Account;

        public ProjectionTests()
        {
            FakeContext = new XrmFakedContext();
            Service = FakeContext.GetOrganizationService();
            Account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "Some name"
            };
        }

        [Fact]
        public void Should_return_primary_key_attribute_even_if_not_specified_in_column_set()
        {
            FakeContext.Initialize(Account);
            var account = Service.Retrieve(Account.EntityLogicalName, Account.Id, new ColumnSet(new string[] { "name" }));
            Assert.True(account.Attributes.ContainsKey("accountid"));
        }

        [Fact]
        public void Should_return_primary_key_attribute_when_retrieving_using_all_columns()
        {
            FakeContext.Initialize(Account);
            var account = Service.Retrieve(Account.EntityLogicalName, Account.Id, new ColumnSet(true));
            Assert.True(account.Attributes.ContainsKey("accountid"));
        }
    }
}
