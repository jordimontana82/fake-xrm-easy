using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class TestGenericMessageExecutors
    {
        [Fact]
        public void TestGenericMessage()
        {
            XrmFakedContext context = new XrmFakedContext();
            context.AddGenericFakeMessageExecutor("new_TestAction", new FakeMessageExecutor());
            IOrganizationService service = context.GetOrganizationService();
            OrganizationRequest request = new OrganizationRequest("new_TestAction");
            request["input"] = "testinput";
            OrganizationResponse response = service.Execute(request);
            Assert.Equal("testinput", response["output"]);
        }

        [Fact]
        public void TestGenericMessageRemoval()
        {

            XrmFakedContext context = new XrmFakedContext();
            context.AddGenericFakeMessageExecutor("new_TestAction", new FakeMessageExecutor());
            IOrganizationService service = context.GetOrganizationService();
            OrganizationRequest request = new OrganizationRequest("new_TestAction");
            request["input"] = "testinput";
            OrganizationResponse response = service.Execute(request);
            Assert.Equal("testinput", response["output"]);
            context.RemoveGenericFakeMessageExecutor("new_TestAction");
            Assert.Throws(typeof(FakeXrmEasy.PullRequestException), () => service.Execute(request));
        }
    }

    class FakeMessageExecutor : FakeMessageExecutors.IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request.RequestName == "new_TestAction";
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            OrganizationResponse response = new OrganizationResponse();
            response["output"] = request["input"];
            return response;
        }

        public Type GetResponsibleRequestType()
        {
            throw new NotImplementedException();
        }
    }
}
