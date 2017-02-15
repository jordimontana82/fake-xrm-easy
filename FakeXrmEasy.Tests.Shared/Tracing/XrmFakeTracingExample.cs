﻿using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.Tracing
{
    public class XrmFakeTracingExample
    {
        [Fact]
        public void Example_about_retrieving_traces_written_by_plugin()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("account") { Id = guid1 };

            //Execute our plugin against a target that doesn't contains the accountnumber attribute
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<AccountNumberPlugin>(target);

            //Get tracing service
            var fakeTracingService = fakedContext.GetFakeTracingService();
            var log = fakeTracingService.DumpTrace();

            //Assert that the target contains a new attribute      
            Assert.Equal(log, "Contains target\r\nIs Account\r\n");
        }
    }
}
