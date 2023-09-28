using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System.Reflection;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveMetadataChangesRequestExecutor : IFakeMessageExecutor
    {
        
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveMetadataChangesRequest;
        }
        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as RetrieveMetadataChangesRequest;

            var response = new RetrieveMetadataChangesResponse()
            {
                Results = new ParameterCollection
                {
                    ["EntityMetadata"] = ApplyFilter(req.Query, ctx.EntityMetadata.Values)
                }
            };

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveMetadataChangesRequest);
        }

        private EntityMetadataCollection ApplyFilter(EntityQueryExpression qry, IEnumerable<EntityMetadata> metadata)
        {
            var results = new EntityMetadataCollection();
            results.AddRange(metadata
                .Where(e => IsMatch(e, qry.Criteria))
                .Select(e => Project(e, qry, qry.Properties)));

            return results;
        }

        private bool IsMatch(object obj, MetadataFilterExpression criteria)
        {
            if (criteria == null)
                return true;

            if ((criteria.Conditions == null || criteria.Conditions.Count == 0) && (criteria.Filters == null || criteria.Filters.Count == 0))
                return true;

            if (criteria.Conditions != null)
            {
                foreach (var condition in criteria.Conditions)
                {
                    var conditionMatch = IsMatch(obj, condition);

                    if (criteria.FilterOperator == LogicalOperator.And && !conditionMatch)
                        return false;
                    else if (criteria.FilterOperator == LogicalOperator.Or && conditionMatch)
                        return true;
                }
            }

            if (criteria.Filters != null)
            {
                foreach (var filter in criteria.Filters)
                {
                    var filterMatch = IsMatch(obj, filter);

                    if (criteria.FilterOperator == LogicalOperator.And && !filterMatch)
                        return false;
                    else if (criteria.FilterOperator == LogicalOperator.Or && filterMatch)
                        return true;
                }
            }

            if (criteria.FilterOperator == LogicalOperator.And)
                return true;

            return false;
        }

        private bool IsMatch(object obj, MetadataConditionExpression condition)
        {
            var prop = obj.GetType().GetProperty(condition.PropertyName);

            if (prop == null)
                throw new Exception($"Unknown property {condition.PropertyName}");

            var value = prop.GetValue(obj, null);

            switch (condition.ConditionOperator)
            {
                case MetadataConditionOperator.Equals:
                    return value == condition.Value || (value != null && condition.Value != null && value.Equals(condition.Value));

                case MetadataConditionOperator.GreaterThan:
                    return value != null && condition.Value != null && ((IComparable)value).CompareTo(condition.Value) > 0;

                case MetadataConditionOperator.In:
                    foreach (var v in (Array) condition.Value)
                    {
                        if (value == v || (value != null && v != null && value.Equals(v)))
                            return true;
                    }
                    return false;

                case MetadataConditionOperator.LessThan:
                    return value != null && condition.Value != null && ((IComparable)value).CompareTo(condition.Value) < 0;

                case MetadataConditionOperator.NotEquals:
                    return (value == null ^ condition.Value == null) || (value != null && condition.Value != null && !value.Equals(condition.Value));

                case MetadataConditionOperator.NotIn:
                    foreach (var v in (Array)condition.Value)
                    {
                        if ((value == null ^ v == null) || (value != null && v != null && value.Equals(v)))
                            return false;
                    }
                    return true;

                default:
                    throw new Exception($"Unknown condition operator {condition.ConditionOperator}");
            }
        }

        private T Project<T>(T obj, EntityQueryExpression qry, MetadataPropertiesExpression properties)
        {
            var props = obj.GetType().GetProperties();

            if (properties != null && !properties.AllProperties)
            {
                props = props
                    .Where(p => properties.PropertyNames.Contains(p.Name))
                    .ToArray();
            }

            var result = Activator.CreateInstance(obj.GetType());

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj, null);

                if (prop.PropertyType == typeof(AttributeMetadata[]) && qry.AttributeQuery != null)
                {
                    var attrs = (AttributeMetadata[])value;

                    value = attrs
                        .Where(a => IsMatch(a, qry.AttributeQuery.Criteria))
                        .Select(a => Project(a, qry, qry.AttributeQuery.Properties))
                        .ToArray();
                }
                else if (prop.PropertyType == typeof(OneToManyRelationshipMetadata[]) && qry.RelationshipQuery != null)
                {
                    var rels = (OneToManyRelationshipMetadata[])value;

                    value = rels
                        .Where(r => IsMatch(r, qry.RelationshipQuery.Criteria))
                        .Select(r => Project(r, qry, qry.RelationshipQuery.Properties))
                        .ToArray();
                }
                else if (prop.PropertyType == typeof(ManyToManyRelationshipMetadata[]) && qry.RelationshipQuery != null)
                {
                    var rels = (ManyToManyRelationshipMetadata[])value;

                    value = rels
                        .Where(r => IsMatch(r, qry.RelationshipQuery.Criteria))
                        .Select(r => Project(r, qry, qry.RelationshipQuery.Properties))
                        .ToArray();
                }
                else if (prop.PropertyType == typeof(Label) && qry.LabelQuery != null)
                {
                    var label = (Label)value;

                    var locLabels = label.LocalizedLabels.ToArray();
                    label.LocalizedLabels.Clear();
                    label.LocalizedLabels.AddRange(locLabels.Where(l => qry.LabelQuery.FilterLanguages.Contains(l.LanguageCode)));
                }

                prop.SetValue(result, value, null);
            }

            return (T) result;
        }
    }
}
