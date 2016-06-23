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

                case "value":
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

        public static int? ToTopCount(this XElement el)
        {
            var countAttr = el.GetAttribute("count");
            if (countAttr == null) return null;

            int iCount;
            if (!int.TryParse(countAttr.Value, out iCount))
                throw new Exception("Count attribute in fetch node must be an integer");

            return iCount;
        }

        public static ColumnSet ToColumnSet(this XDocument xlDoc)
        {
            //Check if all-attributes exist
            return xlDoc.Elements()   //fetch
                    .Elements()
                    .FirstOrDefault()
                    .ToColumnSet();
        }


        public static int? ToTopCount(this XDocument xlDoc)
        {
            //Check if all-attributes exist
            return xlDoc.Elements()   //fetch
                    .FirstOrDefault()
                    .ToTopCount();
        }

        public static FilterExpression ToCriteria(this XDocument xlDoc, XrmFakedContext ctx)
        {
            return xlDoc.Elements()   //fetch
                    .Elements()     //entity
                    .Elements()     //child nodes of entity
                    .Where(el => el.Name.LocalName.Equals("filter"))
                    .Select(el => el.ToFilterExpression(ctx))
                    .FirstOrDefault();
        }

        public static string GetAssociatedEntityNameForConditionExpression(this XElement el)
        {
            
            while(el != null)
            {
                var parent = el.Parent;
                if(parent.Name.LocalName.Equals("entity") || parent.Name.LocalName.Equals("link-entity"))
                {
                    return parent.GetAttribute("name").Value;
                }
                el = parent;
            }

            return null;
        }

        public static LinkEntity ToLinkEntity(this XElement el, XrmFakedContext ctx)
        {
            //Create this node
            var linkEntity = new LinkEntity();

            linkEntity.LinkFromEntityName = el.Parent.GetAttribute("name").Value;
            linkEntity.LinkFromAttributeName = el.GetAttribute("from").Value;
            linkEntity.LinkToAttributeName = el.GetAttribute("to").Value;
            linkEntity.LinkToEntityName = el.GetAttribute("name").Value;  

            if(el.GetAttribute("alias") != null)
            {
                linkEntity.EntityAlias = el.GetAttribute("alias").Value;
            }

            //Process other link entities recursively
            var convertedLinkEntityNodes = el.Elements()
                                    .Where(e => e.Name.LocalName.Equals("link-entity"))
                                    .Select(e => e.ToLinkEntity(ctx))
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
                                        .Select(e => e.ToFilterExpression(ctx))
                                        .FirstOrDefault();

            return linkEntity;
        }

        public static List<LinkEntity> ToLinkEntities(this XDocument xlDoc, XrmFakedContext ctx)
        {
            return xlDoc.Elements()   //fetch
                    .Elements()     //entity
                    .Elements()     //child nodes of entity
                    .Where(el => el.Name.LocalName.Equals("link-entity"))
                    .Select(el => el.ToLinkEntity(ctx))
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

        public static FilterExpression ToFilterExpression(this XElement elem, XrmFakedContext ctx)
        {
            var filterExpression = new FilterExpression();

            filterExpression.FilterOperator = elem.GetAttribute("type").Value.Equals("and") ? 
                                                    LogicalOperator.And : LogicalOperator.Or;

            //Process other filters recursively
            var otherFilters = elem
                        .Elements() //child nodes of this filter
                        .Where(el => el.Name.LocalName.Equals("filter"))
                        .Select(el => el.ToFilterExpression(ctx))
                        .ToList();


            //Process conditions
            var conditions = elem
                        .Elements() //child nodes of this filter
                        .Where(el => el.Name.LocalName.Equals("condition"))
                        .Select(el => el.ToConditionExpression(ctx))
                        .ToList();

            foreach (var c in conditions)
                filterExpression.AddCondition(c);

            foreach (var f in otherFilters)
                filterExpression.AddFilter(f);

            return filterExpression;
        }

        public static object ToValue(this XElement elem, XrmFakedContext ctx, string sEntityName, string sAttributeName)
        {
            return GetConditionExpressionValueCast(elem.Value, ctx, sEntityName, sAttributeName);
        }

        public static ConditionExpression ToConditionExpression(this XElement elem, XrmFakedContext ctx)
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
                case "neq":
                    op = ConditionOperator.NotEqual;
                    break;
                case "begins-with":
                    op = ConditionOperator.BeginsWith;
                    break;
                case "not-begin-with":
                    op = ConditionOperator.DoesNotBeginWith;
                    break;
                case "ends-with":
                    op = ConditionOperator.EndsWith;
                    break;
                case "not-end-with":
                    op = ConditionOperator.DoesNotEndWith;
                    break;
                case "in":
                    op = ConditionOperator.In;
                    break;
                case "not-in":
                    op = ConditionOperator.NotIn;
                    break;
                case "null":
                    op = ConditionOperator.Null;
                    break;
                case "not-null":
                    op = ConditionOperator.NotNull;
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

                case "gt":
                    op = ConditionOperator.GreaterThan;
                    break;

                case "gte":
                    op = ConditionOperator.GreaterEqual;
                    break;

                case "lt":
                    op = ConditionOperator.LessThan;
                    break;

                case "lte":
                    op = ConditionOperator.LessEqual;
                    break;


                default:
                    throw PullRequestException.FetchXmlOperatorNotImplemented(elem.GetAttribute("operator").Value);
            }

            //Process values
            object[] values = null;

            var entityName = GetAssociatedEntityNameForConditionExpression(elem);

            //Find values inside the condition expression, if apply
            values = elem
                        .Elements() //child nodes of this filter
                        .Where(el => el.Name.LocalName.Equals("value"))
                        .Select(el => el.ToValue(ctx, entityName, attributeName))
                        .ToArray();


            //Otherwise, a single value was used
            if (value != null)
            {
                
                return new ConditionExpression(attributeName, op, GetConditionExpressionValueCast(value, ctx, entityName, attributeName));
            }

            return new ConditionExpression(attributeName, op, values);

        }

        public static object GetConditionExpressionValueCast(string value, XrmFakedContext ctx, string sEntityName, string sAttributeName)
        {
            //Try parsing a guid
            Guid gOut = Guid.Empty;
            if (Guid.TryParse(value, out gOut))
                return gOut;

            //Try checking if it is a numeric value, cause, from the fetchxml it 
            //would be impossible to know the real typed based on the string value only
            // ex: "123" might compared as a string, or, as an int, it will depend on the attribute
            //    data type, therefore, in this case we do need to use proxy types

            bool bIsNumeric = false;
            double dblValue = 0.0;

            if (double.TryParse(value, out dblValue))
                return dblValue;
            else
            {
                if (ctx.ProxyTypesAssembly == null)
                    throw new PullRequestException("When using arithmetic operators in Fetch a ProxyTypesAssembly must be used in order to guess types");


            }

            //Default case
            return value;
        }
    }
}
