using Crm;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests
{
    public class FormattedValuesTests
    {
        [Fact]
        public void When_an_optionset_is_retrieved_where_its_value_is_an_enum_formatted_value_doesnt_contain_key_if_value_was_null()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var account = new Account() { Id = Guid.NewGuid() };
            account["statecode"] = null;

            context.Initialize(new List<Entity>()
            {
                account
            });

            using (var ctx = new XrmServiceContext(service))
            {
                var a = (from acc in ctx.CreateQuery<Account>()
                         select acc).FirstOrDefault();

                Assert.Equal(0, a.FormattedValues.Count);
                Assert.False(a.FormattedValues.Contains("statecode"));
            }
        }

        [Fact]
        public void When_an_optionset_is_retrieved_where_its_value_is_an_enum_formatted_value_is_returned()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var account = new Account() { Id = Guid.NewGuid() };
            account["statecode"] = AccountState.Active;

            context.Initialize(new List<Entity>()
            {
                account
            });

            using (var ctx = new XrmServiceContext(service))
            {
                var a = (from acc in ctx.CreateQuery<Account>()
                         select acc).FirstOrDefault();

                Assert.True(a.FormattedValues != null);
                Assert.True(a.FormattedValues.Contains("statecode"));
                Assert.Equal("Active", a.FormattedValues["statecode"]);
            }
        }

        [Fact]
        public void When_an_entity_is_returned_formatted_values_are_also_cloned()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var account = new Account() { Id = Guid.NewGuid() };
            account["statecode"] = new OptionSetValue(0);

            var formattedValues = new FormattedValueCollection();
            formattedValues.Add("statecode", "Active");
            account.Inject("FormattedValues", formattedValues);

            context.Initialize(new List<Entity>()
            {
                account
            });

            using (var ctx = new XrmServiceContext(service))
            {
                var a = (from acc in ctx.CreateQuery<Account>()
                         select acc).FirstOrDefault();

                Assert.True(a.FormattedValues != null);
                Assert.True(a.FormattedValues.Contains("statecode"));
                Assert.Equal("Active", a.FormattedValues["statecode"]);
            }
        }
    }
}