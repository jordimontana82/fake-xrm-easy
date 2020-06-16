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

            Entity update = new Entity
            {
                Id = quote.Id,
                LogicalName = quote.LogicalName,
                Attributes = new AttributeCollection
                {
                    { "statuscode", winQuoteRequest.Status }
                }
            };

            var service = ctx.GetOrganizationService();

            service.Update(update);

            return new WinQuoteResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(WinQuoteRequest);
        }
    }
}