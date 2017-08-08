using Crm;
using FakeItEasy;
using FakeXrmEasy.Extensions;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveVersionRequestTests
{
    public class RetrieveVersionRequestTests
    {
        [Fact]
        public void AddsFakeVersionRequest()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var fakeVersionExecutor = new RetrieveVersionRequestExecutor();
            fakedContext.AddFakeMessageExecutor<RetrieveVersionRequest>(fakeVersionExecutor);

            var fakeVersionRequest = new RetrieveVersionRequest();
            var result = (RetrieveVersionResponse)fakedService.Execute(fakeVersionRequest);
            var version = result.Version;
            var versionComponents = version.Split('.');

            var majorVersion = versionComponents[0];
            var minorVersion = versionComponents[1];

#if FAKE_XRM_EASY_365

            Assert.True(int.Parse(majorVersion) >= 8);

            if (majorVersion == "8")
            {
                Assert.True(int.Parse(minorVersion) >= 2);
            }

#elif FAKE_XRM_EASY_2016

            Assert.True(int.Parse(majorVersion) >= 8);            
            Assert.True(int.Parse(minorVersion) >= 0);
            Assert.True(int.Parse(minorVersion) < 2);

#elif FAKE_XRM_EASY_2015

            Assert.True(version.StartsWith("7"));

#elif FAKE_XRM_EASY_2013

            Assert.True(version.StartsWith("6"));

#elif FAKE_XRM_EASY

            Assert.True(version.StartsWith("5"));
#endif
        }
    }
}
