using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.CloseQuoteRequestTests
{
    public class CloseQuoteRequestTests
    {
        private static readonly OptionSetValue StateDraft = new OptionSetValue(0);
        private static readonly OptionSetValue StateClosed = new OptionSetValue(3);
        private static readonly OptionSetValue StatusInProgress = new OptionSetValue(1);
        private static readonly OptionSetValue StatusCanceled = new OptionSetValue(6);

        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new CloseQuoteRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void Should_Change_Status_When_Closing()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var quote = new Entity("quote")
            {
                Id = Guid.NewGuid(),
                ["statecode"] = StateDraft,
                ["statuscode"] = StatusInProgress
            };

            context.Initialize(new[]
            {
                quote
            });

            var executor = new CloseQuoteRequestExecutor();

            var req = new CloseQuoteRequest
            {
                QuoteClose = new Entity("quoteclose")
                {
                    ["quoteid"] = quote.ToEntityReference()
                },
                Status = StatusCanceled
            };

            executor.Execute(req, context);

            quote = service.Retrieve("quote", quote.Id, new ColumnSet(true));

            Assert.Equal(StateClosed, quote.GetAttributeValue<OptionSetValue>("statecode"));
            Assert.Equal(StatusCanceled, quote.GetAttributeValue<OptionSetValue>("statuscode"));
        }
    }
}