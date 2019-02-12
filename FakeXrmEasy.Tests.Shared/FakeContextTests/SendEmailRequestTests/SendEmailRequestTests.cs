using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.SendEmailRequestTests
{
    public class SendEmailRequestTests
    {
        [Fact]
        public void When_SendEmailRequest_call_statecode_is_Completed_and_statuscode_is_Sent()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var email = new Crm.Email()
            {
                Id = Guid.NewGuid()
            };
            var emailId = service.Create(email);

            var request = new SendEmailRequest
            {
                EmailId = emailId,
                TrackingToken = "TrackingToken",
                IssueSend = true
            };
            var response = (SendEmailResponse)service.Execute(request);

            var entity = service.Retrieve("email", emailId, new ColumnSet("statecode", "statuscode"));
            Assert.Equal(1, entity?.GetAttributeValue<OptionSetValue>("statecode")?.Value);
            Assert.Equal(3, entity?.GetAttributeValue<OptionSetValue>("statuscode")?.Value);
        }
    }
}
