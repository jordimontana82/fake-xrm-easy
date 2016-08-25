using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.AssociateRequestTests
{
    public class AssociateRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new AssociateRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_request_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new AssociateRequestExecutor();
            AssociateRequest req = null;
            Assert.Throws<Exception>(() => executor.Execute(req, context));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_target_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new AssociateRequestExecutor();
            var req = new AssociateRequest() { Target = null, Relationship = new Relationship("fakeRelationship") };
            context.AddRelationship("fakeRelationship", new XrmFakedRelationship());
            Assert.Throws<Exception>(() => executor.Execute(req, context));
        }
    }
}
