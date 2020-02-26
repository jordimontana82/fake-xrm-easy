#if FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9

using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.ExecuteTransationTests
{
    public class ExecuteTransactionTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new ExecuteTransactionExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_execute_is_called_all_requests_are_executed()
        {
            var context = new XrmFakedContext();
            var executor = new ExecuteTransactionExecutor();
            var req = new ExecuteTransactionRequest()
            {
                Requests = new OrganizationRequestCollection()
                {
                    new CreateRequest() { Target = new Entity("contact") },
                    new CreateRequest() { Target = new Entity("contact") },
                    new CreateRequest() { Target = new Entity("contact") }
                }
            };

            var response = executor.Execute(req, context) as ExecuteTransactionResponse;
            var contacts = context.CreateQuery("contact").ToList();
            Assert.Equal(0, response.Responses.Count);
            Assert.Equal(3, contacts.Count);
        }

        [Fact]
        public void When_execute_is_called_all_requests_are_executed_with_responses()
        {
            var context = new XrmFakedContext();
            var executor = new ExecuteTransactionExecutor();
            var req = new ExecuteTransactionRequest()
            {
                ReturnResponses = true,
                Requests = new OrganizationRequestCollection()
                {
                    new CreateRequest() { Target = new Entity("contact") },
                    new CreateRequest() { Target = new Entity("contact") },
                    new CreateRequest() { Target = new Entity("contact") }
                }
            };

            var response = executor.Execute(req, context) as ExecuteTransactionResponse;
            var contacts = context.CreateQuery("contact").ToList();
            Assert.Equal(3, response.Responses.Count);
            Assert.Equal(3, contacts.Count);
        }
    }
}

#endif