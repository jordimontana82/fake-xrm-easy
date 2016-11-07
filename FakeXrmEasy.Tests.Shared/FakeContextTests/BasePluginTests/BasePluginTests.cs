using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.BasePluginTests
{
    public class BasePluginTests
    {
        [Fact]
        public void My_First_Test()
        {
            var context = new XrmFakedContext();

            var account = new Entity("account");
            account["name"] = "Hello World";
            account["address1_postcode"] = "1234";

            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", account);

            var pluginCtx = context.GetDefaultPluginContext();
            pluginCtx.Stage = 20;
            pluginCtx.MessageName = "Create";
            pluginCtx.InputParameters = inputParameters;

            Assert.DoesNotThrow(() => context.ExecutePluginWithConfigurations<AccountSetTerritories>(pluginCtx, null, null));
        }
    }
}
