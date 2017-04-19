﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class ExecuteFetchRequestExecutor : IFakeMessageExecutor
    {
        private Dictionary<string, int?> _typeCodes = new Dictionary<string, int?>();

        public bool CanExecute(OrganizationRequest request)
        {
            return request is ExecuteFetchRequest;
        }


        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var executeFetchRequest = (ExecuteFetchRequest)request;


            if (executeFetchRequest.FetchXml == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "You need to provide FetchXml value");
            }

            var service = ctx.GetFakedOrganizationService();

            var retrieveMultiple = new RetrieveMultipleRequest()
            {
                Query = new FetchExpression(executeFetchRequest.FetchXml)
            };
            var queryResult = (service.Execute(retrieveMultiple) as RetrieveMultipleResponse).EntityCollection;



            XDocument doc = new XDocument(new XElement("resultset",
                new XAttribute("morerecords", Convert.ToInt16(queryResult.MoreRecords))));
            if (queryResult.PagingCookie != null)
            {
                doc.Root.Add(new XAttribute("paging-cookie", queryResult.PagingCookie));
            }
            
            foreach(var row in queryResult.Entities)
            {
                doc.Root.Add(CreateXmlResult(row, ctx));
            }

            var response = new ExecuteFetchResponse
            {
                Results = new ParameterCollection
                                 {

                                    { "FetchXmlResult", doc.ToString() }
                                 }
            };

            return response;
        }

        private XElement CreateXmlResult(Entity entity, XrmFakedContext ctx)
        {
            XElement row = new XElement("result");
            var formattedValues = entity.FormattedValues;

            foreach (var entAtt in entity.Attributes)
            {
                XElement attributeValueElement = AttributeValueToFetchResult(entAtt, formattedValues, ctx);

                

                if (attributeValueElement == null)
                    continue;
                row.Add(attributeValueElement);
            }
            return row;
        }

        public XElement AttributeValueToFetchResult(KeyValuePair<string,object> entAtt, FormattedValueCollection formattedValues, XrmFakedContext  ctx)
        {
            XElement attributeValueElement;
            if (entAtt.Value == null)
                return null;
            if (entAtt.Value is DateTime?)
            {
                attributeValueElement = XElement.Parse(String.Format("<{0} date=\"{1:yyyy-MM-dd}\" time=\"{1:hh:mm tt}\">{1:yyyy-MM-ddTHH:mm:sszz:00}</{0}>", entAtt.Key, entAtt.Value));
            }
            else if (entAtt.Value is EntityReference)
            {
                var entRef = (EntityReference)entAtt.Value;
                if (!_typeCodes.ContainsKey(entRef.LogicalName))
                {
                    var entType =  RetrieveEntityRequestExecutor.GetEntityProxyType(entRef.LogicalName, ctx);
                    var typeCode = entType.GetField("EntityTypeCode").GetValue(null);

                    _typeCodes.Add(entRef.LogicalName, (int?)typeCode);
                }

                attributeValueElement = XElement.Parse(String.Format("<{0} dsc=\"0\" yomi=\"{1}\" name=\"{1}\" type=\"{3}\">{2:D}</{0}>", entAtt.Key, entRef.Name, entRef.Id.ToString().ToUpper(), _typeCodes[entRef.LogicalName]));
            }
            else if (entAtt.Value is bool?)
            {

                var boolValue = (bool?)entAtt.Value;

                var formattedValue = boolValue.ToString();
                if (formattedValues.ContainsKey(entAtt.Key))
                    formattedValue = formattedValues[entAtt.Key];
                attributeValueElement = XElement.Parse(String.Format("<{0} name=\"{1}\">{2}</{0}>", entAtt.Key, formattedValue, Convert.ToInt16(boolValue)));
            }
            else if (entAtt.Value is OptionSetValue)
            {

                var osValue = (OptionSetValue)entAtt.Value;

                var formattedValue = osValue.Value.ToString();
                if (formattedValues.ContainsKey(entAtt.Key))
                    formattedValue = formattedValues[entAtt.Key];
                attributeValueElement = XElement.Parse(String.Format("<{0} name=\"{1}\" formattedvalue=\"{2}\">{2}</{0}>", entAtt.Key, formattedValue, osValue.Value));
            }
            else if (entAtt.Value is Enum)
            {

                var osValue = (Enum)entAtt.Value;

                var formattedValue = osValue.ToString();
                if (formattedValues.ContainsKey(entAtt.Key))
                    formattedValue = formattedValues[entAtt.Key];
                attributeValueElement = XElement.Parse(String.Format("<{0} name=\"{1}\" formattedvalue=\"{2}\">{2}</{0}>", entAtt.Key, formattedValue, osValue));
            }
            else if (entAtt.Value is Money)
            {

                var moneyValue = (Money)entAtt.Value;

                var formattedValue = moneyValue.Value.ToString();
                if (formattedValues.ContainsKey(entAtt.Key))
                    formattedValue = formattedValues[entAtt.Key];
                attributeValueElement = XElement.Parse(String.Format("<{0} formattedvalue=\"{1}\">{2:0.##}</{0}>", entAtt.Key, formattedValue, moneyValue.Value));
            }
            else if (entAtt.Value is decimal?)
            {

                var decimalVal = (decimal?)entAtt.Value;

                attributeValueElement = XElement.Parse(String.Format("<{0}>{1:0.####}</{0}>", entAtt.Key, decimalVal.Value));
            }
            else if (entAtt.Value is AliasedValue)
            {
                var alliasedVal = entAtt.Value as AliasedValue;
                attributeValueElement = AttributeValueToFetchResult(new KeyValuePair<string, object>(entAtt.Key, alliasedVal.Value), formattedValues, ctx);
            }
            else if (entAtt.Value is Guid)
            {
                attributeValueElement = XElement.Parse(String.Format("<{0}>{1}</{0}>", entAtt.Key, entAtt.Value.ToString().ToUpper())); ;
            }
            else
            {
                attributeValueElement = XElement.Parse(String.Format("<{0}>{1}</{0}>", entAtt.Key, entAtt.Value));
            }
            return attributeValueElement;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(ExecuteFetchRequest);
        }
    }
}
