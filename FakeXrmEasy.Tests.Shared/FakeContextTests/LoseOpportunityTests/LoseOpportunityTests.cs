using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.LoseOpportunityTests
{
    public class LoseOpportunityTests
    {
        [Fact]
        public void Check_if_Opportunity_status_is_Lose_after_set()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetFakedOrganizationService();

            var opportunity = new Opportunity()
            {
                Id = Guid.NewGuid()
            };
            context.Initialize(new[] { opportunity });

            var request = new LoseOpportunityRequest()
            {
                OpportunityClose = new OpportunityClose
                {
                    OpportunityId = new EntityReference(Opportunity.EntityLogicalName, opportunity.Id)
                },
                Status = new OptionSetValue((int)OpportunityState.Lost)
            };

            service.Execute(request);

            var opp = (from op in context.CreateQuery<Opportunity>()
                       where op.Id == opportunity.Id
                       select op).FirstOrDefault();

            Assert.Equal(opp.StatusCode.Value, (int)OpportunityState.Lost);
        }
    }
}