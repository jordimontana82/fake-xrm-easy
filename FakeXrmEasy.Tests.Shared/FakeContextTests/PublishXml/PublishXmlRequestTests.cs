using Microsoft.Crm.Sdk.Messages;
using System;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.PublishXml
{
    public class PublishXmlRequestTests
    {
        [Fact]
        public void When_calling_publish_xml_exception_is_raised_if_parameter_xml_is_blank()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new PublishXmlRequest()
            {
                ParameterXml = ""
            };

            Assert.Throws<Exception>(() => service.Execute(req));
        }

        [Fact]
        public void When_calling_publish_xml_no_exception_is_raised()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var req = new PublishXmlRequest()
            {
                ParameterXml = "<somexml></somexml>"
            };

            var ex = Record.Exception(() => service.Execute(req));
            Assert.Null(ex);
        }
    }
}