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
                var list = linqQuery.ToList();
                list.ForEach(e => PopulateFormattedValues(e));

                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(list) }
                                 }
                };
                return response;
            }
            else if (request.Query is FetchExpression)
            {
                var fetchXml = (request.Query as FetchExpression).Query;
                var queryExpression = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

                var linqQuery = XrmFakedContext.TranslateQueryExpressionToLinq(ctx, queryExpression);
                var list = linqQuery.ToList();
                list.ForEach(e => PopulateFormattedValues(e));

                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(list) }
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
                var list = linqQuery.ToList();
                list.ForEach(e => PopulateFormattedValues(e));

                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                                 {
                                    { "EntityCollection", new EntityCollection(list) }
                                 }
                };
                return response;
            }
            else
                throw PullRequestException.NotImplementedOrganizationRequest(request.Query.GetType());
        }

        /// <summary>
        /// Populates the formmated values property of this entity record based on the proxy types
        /// </summary>
        /// <param name="e"></param>
        protected void PopulateFormattedValues(Entity e)
        {
            //Iterate through attributes and retrieve formatted values based on type
            foreach(var attKey in e.Attributes.Keys)
            {
                var value = e[attKey];
                string formattedValue = "";
                if(value != null)
                {
                    bool bShouldAdd = false;
                    formattedValue = GetFormattedValueForValue(value, out bShouldAdd);
                    if(bShouldAdd)
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

            if(value is Enum)
            {
                // Retrieve the enum type
                sFormattedValue = Enum.GetName(value.GetType(), value);
                bShouldAddFormattedValue = true;
            }

            return sFormattedValue;
        }
    }
}
