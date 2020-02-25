using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

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
                throw new Exception("You did not pass a CloseQuoteRequest");
            }

            var quoteClose = closeRequest.QuoteClose;

            if (quoteClose == null)
            {
                throw new Exception("QuoteClose is mandatory");
            }

            var quoteId = quoteClose.GetAttributeValue<EntityReference>("quoteid");

            if (quoteId == null)
            {
                throw new Exception("Quote ID is not set on QuoteClose, but is required");
            }

            var update = new Entity
            {
                Id = quoteId.Id,
                LogicalName = "quote",
                Attributes = new AttributeCollection
                {
                    { "statuscode", closeRequest.Status }
                }
            };

            var service = ctx.GetOrganizationService();

            service.Update(update);

            return new CloseQuoteResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(CloseQuoteRequest);
        }
    }
}