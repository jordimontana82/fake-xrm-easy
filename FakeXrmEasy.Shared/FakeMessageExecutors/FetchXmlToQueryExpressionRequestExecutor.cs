using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class FetchXmlToQueryExpressionRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is FetchXmlToQueryExpressionRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as FetchXmlToQueryExpressionRequest;
            var service = ctx.GetOrganizationService();
            FetchXmlToQueryExpressionResponse response = new FetchXmlToQueryExpressionResponse();
            response["Query"] = XrmFakedContext.TranslateFetchXmlDocumentToQueryExpression(ctx, XrmFakedContext.ParseFetchXml(req.FetchXml)); ;
            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(FetchXmlToQueryExpressionRequest);
        }
    }
}
