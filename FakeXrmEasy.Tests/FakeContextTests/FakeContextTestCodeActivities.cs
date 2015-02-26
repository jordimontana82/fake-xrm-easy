using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

using FakeXrmEasy.Tests.CodeActivitiesForTesting;
using Crm;
using System.Reflection;

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
    }
}
