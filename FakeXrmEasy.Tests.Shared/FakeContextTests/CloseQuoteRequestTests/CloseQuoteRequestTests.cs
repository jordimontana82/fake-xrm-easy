using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.CloseQuoteRequestTests
{
    public enum QuoteStatusCode
    {
        Draft = 1,
        Active = 2,
        Open = 3,
        Won = 4,
        Lost = 5,
        Canceled = 6,
        Revised = 7
    }

    public class CloseQuoteRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new CloseQuoteRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public static void When_valid_request_supplied_then_updates_quote_statecode_and_statuscode()
        {
            var executor = new CloseQuoteRequestExecutor();
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var quote = new Entity
            {
                LogicalName = "quote",
                Id = Guid.NewGuid(),
                Attributes = new AttributeCollection
                {
                    { "statuscode", new OptionSetValue((int)QuoteStatusCode.Draft) }
                }
            };

            context.Initialize(new[]
            {
                quote
            });

            var newStatus = QuoteStatusCode.Lost;

            var request = new CloseQuoteRequest
            {
                QuoteClose = new Entity
                {
                    LogicalName = "quoteclose",
                    Attributes = new AttributeCollection
                    {
                        { "quoteid", new EntityReference(quote.LogicalName, quote.Id) }
                    }
                },
                Status = new OptionSetValue((int)newStatus)
            };

            executor.Execute(request, context);

            var updatedQuote = service.Retrieve(quote.LogicalName, quote.Id, new ColumnSet(true));

            Assert.Equal(3, updatedQuote.GetAttributeValue<OptionSetValue>("statecode")?.Value); // Assert Quote statecode is 'Closed'
            Assert.Equal((int)newStatus, updatedQuote.GetAttributeValue<OptionSetValue>("statuscode")?.Value);
        }

        [Fact]
        public static void When_valid_request_supplied_then_creates_quoteclose_activity()
        {
            var executor = new CloseQuoteRequestExecutor();
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var quote = new Entity
            {
                LogicalName = "quote",
                Id = Guid.NewGuid(),
                Attributes = new AttributeCollection
                {
                    { "statuscode", new OptionSetValue((int)QuoteStatusCode.Draft) }
                }
            };

            context.Initialize(new[]
            {
                quote
            });

            var newStatus = QuoteStatusCode.Lost;

            var request = new CloseQuoteRequest
            {
                QuoteClose = new Entity
                {
                    LogicalName = "quoteclose",
                    Attributes = new AttributeCollection
                    {
                        { "quoteid", new EntityReference(quote.LogicalName, quote.Id) }
                    }
                },
                Status = new OptionSetValue((int)newStatus)
            };

            executor.Execute(request, context);

            IEnumerable<Entity> quoteCloseActivities = service.RetrieveMultiple(new QueryExpression("quoteclose") { ColumnSet = new ColumnSet(true) }).Entities;
            Assert.Equal(1, quoteCloseActivities.Count());

            Entity quoteCloseActivity = quoteCloseActivities.Single();
            Assert.Equal(4211, quoteCloseActivity.GetAttributeValue<OptionSetValue>("activitytypecode")?.Value); // Assert activity type is 'Quote Close'
            Assert.Equal(quote.Id, quoteCloseActivity.GetAttributeValue<EntityReference>("quoteid").Id); // Assert QuoteClose refers to the Quote
            Assert.Equal(quote.Id, quoteCloseActivity.GetAttributeValue<EntityReference>("regardingobjectid").Id); // Assert QuoteClose is regarding the Quote
            Assert.Equal(1, quoteCloseActivity.GetAttributeValue<OptionSetValue>("statecode")?.Value); // Assert QuoteClose statecode is 'Completed'
            Assert.Equal(2, quoteCloseActivity.GetAttributeValue<OptionSetValue>("statuscode")?.Value); // Assert QuoteClose statuscode is 'Completed'
        }
    }
}