using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveMultipleRequestExecutor: IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveMultipleRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest req, XrmFakedContext ctx)
        {
            var request = req as RetrieveMultipleRequest;
            if (request.Query is QueryExpression)
            {
                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, request.Query as QueryExpression);
                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(linqQuery.ToList()) }
                                 }
                };
                return response;
            }
            else if (request.Query is FetchExpression)
            {
                var fetchXml = (request.Query as FetchExpression).Query;
                var queryExpression = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, queryExpression);
                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(linqQuery.ToList()) }
                                 }
                };
                return response;
            }
            else if (request.Query is QueryByAttribute)
            {
                //We instantiate a QueryExpression to be executed as we have the implementation done already
                var query = request.Query as QueryByAttribute;
                var qe = new QueryExpression(query.EntityName);

                qe.ColumnSet = query.ColumnSet;
                qe.Criteria = new FilterExpression();
                for (var i = 0; i < query.Attributes.Count; i++)
                {
                    qe.Criteria.AddCondition(new ConditionExpression(query.Attributes[i], ConditionOperator.Equal, query.Values[i]));
                }

                //QueryExpression now done... execute it!
                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, qe as QueryExpression);
                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(linqQuery.ToList()) }
                                 }
                };
                return response;
            }
            else
                throw PullRequestException.NotImplementedOrganizationRequest(request.Query.GetType());
        }
    }
}
