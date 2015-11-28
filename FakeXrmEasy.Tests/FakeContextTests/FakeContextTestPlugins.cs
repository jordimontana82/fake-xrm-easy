using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

using FakeXrmEasy.Tests.PluginsForTesting;
using Crm;
using System.Reflection;

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

            //Execute our plugin against a target thatcontains the accountnumber attribute will throw exception
            Assert.Throws<InvalidPluginExecutionException>(() => fakedContext.ExecutePluginWithTarget<AccountNumberPlugin>(target));

        }

        [Fact]
        public void When_the_followup_plugin_is_executed_for_an_account_it_should_create_a_new_task()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly(); //Needed to be able to return early bound entities

            var guid1 = Guid.NewGuid();
            var target = new Entity("account") { Id = guid1 };

            fakedContext.ExecutePluginWithTarget<FollowupPlugin>(target);

            //The plugin creates a followup activity, check that that one exists
            var tasks = (from t in fakedContext.CreateQuery<Task>()
                             select t).ToList();

            Assert.True(tasks.Count == 1);
            Assert.True(tasks[0]["subject"].Equals("Send e-mail to the new customer."));
        }

        [Fact]
        public void When_the_followup_plugin_is_executed_for_an_account_after_create_it_should_create_a_new_task_with_a_regardingid()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly(); //Needed to be able to return early bound entities

            var guid1 = Guid.NewGuid();
            var target = new Entity("account") { Id = guid1 };

            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);

            ParameterCollection outputParameters = new ParameterCollection();
            outputParameters.Add("id", guid1);

            fakedContext.ExecutePluginWith<FollowupPlugin>(inputParameters,outputParameters,null,null);

            //The plugin creates a followup activity, check that that one exists
            var tasks = (from t in fakedContext.CreateQuery<Task>()
                         select t).ToList();

            Assert.True(tasks.Count == 1);
            Assert.True(tasks[0].Subject.Equals("Send e-mail to the new customer."));
            Assert.True(tasks[0].RegardingObjectId != null && tasks[0].RegardingObjectId.Id.Equals(guid1));
        }

        [Fact]
        public void When_A_Plugin_Is_Executed_Configurations_Can_Be_Used()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("contact") { Id = guid1 };

            var inputParams = new ParameterCollection { new KeyValuePair<string, object>("Target", target) };

            var unsecureConfiguration = "Unsecure Configuration";
            var secureConfiguration = "Secure Configuration";

            //Execute our plugin against the selected target
            var plugCtx = fakedContext.GetDefaultPluginContext();
            plugCtx.InputParameters = inputParams;

            fakedContext.ExecutePluginWithConfigurations<ConfigurationPlugin>(plugCtx, unsecureConfiguration, secureConfiguration);

            Assert.True(target.Contains("unsecure"));
            Assert.True(target.Contains("secure"));
            Assert.Equal((string)target["unsecure"], unsecureConfiguration);
            Assert.Equal((string)target["secure"], secureConfiguration);
        }

        [Fact]
        public void When_A_Plugin_Is_Executed_With_Configurations_But_Does_Not_Implement_Constructor_Throw_Exception()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("contact") { Id = guid1 };

            var inputParams = new ParameterCollection { new KeyValuePair<string, object>("Target", target) };

            var unsecureConfiguration = "Unsecure Configuration";
            var secureConfiguration = "Secure Configuration";

            //Execute our plugin against the selected target
            var plugCtx = fakedContext.GetDefaultPluginContext();
            plugCtx.InputParameters = inputParams;

            Assert.Throws<ArgumentException>(() => fakedContext.ExecutePluginWithConfigurations<FollowupPlugin>(plugCtx, unsecureConfiguration, secureConfiguration));
        }

        [Fact]
        public void When_initializing_the_context_with_Properties_Plugins_Can_Access_It()
        {
            var context = new XrmFakedContext();

            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", new Entity());

            var plugCtx = context.GetDefaultPluginContext();
            plugCtx.MessageName = "Create";
            plugCtx.InputParameters = inputParameters;

            Assert.DoesNotThrow(() => context.ExecutePluginWith<TestContextPlugin>(plugCtx));
        }

        [Fact]
        public void When_Passing_In_No_Properties_Plugins_Only_Get_Default_Values()
        {
            var context = new XrmFakedContext();

            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", new Entity());

            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                InputParameters = inputParameters,
                UserId = Guid.NewGuid(),
                InitiatingUserId = Guid.NewGuid()
            };

            //Parameters are defaulted now...
            Assert.DoesNotThrow(() => context.ExecutePluginWith<TestContextPlugin>(pluginContext));

            pluginContext = new XrmFakedPluginExecutionContext()
            {
                InputParameters = inputParameters,
                MessageName = "Create",
                InitiatingUserId = Guid.NewGuid()
            };


            Assert.DoesNotThrow(() => context.ExecutePluginWith<TestContextPlugin>(pluginContext));

            pluginContext = new XrmFakedPluginExecutionContext()
            {
                InputParameters = inputParameters,
                MessageName = "Update",
                UserId = Guid.NewGuid()
            };

            Assert.DoesNotThrow(() => context.ExecutePluginWith<TestContextPlugin>(pluginContext));
        }
    }
}
