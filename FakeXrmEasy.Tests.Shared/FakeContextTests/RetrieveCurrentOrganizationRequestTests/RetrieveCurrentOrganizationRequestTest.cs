using Microsoft.Crm.Sdk.Messages;
using System;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveCurrentOrganizationRequestTests
{
    public class RetrieveCurrentOrganizationRequestTest
    {
        [Fact]
        public void RetrieveCurrentOrganization_Rrequest()
        {
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013
            var ctx = new XrmFakedContext();
            var service = ctx.GetFakedOrganizationService();

            var req = new RetrieveCurrentOrganizationRequest()
            {
                
            };

            var ex = Record.Exception(() => service.Execute(req));
            Assert.Null(ex);
#endif
        }
    }
}
