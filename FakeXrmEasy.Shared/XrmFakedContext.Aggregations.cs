using FakeXrmEasy.Extensions.FetchXml;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext
    {
        internal static bool IsAggregateFetchXml(XDocument xmlDoc)
        {
            var attr = xmlDoc.Root.GetAttribute("aggregate");
            return attr != null && Convert.ToBoolean(attr.Value);
        }

        internal static List<Entity> ProcessAggregateFetchXml(XrmFakedContext ctx, XDocument xmlDoc, List<Entity> resultOfQuery)
        {
            // Validate that <all-attributes> is not present,
            // that all attributes have groupby or aggregate, and an alias,
            // and that there is exactly 1 groupby.
            if (RetrieveFetchXmlNode(xmlDoc, "all-attributes") != null)
            {
                throw new Exception("Can't have <all-attributes /> present when using aggregate");
            }

            var ns = xmlDoc.Root.GetDefaultNamespace();

            var aggregates = new List<FetchAggregate>();
            var groups = new List<FetchGrouping>();


            foreach (var attr in xmlDoc.Descendants(ns + "attribute"))
            {
                //TODO: Find entity alias. Handle aliasedvalue in the query result.

                var alias = attr.GetAttribute("alias")?.Value;
                var logicalName = attr.GetAttribute("name")?.Value;
                if (string.IsNullOrEmpty("alias"))
                {
                    throw new Exception("Missing alias for attribute in aggregate fetch xml");
                }
                if (string.IsNullOrEmpty("name"))
                {
                    throw new Exception("Missing name for attribute in aggregate fetch xml");
                }

                if (Convert.ToBoolean(attr.GetAttribute("groupby")?.Value))
                {
                    var dategrouping = attr.GetAttribute("dategrouping")?.Value;
                    if (dategrouping != null)
                    {
                        DateGroupType t;
                        if(!Enum.TryParse(dategrouping, true, out t))
                        {
                            throw new Exception("Unknown dategrouping value '" + dategrouping + "'");
                        }
                        groups.Add(new DateTimeGroup()
                        {
                            Type = t,
                            OutputAlias = alias,
                            Attribute = logicalName
                        });
                    }
                    else
                    {
                        groups.Add(new SimpleValueGroup()
                        {
                            OutputAlias = alias,
                            Attribute = logicalName
                        });
                    }
                }
                else
                {
                    var agrFn = attr.GetAttribute("aggregate")?.Value;
                    FetchAggregate newAgr = null;

                    switch (agrFn?.ToLower())
                    {
                        case "count":
                            var distinct = attr.GetAttribute("distinct")?.Value;
                            if (distinct != null && Convert.ToBoolean(distinct))
                            {
                                newAgr = new CountDistinctAggregate();
                            }
                            else
                            {
                                newAgr = new CountAggregate();
                            }
                            break;

                        case "min":
                            newAgr = new MinAggregate();
                            break;

                        case "max":
                            newAgr = new MaxAggregate();
                            break;
                        case "avg":
                            newAgr = new AvgAggregate();
                            break;
                        case "sum":
                            newAgr = new SumAggregate();
                            break;
                        default:
                            throw new Exception("Unknown aggregate function '" + agrFn + "'");
                    }


                    newAgr.OutputAlias = alias;
                    newAgr.Attribute = logicalName;
                    aggregates.Add(newAgr);
                }
            }


            // Group by the groupBy-attribute            
            var grouped = resultOfQuery.GroupBy(e =>
            {
                return groups
                    .Select(g => g.Process(e))
                    .ToArray();
            }, new ArrayComparer());

            // Perform aggregates in each group
            var result = new List<Entity>();
            foreach (var g in grouped)
            {
                var firstInGroup = g.First();

                var ent = new Entity();
                ent.LogicalName = firstInGroup.LogicalName;

                // Find the group values
                for (var rule = 0; rule < groups.Count; ++rule)
                {
                    ent[groups[rule].OutputAlias] = new AliasedValue(null, groups[rule].Attribute, g.Key[rule]);
                }

                // Aggregate the remaining values
                foreach (var agg in aggregates)
                {
                    ent[agg.OutputAlias] = new AliasedValue(null, agg.Attribute, agg.Process(g));
                }

                result.Add(ent);
            }

            // TODO: order

            return result;
        }

        abstract class FetchAggregate
        {
            public string Attribute { get; set; }
            public string OutputAlias { get; set; }
            public object Process(IEnumerable<Entity> entities)
            {
                return AggregateValues(entities.Select(e =>
                    e.Contains(Attribute) ? e[Attribute] : null
                ));
            }

            public abstract object AggregateValues(IEnumerable<object> values);
        }

        class CountAggregate : FetchAggregate
        {
            public override object AggregateValues(IEnumerable<object> values)
            {
                return values.Count();
            }
        }

        class CountDistinctAggregate : FetchAggregate
        {
            public override object AggregateValues(IEnumerable<object> values)
            {
                return values.Distinct().Count();
            }
        }

        class MinAggregate : FetchAggregate
        {
            public override object AggregateValues(IEnumerable<object> values)
            {
                return values.Select(x => x ?? 0).Min();
            }
        }

        class MaxAggregate : FetchAggregate
        {
            public override object AggregateValues(IEnumerable<object> values)
            {
                return values.Select(x => x ?? 0).Max();
            }
        }

        class AvgAggregate : FetchAggregate
        {
            public override object AggregateValues(IEnumerable<object> values)
            {
                var lst = values.ToList();
                // TODO: Check these cases in CRM proper
                if (lst.Count == 0) return null;
                if (lst.All(x => x == null)) return null;

                var firstValue = lst.Where(x => x != null).First();
                var valType = firstValue.GetType();

                if (valType == typeof(decimal) || valType == typeof(decimal?))
                {
                    return lst.Average(x => x as decimal? ?? 0m);
                }
                if (valType == typeof(Money))
                {
                    return lst.Average(x => (x as Money)?.Value ?? 0m);
                }

                if (valType == typeof(int) || valType == typeof(int?))
                {
                    return lst.Average(x => x as int? ?? 0);
                }

                if (valType == typeof(float) || valType == typeof(float?))
                {
                    return lst.Average(x => x as float? ?? 0f);
                }

                if (valType == typeof(double) || valType == typeof(double?))
                {
                    return lst.Average(x => x as double? ?? 0d);
                }

                throw new Exception("Unhndled property type '" + valType.FullName + "' in 'avg' aggregate");
            }
        }

        class SumAggregate : FetchAggregate
        {
            public override object AggregateValues(IEnumerable<object> values)
            {
                var lst = values.ToList();
                // TODO: Check these cases in CRM proper
                if (lst.Count == 0) return null;
                if (lst.All(x => x == null)) return null;

                var firstValue = lst.Where(x => x != null).First();
                var valType = firstValue.GetType();

                if (valType == typeof(decimal) || valType == typeof(decimal?))
                {
                    return lst.Sum(x => x as decimal? ?? 0m);
                }
                if (valType == typeof(Money))
                {
                    return new Money(lst.Sum(x => (x as Money)?.Value ?? 0m));
                }

                if (valType == typeof(int) || valType == typeof(int?))
                {
                    return lst.Sum(x => x as int? ?? 0);
                }

                if (valType == typeof(float) || valType == typeof(float?))
                {
                    return lst.Sum(x => x as float? ?? 0f);
                }

                if (valType == typeof(double) || valType == typeof(double?))
                {
                    return lst.Sum(x => x as double? ?? 0d);
                }

                throw new Exception("Unhndled property type '" + valType.FullName + "' in 'sum' aggregate");
            }
        }

        abstract class FetchGrouping
        {
            public string Attribute { get; set; }
            public string OutputAlias { get; set; }
            public IComparable Process(Entity entity)
            {
                var attr = entity.Contains(Attribute) ? entity[Attribute] : null;
                return FindGroupValue(attr);
            }
            public abstract IComparable FindGroupValue(object attributeValue);
        }

        /// <summary>
        /// Used to compare array of objects, in order to group by a variable number of conditions.
        /// </summary>
        class ArrayComparer : IEqualityComparer<IComparable[]>
        {
            public bool Equals(IComparable[] x, IComparable[] y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IComparable[] obj)
            {
                return string.Join(",", obj as IEnumerable<IComparable>).GetHashCode();
            }
        }

        class SimpleValueGroup : FetchGrouping
        {
            public override IComparable FindGroupValue(object attributeValue)
            {
                return attributeValue as IComparable;
            }
        }

        enum DateGroupType
        {
            DateTime,
            Day,
            Week,
            Month,
            Quarter,
            Year
        }

        class DateTimeGroup : FetchGrouping
        {
            public DateGroupType Type { get; set; }

            public override IComparable FindGroupValue(object attributeValue)
            {
                if (attributeValue == null) return null;

                if (!(attributeValue is DateTime || attributeValue is DateTime?))
                {
                    throw new Exception("Can only do date grouping of DateTime values");
                }

                var d = attributeValue as DateTime?;

                switch (Type)
                {
                    case DateGroupType.DateTime:
                        return d;
                    case DateGroupType.Day:
                        return d?.Day;
                    case DateGroupType.Week:
                        var cal = System.Globalization.DateTimeFormatInfo.InvariantInfo;
                        return cal.Calendar.GetWeekOfYear(d.Value, cal.CalendarWeekRule, cal.FirstDayOfWeek);
                    case DateGroupType.Month:
                        return d?.Month;
                    case DateGroupType.Quarter:
                        return (d?.Month + 2) / 3;
                    case DateGroupType.Year:
                        return d?.Year;
                    default:
                        throw new Exception("Unhandled date group type");
                }
            }
        }
    }
}
