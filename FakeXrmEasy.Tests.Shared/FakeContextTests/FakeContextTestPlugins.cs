using Crm;
using FakeItEasy;
using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

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

            fakedContext.ExecutePluginWith<FollowupPlugin>(inputParameters, outputParameters, null, null);

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
        public void When_A_Plugin_Is_Executed_With_And_Instance_That_one_is_executed()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("contact") { Id = guid1 };

            TestPropertiesPlugin plugin =
                new TestPropertiesPlugin()
                { Property = "Some test" };

            var inputParams = new ParameterCollection { new KeyValuePair<string, object>("Target", target) };

            //Execute our plugin against the selected target
            var plugCtx = fakedContext.GetDefaultPluginContext();
            plugCtx.InputParameters = inputParams;

            fakedContext.ExecutePluginWith(plugCtx, plugin);
            Assert.Equal("Property Updated", plugin.Property);
        }

        [Fact]
        public void When_A_Plugin_Is_Executed_With_Configurations_And_Instance_That_one_is_executed()
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

            var ex = Record.Exception(() => context.ExecutePluginWith<TestContextPlugin>(plugCtx));
            Assert.Null(ex);
        }

        [Fact]
        public void When_OrganizationName_Set()
        {
            var fakedContext = new XrmFakedContext();
            var pluginCtx = fakedContext.GetDefaultPluginContext();
            pluginCtx.OutputParameters = new ParameterCollection();
            pluginCtx.OrganizationName = "TestOrgName";
            fakedContext.ExecutePluginWith<TestContextOrgNamePlugin>(pluginCtx);

            Assert.True(pluginCtx.OutputParameters.ContainsKey("OrgName"));
            Assert.Equal("TestOrgName", pluginCtx.OutputParameters["OrgName"]);
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
            var ex = Record.Exception(() => context.ExecutePluginWith<TestContextPlugin>(pluginContext));
            Assert.Null(ex);

            pluginContext = new XrmFakedPluginExecutionContext()
            {
                InputParameters = inputParameters,
                MessageName = "Create",
                InitiatingUserId = Guid.NewGuid()
            };

            ex = Record.Exception(() => context.ExecutePluginWith<TestContextPlugin>(pluginContext));
            Assert.Null(ex);

            pluginContext = new XrmFakedPluginExecutionContext()
            {
                InputParameters = inputParameters,
                MessageName = "Update",
                UserId = Guid.NewGuid()
            };

            ex = Record.Exception(() => context.ExecutePluginWith<TestContextPlugin>(pluginContext));
            Assert.Null(ex);
        }

        [Fact]
        public void When_executing_a_plugin_which_inherits_from_iplugin_it_does_compile()
        {
            var fakedContext = new XrmFakedContext();
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<MyPlugin>(new Entity());
        }

        [Fact]
        public void When_executing_a_plugin_primaryentityname_exists_in_the_context()
        {
            var context = new XrmFakedContext();

            var pluginContext = context.GetDefaultPluginContext();
            pluginContext.PrimaryEntityName = "Account";
            pluginContext.MessageName = "Create";
            pluginContext.Stage = 20;

            var entity = new Entity();
            context.ExecutePluginWith<PreAccountCreate>(pluginContext);

            Assert.True(true);
        }

        [Fact]
        public void When_getting_a_default_context_shared_variables_can_be_accessed_from_a_plugin()
        {
            var context = new XrmFakedContext();

            var pluginContext = context.GetDefaultPluginContext();
            pluginContext.SharedVariables.Add("key", "somevalue");

            var ex = Record.Exception(() => context.ExecutePluginWith<TestSharedVariablesPropertyPlugin>(pluginContext));
            Assert.Null(ex);
        }

        [Fact]
        public void When_executing_a_plugin_theres_no_need_to_pass_a_default_plugin_context_if_the_plugin_doesnt_need_it()
        {
            var context = new XrmFakedContext();
            var entity = new Entity();
            context.ExecutePluginWith<AccountNumberPlugin>();
            Assert.True(true);
        }

        [Fact]
        public void When_passing_preentityimages_it_gets_added_to_context()
        {
            // arrange
            var context = new XrmFakedContext();
            var target = new Entity();

            EntityImageCollection preEntityImages = new EntityImageCollection();
            preEntityImages.Add("PreImage", new Entity());

            // act
            context.ExecutePluginWithTargetAndPreEntityImages<EntityImagesPlugin>(target, preEntityImages);

            // assert
            EntityImageCollection postImagesReturned = target["PostEntityImages"] as EntityImageCollection;

            if (postImagesReturned.Count > 0)
                throw new Exception("PostEntityImages should not be set.");

            EntityImageCollection preImagesReturned = target["PreEntityImages"] as EntityImageCollection;

            Assert.Equal(1, preImagesReturned?.Count);
            Assert.IsType(typeof(Entity), preImagesReturned["PreImage"]);
        }

        [Fact]
        public void When_passing_postentityimages_it_gets_added_to_context()
        {
            // arrange
            var context = new XrmFakedContext();
            var target = new Entity();

            EntityImageCollection postEntityImages = new EntityImageCollection();
            postEntityImages.Add("PostImage", new Entity());

            // act
            context.ExecutePluginWithTargetAndPostEntityImages<EntityImagesPlugin>(target, postEntityImages);

            // assert
            EntityImageCollection preImagesReturned = target["PreEntityImages"] as EntityImageCollection;

            if (preImagesReturned.Count > 0)
                throw new Exception("PreImages should not be set.");

            EntityImageCollection postImagesReturned = target["PostEntityImages"] as EntityImageCollection;

            Assert.Equal(1, postImagesReturned?.Count);
            Assert.IsType(typeof(Entity), postImagesReturned["PostImage"]);
        }
    }
}