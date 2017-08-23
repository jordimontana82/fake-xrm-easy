using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.QualifyLeadTests
{
    public class QualifyLeadTests
    {
        [Fact]
        public void Check_if_Account_was_created_after_sending_request()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetFakedOrganizationService();

            var lead = new Lead()
            {
                Id = Guid.NewGuid()
            };
            context.Initialize(new[] { lead });

            var request = new QualifyLeadRequest()
            {
                CreateAccount = true,
                CreateContact = false,
                CreateOpportunity = false,
                LeadId = lead.ToEntityReference(),
                Status = new OptionSetValue((int)LeadState.Qualified)
            };

            service.Execute(request);

            var account = (from acc in context.CreateQuery("account")
                           where acc.GetAttributeValue<EntityReference>("originatingleadid").Id == lead.Id
                           select acc).FirstOrDefault();

            Assert.NotNull(account);
        }

        [Fact]
        public void Check_if_Contact_was_created_after_sending_request()
        {
        }

        [Fact]
        public void Check_if_Opportunity_was_created_after_sending_request()
        {
        }
    }
}