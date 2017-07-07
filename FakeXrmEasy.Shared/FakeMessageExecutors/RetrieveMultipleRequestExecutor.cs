using FakeXrmEasy.Extensions;
using FakeXrmEasy.Extensions.FetchXml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveMultipleRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveMultipleRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest req, XrmFakedContext ctx)
        {
            var request = req as RetrieveMultipleRequest;
            List<Entity> list = null;
            PagingInfo pageInfo = null;
            int? topCount = null;

            if (request.Query is QueryExpression)
            {
                var qe = request.Query as QueryExpression;

                pageInfo = qe.PageInfo;
                topCount = qe.TopCount;

                //need to request 1 extra to get a fill if there are more records
                if (topCount != null)
                    qe.TopCount = topCount + 1;

                if (qe.PageInfo.Count > 0)
                    qe.TopCount = qe.PageInfo.Count + 1;

                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, request.Query as QueryExpression);
                list = linqQuery.ToList();
            }
            else if (request.Query is FetchExpression)
            {
                var fetchXml = (request.Query as FetchExpression).Query;
                var xmlDoc = XrmFakedContext.ParseFetchXml(fetchXml);
                var queryExpression = XrmFakedContext.TranslateFetchXmlDocumentToQueryExpression(ctx, xmlDoc);

                pageInfo = queryExpression.PageInfo;
                topCount = queryExpression.TopCount;

                //need to request 1 extra to get a fill if there are more records
                if (topCount != null)
                    queryExpression.TopCount = topCount + 1;
                if (queryExpression.PageInfo.Count > 0)
                {
                    queryExpression.TopCount = queryExpression.PageInfo.Count + 1;
                }

                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, queryExpression);
                list = linqQuery.ToList();

                if (xmlDoc.IsAggregateFetchXml())
                {
                    list = XrmFakedContext.ProcessAggregateFetchXml(ctx, xmlDoc, list);
                }
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

                pageInfo = qe.PageInfo;
                topCount = qe.TopCount;

                //need to request 1 extra to be able check if there are more records
                if (topCount != null)
                    qe.TopCount = topCount + 1;
                if (qe.PageInfo.Count > 0)
                {
                    qe.TopCount = qe.PageInfo.Count + 1;
                }

                //QueryExpression now done... execute it!
                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, qe as QueryExpression);
                list = linqQuery.ToList();
            }
            else
                throw PullRequestException.NotImplementedOrganizationRequest(request.Query.GetType());

            list.ForEach(e => e.ApplyDateBehaviour(ctx));
            list.ForEach(e => PopulateFormattedValues(e));
            var recordCount = list.Count();
            var pageSize = recordCount;
            int pageNumber = 1;
            if (pageInfo != null && pageInfo.PageNumber > 0 && pageInfo.Count > 0)
            {
                pageSize = pageInfo.Count;
                pageNumber = pageInfo.PageNumber;
            }
            else if (topCount != null)
                pageSize = topCount.Value;

            var response = new RetrieveMultipleResponse
            {
                Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(list.Take(pageSize).ToList()) }
                                 }
            };
            response.EntityCollection.MoreRecords = recordCount > pageSize;
            if (response.EntityCollection.MoreRecords)
            {
                var first = response.EntityCollection.Entities.First();
                var last = response.EntityCollection.Entities.Last();
                response.EntityCollection.PagingCookie = String.Format(
                    "<cookie page=\"{0}\"><{1}id last=\"{2}\" first=\"{3}\" /></cookie>",
                    pageNumber, first.LogicalName, last.Id.ToString("B").ToUpper(), first.Id.ToString("B").ToUpper());
            }

            return response;
        }

        /// <summary>
        /// Populates the formmated values property of this entity record based on the proxy types
        /// </summary>
        /// <param name="e"></param>
        protected void PopulateFormattedValues(Entity e)
        {
            //Iterate through attributes and retrieve formatted values based on type
            foreach (var attKey in e.Attributes.Keys)
            {
                var value = e[attKey];
                string formattedValue = "";
                if (value != null)
                {
                    bool bShouldAdd = false;
                    formattedValue = GetFormattedValueForValue(value, out bShouldAdd);
                    if (bShouldAdd)
                    {
                        e.FormattedValues.Add(attKey, formattedValue);
                    }
                }
            }
        }

        protected string GetFormattedValueForValue(object value, out bool bShouldAddFormattedValue)
        {
            bShouldAddFormattedValue = false;
            string sFormattedValue = "";

            if (value is Enum)
            {
                // Retrieve the enum type
                sFormattedValue = Enum.GetName(value.GetType(), value);
                bShouldAddFormattedValue = true;
            }
            else if (value is AliasedValue)
            {
                return GetFormattedValueForValue((value as AliasedValue)?.Value, out bShouldAddFormattedValue);
            }

            return sFormattedValue;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveMultipleRequest);
        }
    }
}