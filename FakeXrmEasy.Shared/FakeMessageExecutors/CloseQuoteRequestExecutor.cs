using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class CloseQuoteRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is CloseQuoteRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var closeRequest = request as CloseQuoteRequest;

            if (closeRequest == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(CloseQuoteRequest)} must not be null");
            }

            if (closeRequest.Status == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(CloseQuoteRequest.Status)} must not be null");
            }

            if (closeRequest.QuoteClose == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(CloseQuoteRequest.QuoteClose)} must not be null");
            }

            var quote = closeRequest.QuoteClose.GetAttributeValue<EntityReference>("quoteid");

            if (quote == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(CloseQuoteRequest.QuoteClose)} must have a 'quoteid' EntityReference detailing the Quote to update");
            }

            if (!quote.LogicalName.Equals("quote", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{nameof(CloseQuoteRequest.QuoteClose)} must have a 'quoteid' EntityReference that refers to a 'quote'; got '{quote.LogicalName}'");
            }

            // Build the update to perform on the Quote
            var quoteUpdate = new Entity
            {
                Id = quote.Id,
                LogicalName = "quote",
                Attributes = new AttributeCollection
                {
                    { "statecode", new OptionSetValue(3) }, // 'Closed' statecode
                    { "statuscode", closeRequest.Status }
                }
            };

            closeRequest.QuoteClose["regardingobjectid"] = quote;
            closeRequest.QuoteClose["activitytypecode"] = new OptionSetValue(4211); // 'QuoteClose' activity type

            var service = ctx.GetOrganizationService();

            // Update the Quote state and status
            service.Update(quoteUpdate);

            // Create the related QuoteClose activity
            Guid quoteCloseId = service.Create(closeRequest.QuoteClose);

            // Mark the QuoteClose activity as 'Completed'
            service.Update(new Entity
            {
                Id = quoteCloseId,
                LogicalName = closeRequest.QuoteClose.LogicalName,
                Attributes = new AttributeCollection
                {
                    { "statecode", new OptionSetValue(1) }, // 'Completed' QuoteClose activity statecode
                    { "statuscode", new OptionSetValue(2) } // 'Completed' QuoteClose activity statuscode
                }
            });

            return new CloseQuoteResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(CloseQuoteRequest);
        }
    }
}