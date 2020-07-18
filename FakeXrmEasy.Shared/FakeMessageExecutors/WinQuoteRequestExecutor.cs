using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class WinQuoteRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is WinQuoteRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var winQuoteRequest = request as WinQuoteRequest;

            if (winQuoteRequest == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(WinQuoteRequest)} must not be null");
            }

            if (winQuoteRequest.Status == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(WinQuoteRequest.Status)} must not be null");
            }

            if (winQuoteRequest.QuoteClose == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(WinQuoteRequest.QuoteClose)} must not be null");
            }

            var quote = winQuoteRequest.QuoteClose.GetAttributeValue<EntityReference>("quoteid");

            if (quote == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(WinQuoteRequest.QuoteClose)} must have a 'quoteid' EntityReference detailing the Quote to update");
            }

            if (!quote.LogicalName.Equals("quote", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(WinQuoteRequest.QuoteClose)} must have a 'quoteid' EntityReference that refers to a 'quote'; got '{quote.LogicalName}'");
            }

            // Build the update to perform on the Quote
            Entity quoteUpdate = new Entity
            {
                Id = quote.Id,
                LogicalName = quote.LogicalName,
                Attributes = new AttributeCollection
                {
                    { "statecode", new OptionSetValue(2) }, // 'Won' statecode
                    { "statuscode", winQuoteRequest.Status }
                }
            };

            winQuoteRequest.QuoteClose["regardingobjectid"] = quote;
            winQuoteRequest.QuoteClose["activitytypecode"] = new OptionSetValue(4211); // 'QuoteClose' activity type

            var service = ctx.GetOrganizationService();

            // Update the Quote state and status
            service.Update(quoteUpdate);

            // Create the related QuoteClose activity
            Guid quoteCloseId = service.Create(winQuoteRequest.QuoteClose);

            // Mark the QuoteClose activity as 'Completed'
            service.Update(new Entity
            {
                Id = quoteCloseId,
                LogicalName = winQuoteRequest.QuoteClose.LogicalName,
                Attributes = new AttributeCollection
                {
                    { "statecode", new OptionSetValue(1) }, // 'Completed' QuoteClose activity statecode
                    { "statuscode", new OptionSetValue(2) } // 'Completed' QuoteClose activity statuscode
                }
            });

            return new WinQuoteResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(WinQuoteRequest);
        }
    }
}