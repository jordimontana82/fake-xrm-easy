using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.OptionSetValuesRequestTests
{
    public class OptionSetValueRequestsTests
    {
        [Fact]
        public void When_calling_insert_option_set_value_without_label_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new InsertOptionValueRequest()
            {
                Label = new Label("", 0)
            };

            Assert.Throws<Exception>(() => service.Execute(req));
        }

        [Fact]
        public void When_calling_insert_option_set_value_without_optionsetname_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new InsertOptionValueRequest()
            {
                Label = new Label("Yeah! This is a fake label!", 0)
            };

            Assert.Throws<Exception>(() => service.Execute(req));
        }

        [Fact]
        public void When_calling_insert_option_set_value_without_entityname_or_attributename_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new InsertOptionValueRequest()
            {
                EntityLogicalName = "Not empty",
                Label = new Label("Yeah! This is a fake label!", 0)
            };

            Assert.Throws<Exception>(() => service.Execute(req));

            req = new InsertOptionValueRequest()
            {
                AttributeLogicalName = "Not empty",
                Label = new Label("Yeah! This is a fake label!", 0)
            };

            Assert.Throws<Exception>(() => service.Execute(req));
        }

        [Fact]
        public void When_calling_insert_option_set_value_for_global_optionset_optionmetadata_contains_it()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new InsertOptionValueRequest()
            {
                OptionSetName = "GlobalOptionSet",
                Label = new Label("Yeah! This is a fake label!", 0)
            };

            service.Execute(req);

            //Check the optionsetmetadata was updated
            Assert.True(ctx.OptionSetValuesMetadata.ContainsKey("GlobalOptionSet"));

            var option = ctx.OptionSetValuesMetadata["GlobalOptionSet"].Options.FirstOrDefault();
            Assert.NotEqual(null, option);
            Assert.Equal("Yeah! This is a fake label!", option.Label.LocalizedLabels[0].Label);
        }

        [Fact]
        public void When_calling_insert_option_set_value_for_local_optionset_optionmetadata_contains_it()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new InsertOptionValueRequest()
            {
                EntityLogicalName = Account.EntityLogicalName,
                AttributeLogicalName = "new_custom",
                Label = new Label("Yeah! This is a fake label!", 0)
            };

            service.Execute(req);

            //Check the optionsetmetadata was updated
            var key = string.Format("{0}#{1}", req.EntityLogicalName, req.AttributeLogicalName);

            Assert.True(ctx.OptionSetValuesMetadata.ContainsKey(key));

            var option = ctx.OptionSetValuesMetadata[key].Options.FirstOrDefault();
            Assert.NotEqual(null, option);
            Assert.Equal("Yeah! This is a fake label!", option.Label.LocalizedLabels[0].Label);
        }
    }
}