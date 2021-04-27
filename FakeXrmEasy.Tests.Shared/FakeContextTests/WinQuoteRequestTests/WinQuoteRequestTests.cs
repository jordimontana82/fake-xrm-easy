using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.WinQuoteRequestTests
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

    public class WinQuoteRequestTests
    {
        [Fact]
        public static void When_diffrent_request_supplied_then_returns_false()
        {
            var executor = new WinQuoteRequestExecutor();

            Assert.False(executor.CanExecute(new RetrieveMultipleRequest()));
        }

        [Fact]
        public static void When_null_quoteclose_supplied_then_throws()
        {
            var executor = new WinQuoteRequestExecutor();
            var context = new XrmFakedContext();
            var request = new WinQuoteRequest
            {
                QuoteClose = null,
                Status = new OptionSetValue((int)QuoteStatusCode.Draft)
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(request, context));
        }

        [Fact]
        public static void When_null_quoteid_on_quoteclose_supplied_then_throws()
        {
            var executor = new WinQuoteRequestExecutor();
            var context = new XrmFakedContext();
            var request = new WinQuoteRequest
            {
                QuoteClose = new Entity
                {
                    LogicalName = "quoteclose",
                    Attributes = new AttributeCollection
                    {
                        { "quoteid", null }
                    }
                },
                Status = new OptionSetValue((int)QuoteStatusCode.Draft)
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(request, context));
        }

        [Fact]
        public static void When_nonquote_quoteid_on_quoteclose_supplied_then_throws()
        {
            var executor = new WinQuoteRequestExecutor();
            var context = new XrmFakedContext();
            var request = new WinQuoteRequest
            {
                QuoteClose = new Entity
                {
                    LogicalName = "quoteclose",
                    Attributes = new AttributeCollection
                    {
                        { "quoteid", new EntityReference("apples", Guid.NewGuid()) }
                    }
                },
                Status = new OptionSetValue((int)QuoteStatusCode.Draft)
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(request, context));
        }

        [Fact]
        public static void When_null_status_supplied_then_throws()
        {
            var executor = new WinQuoteRequestExecutor();
            var context = new XrmFakedContext();

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

            var request = new WinQuoteRequest
            {
                QuoteClose = new Entity
                {
                    LogicalName = "quoteclose",
                    Attributes = new AttributeCollection
                    {
                        { "quoteid", new EntityReference(quote.LogicalName, quote.Id) }
                    }
                },
                Status = null
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(request, context));
        }

        [Fact]
        public static void When_valid_request_supplied_then_updates_quote_statecode_and_statuscode()
        {
            var executor = new WinQuoteRequestExecutor();
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

            var newStatus = QuoteStatusCode.Won;

            var request = new WinQuoteRequest
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

            Assert.Equal(2, updatedQuote.GetAttributeValue<OptionSetValue>("statecode")?.Value); // Assert Quote statecode is 'Won'
            Assert.Equal((int)newStatus, updatedQuote.GetAttributeValue<OptionSetValue>("statuscode")?.Value);
        }

        [Fact]
        public static void When_valid_request_supplied_then_creates_quoteclose_activity()
        {
            var executor = new WinQuoteRequestExecutor();
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

            var newStatus = QuoteStatusCode.Won;

            var request = new WinQuoteRequest
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