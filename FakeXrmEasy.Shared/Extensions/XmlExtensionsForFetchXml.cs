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
        public static XAttribute GetAttribute(this XElement elem, string sAttributeName)
        {
            return elem.Attributes().Where(a => a.Name.LocalName.Equals(sAttributeName)).FirstOrDefault();
        }

        public static ColumnSet ToColumnSet(this XDocument xlDoc)
        {
            //Check if all-attributes exist
            var allAttributes = xlDoc.Elements()   //fetch
                    .Elements()     //entity
                    .Elements()     //child nodes of entity
                    .Where(el => el.Name.LocalName.Equals("all-attributes"))
                    .FirstOrDefault();

            if (allAttributes != null)
            {
                return new ColumnSet(true);
            }

            var attributes = xlDoc.Elements()   //fetch
                                .Elements()     //entity
                                .Elements()     //child nodes of entity
                                .Where(el => el.Name.LocalName.Equals("attribute"))
                                .Select(el => el.GetAttribute("name").Value)
                                .ToList()
                                .ToArray();


            return new ColumnSet(attributes);

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
