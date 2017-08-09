using Crm;
using FakeXrmEasy.Tests.CodeActivitiesForTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestCodeActivities
    {
        [Fact]
        public void When_the_add_activity_is_executed_the_right_sum_is_returned()
        {
            var fakedContext = new XrmFakedContext();

            //Inputs
            var inputs = new Dictionary<string, object>() {
                { "firstSummand", 2 },
                { "secondSummand", 3 }
            };

            var result = fakedContext.ExecuteCodeActivity<AddActivity>(inputs);

            Assert.True(((int)result["result"]).Equals(5));
        }

        [Fact]
        public void When_the_create_task_activity_is_executed_a_task_is_created_in_the_context()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var guid1 = Guid.NewGuid();
            var account = new Account() { Id = guid1 };
            fakedContext.Initialize(new List<Entity>() {
                account
            });

            //Inputs
            var inputs = new Dictionary<string, object>() {
                { "inputEntity", account.ToEntityReference() }
            };

            var result = fakedContext.ExecuteCodeActivity<CreateTaskActivity>(inputs);

            //The wf creates an activity, so make sure it is created
            var tasks = (from t in fakedContext.CreateQuery<Task>()
                         select t).ToList();

            //The activity creates a taks
            Assert.True(tasks.Count == 1);

            var output = result["taskCreated"] as EntityReference;

            //Task created contains the account passed as the regarding Id
            Assert.True(tasks[0].RegardingObjectId != null && tasks[0].RegardingObjectId.Id.Equals(guid1));

            //Same task created is returned
            Assert.Equal(output.Id, tasks[0].Id);
        }

        [Fact]
        public void When_the_add_activity_with_constant_is_executed_without_a_specific_instance_result_is_the_two_summands()
        {
            var fakedContext = new XrmFakedContext();

            //Inputs
            var inputs = new Dictionary<string, object>() {
                { "firstSummand", 2 },
                { "secondSummand", 3 }
            };

            var result = fakedContext.ExecuteCodeActivity<AddActivityWithConstant>(inputs);

            Assert.True(((int)result["result"]).Equals(5));
        }

        [Fact]
        public void When_the_add_activity_with_constant_is_executed_with_a_specific_instance_result_is_the_two_summands_plus_constant()
        {
            var fakedContext = new XrmFakedContext();

            //Inputs
            var inputs = new Dictionary<string, object>() {
                { "firstSummand", 2 },
                { "secondSummand", 3 }
            };

            AddActivityWithConstant codeActivity = new AddActivityWithConstant();
            codeActivity.Constant = 69;

            var result = fakedContext.ExecuteCodeActivity<AddActivityWithConstant>(inputs, codeActivity);

            Assert.True(((int)result["result"]).Equals(5 + 69));
        }

        [Fact]
        public void When_passing_a_custom_workflow_activity_context_injected_property_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var wfContext = fakedContext.GetDefaultWorkflowContext();
            wfContext.MessageName = "Update";

            //Inputs
            var inputs = new Dictionary<string, object>();

            CheckContextPropertyActivity codeActivity = new CheckContextPropertyActivity();

            var result = fakedContext.ExecuteCodeActivity<CheckContextPropertyActivity>(wfContext, inputs, codeActivity);

            Assert.True(((string)result["MessageName"]).Equals("Update"));
        }
    }
}