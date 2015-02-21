using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

using FakeXrmEasy.Tests.PluginsForTesting;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestPlugins
    {
        [Fact]
        public void When_a_plugin_with_target_is_executed_the_inherent_plugin_was_also_executed_without_exceptions()
        {
            var fakedContext = new XrmFakedContext();
            
            var guid1 = Guid.NewGuid();
            var target = new Entity("contact") { Id = guid1 };

            //Execute our plugin against the selected target
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<RetrieveServicesPlugin>(target);

            //Assert that the plugin was executed      
            A.CallTo(() => fakedPlugin.Execute(A<IServiceProvider>._))
                .MustHaveHappened();            
            
        }

        [Fact]
        public void When_the_account_number_plugin_is_executed_it_adds_a_random_number_to_an_account_entity()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("account") { Id = guid1 };
            
            //Execute our plugin against a target that doesn't contains the accountnumber attribute
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<AccountNumberPlugin>(target);

            //Assert that the target contains a new attribute      
            Assert.True(target.Attributes.ContainsKey("accountnumber"));

        }

        [Fact]
        public void When_the_account_number_plugin_is_executed_for_an_account_that_already_has_a_number_exception_is_thrown()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("account") { Id = guid1 };
            target["accountnumber"] = 69;

            //Execute our plugin against a target that doesn't contains the accountnumber attribute
            Assert.Throws<InvalidPluginExecutionException>(() => fakedContext.ExecutePluginWithTarget<AccountNumberPlugin>(target));

        }
    }
}
