using Crm;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
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
            var service = context.GetOrganizationService();

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
            var service = context.GetOrganizationService();

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
            var service = context.GetOrganizationService();

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

        [Fact]
        public void When_an_entity_is_returned_with_specific_columns_formatted_values_are_also_cloned()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var account = new Account() { Id = Guid.NewGuid() };
            account["statecode"] = AccountState.Active;

            var formattedValues = new FormattedValueCollection();
            formattedValues.Add("statecode", "Active");

            account.Inject("FormattedValues", formattedValues);

            context.Initialize(new List<Entity>()
            {
                account
            });

            var a = service.Retrieve("account", account.Id, new ColumnSet("statecode"));

            Assert.True(a.FormattedValues != null);
            Assert.True(a.FormattedValues.Contains("statecode"));
            Assert.Equal("Active", a.FormattedValues["statecode"]);
        }

        [Fact]
        public void When_an_entity_is_returned_with_link_entity_and_specific_columns_formatted_values_are_also_cloned()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var contact = new Contact() { Id = Guid.NewGuid() };
            contact["statecode"] = new OptionSetValue((int) ContactState.Inactive);
            var contactFormattedValues = new FormattedValueCollection();
            contactFormattedValues.Add("statecode", "Inactive");

            contact.Inject("FormattedValues", contactFormattedValues);

            var account = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference()
            };
            account["statecode"] = new OptionSetValue((int)AccountState.Active);

            var accountFormattedValues = new FormattedValueCollection();
            accountFormattedValues.Add("statecode", "Active");

            account.Inject("FormattedValues", accountFormattedValues);

            context.Initialize(new List<Entity>()
            {
                account,
                contact
            });

            var query = new QueryExpression("account");
            query.Criteria.AddCondition("accountid", ConditionOperator.Equal, account.Id);
            var linkedContact = query.AddLink("contact", "primarycontactid", "contactid");
            linkedContact.Columns.AddColumns("statecode");

            var a = service.RetrieveMultiple(query).Entities.FirstOrDefault();

            Assert.True(a.FormattedValues != null);
            Assert.True(a.FormattedValues.Contains("contact1.statecode"));
            Assert.Equal("Inactive", a.FormattedValues["contact1.statecode"]);
        }

        [Fact]
        public void When_an_entity_is_returned_with_link_entity_and_specific_columns_formatted_values_are_also_cloned2()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var contact = new Contact() { Id = Guid.NewGuid() };
            contact["statecode"] = ContactState.Inactive;
            var contactFormattedValues = new FormattedValueCollection();
            contactFormattedValues.Add("statecode", "Inactive");

            contact.Inject("FormattedValues", contactFormattedValues);

            var account = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference()
            };
            account["statecode"] = AccountState.Active;

            var accountFormattedValues = new FormattedValueCollection();
            accountFormattedValues.Add("statecode", "Active");

            account.Inject("FormattedValues", accountFormattedValues);

            context.Initialize(new List<Entity>()
            {
                account,
                contact
            });

            var query = new QueryExpression("account");
            query.Criteria.AddCondition("accountid", ConditionOperator.Equal, account.Id);
            var linkedContact = query.AddLink("contact", "primarycontactid", "contactid");
            linkedContact.Columns.AddColumns("statecode");

            var a = service.RetrieveMultiple(query).Entities.FirstOrDefault();

            Assert.True(a.FormattedValues != null);
            Assert.True(a.FormattedValues.Contains("contact1.statecode"));
            Assert.Equal("Inactive", a.FormattedValues["contact1.statecode"]);
        }
    }
}