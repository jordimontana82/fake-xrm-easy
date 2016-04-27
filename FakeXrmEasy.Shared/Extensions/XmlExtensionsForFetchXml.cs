using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace FakeXrmEasy.Extensions.FetchXml
{
    public static class XmlExtensionsForFetchXml
    {
        public static bool IsFetchXmlNodeValid(this XElement elem)
        {
            switch (elem.Name.LocalName)
            {
                case "filter":
                    return elem.GetAttribute("type") != null;

                case "fetch":
                    return true;

                case "entity":
                    return elem.GetAttribute("name") != null;

                case "all-attributes":
                    return true;

                case "attribute":
                    return elem.GetAttribute("name") != null;

                case "link-entity":
                    return elem.GetAttribute("name") != null
                            && elem.GetAttribute("from") != null
                            && elem.GetAttribute("to") != null;

                case "order":
                    return elem.GetAttribute("attribute") != null
                           && elem.GetAttribute("descending") != null;

                case "condition":
                    return elem.GetAttribute("attribute") != null
                           && elem.GetAttribute("operator") != null;

                default:
                    throw new Exception(string.Format("Node {0} is not a valid FetchXml node or it doesn't have the required attributes", elem.Name.LocalName));
            }
        }

        public static XAttribute GetAttribute(this XElement elem, string sAttributeName)
        {
            return elem.Attributes().Where(a => a.Name.LocalName.Equals(sAttributeName)).FirstOrDefault();
        }

        public static ColumnSet ToColumnSet(this XElement el)
        {
            var allAttributes = el.Elements()
                    .Where(e => e.Name.LocalName.Equals("all-attributes"))
                    .FirstOrDefault();

            if (allAttributes != null)
            {
                return new ColumnSet(true);
            }

            var attributes = el.Elements()
                                .Where(e => e.Name.LocalName.Equals("attribute"))
                                .Select(e => e.GetAttribute("name").Value)
                                .ToList()
                                .ToArray();


            return new ColumnSet(attributes);
        }

        public static ColumnSet ToColumnSet(this XDocument xlDoc)
        {
            //Check if all-attributes exist
            return xlDoc.Elements()   //fetch
                    .Elements()
                    .FirstOrDefault()
                    .ToColumnSet();
        }

        public static FilterExpression ToCriteria(this XDocument xlDoc)
        {
            return xlDoc.Elements()   //fetch
                    .Elements()     //entity
                    .Elements()     //child nodes of entity
                    .Where(el => el.Name.LocalName.Equals("filter"))
                    .Select(el => el.ToFilterExpression())
                    .FirstOrDefault();
        }

        public static LinkEntity ToLinkEntity(this XElement el)
        {
            //Create this node
            var linkEntity = new LinkEntity();

            linkEntity.LinkFromEntityName = el.GetAttribute("name").Value;
            linkEntity.LinkFromAttributeName = el.GetAttribute("from").Value;
            linkEntity.LinkToAttributeName = el.GetAttribute("to").Value;
            linkEntity.LinkToEntityName = el.Parent.GetAttribute("name").Value;

            if(el.GetAttribute("alias") != null)
            {
                linkEntity.EntityAlias = el.GetAttribute("alias").Value;
            }

            //Process other link entities recursively
            var convertedLinkEntityNodes = el.Elements()
                                    .Where(e => e.Name.LocalName.Equals("link-entity"))
                                    .Select(e => e.ToLinkEntity())
                                    .ToList();

            foreach(var le in convertedLinkEntityNodes)
            {
                linkEntity.LinkEntities.Add(le);
            }

            //Process column sets
            linkEntity.Columns = el.ToColumnSet();

            //Process filter
            linkEntity.LinkCriteria = el.Elements()
                                        .Where(e => e.Name.LocalName.Equals("filter"))
                                        .Select(e => e.ToFilterExpression())
                                        .FirstOrDefault();

            return linkEntity;
        }

        public static List<LinkEntity> ToLinkEntities(this XDocument xlDoc)
        {
            return xlDoc.Elements()   //fetch
                    .Elements()     //entity
                    .Elements()     //child nodes of entity
                    .Where(el => el.Name.LocalName.Equals("link-entity"))
                    .Select(el => el.ToLinkEntity())
                    .ToList();
        }

        public static List<OrderExpression> ToOrderExpressionList(this XDocument xlDoc)
        {
            var orderByElements = xlDoc.Elements()   //fetch
                                .Elements()     //entity
                                .Elements()     //child nodes of entity
                                .Where(el => el.Name.LocalName.Equals("order"))
                                .Select(el =>
                                        new OrderExpression
                                        {
                                            AttributeName = el.GetAttribute("attribute").Value,
                                            OrderType = el.GetAttribute("descending").Value.Equals("true") ?
                                                            OrderType.Descending : OrderType.Ascending
                                        })
                                .ToList();

            return orderByElements;
        }

        public static FilterExpression ToFilterExpression(this XElement elem)
        {
            var filterExpression = new FilterExpression();

            filterExpression.FilterOperator = elem.GetAttribute("type").Value.Equals("and") ? 
                                                    LogicalOperator.And : LogicalOperator.Or;

            //Process other filters recursively
            var otherFilters = elem
                        .Elements() //child nodes of this filter
                        .Where(el => el.Name.LocalName.Equals("filter"))
                        .Select(el => el.ToFilterExpression())
                        .ToList();


            //Process conditions
            var conditions = elem
                        .Elements() //child nodes of this filter
                        .Where(el => el.Name.LocalName.Equals("condition"))
                        .Select(el => el.ToConditionExpression())
                        .ToList();

            foreach (var c in conditions)
                filterExpression.AddCondition(c);

            foreach (var f in otherFilters)
                filterExpression.AddFilter(f);

            return filterExpression;
        }

        public static ConditionExpression ToConditionExpression(this XElement elem)
        {
            var conditionExpression = new ConditionExpression();

            var attributeName = elem.GetAttribute("attribute").Value;
            ConditionOperator op = ConditionOperator.Equal;

            string value = null;
            if (elem.GetAttribute("value") != null)
            {
                value = elem.GetAttribute("value").Value;
            }

            switch (elem.GetAttribute("operator").Value)
            {
                case "eq":
                    op = ConditionOperator.Equal;
                    break;
                case "ne":
                    op = ConditionOperator.NotEqual;
                    break;
                case "like":
                    op = ConditionOperator.Like;

                    if(value != null)
                    {
                        if (value.StartsWith("%") && !value.EndsWith("%"))
                            op = ConditionOperator.EndsWith;
                        else if (!value.StartsWith("%") && value.EndsWith("%"))
                            op = ConditionOperator.BeginsWith;

                        value = value.Replace("%", "");
                    }
                    break;

                case "not-like":
                    op = ConditionOperator.NotLike;

                    if (value != null)
                    {
                        if (value.StartsWith("%") && !value.EndsWith("%"))
                            op = ConditionOperator.DoesNotEndWith;
                        else if (!value.StartsWith("%") && value.EndsWith("%"))
                            op = ConditionOperator.DoesNotBeginWith;

                        value = value.Replace("%", "");
                    }
                    break;

                default:
                    throw PullRequestException.FetchXmlOperatorNotImplemented(elem.GetAttribute("operator").Value);
            }

            //Process values
            object[] values = null;
            if(value != null)
            {
                return new ConditionExpression(attributeName, op, value);
            }

            return new ConditionExpression(attributeName, op, values);

        }
    }
}
