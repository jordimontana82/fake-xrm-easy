using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using FakeXrmEasy.Extensions;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class InsertOptionValueRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is InsertOptionValueRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as InsertOptionValueRequest;

            if (req.Label == null)
                throw new Exception("Label must not be null");

            if (string.IsNullOrWhiteSpace(req.Label.LocalizedLabels[0].Label))
            {
                throw new Exception("Label must not be empty");
            }

            if (string.IsNullOrEmpty(req.OptionSetName)
                && (string.IsNullOrEmpty(req.EntityLogicalName)
                || string.IsNullOrEmpty(req.AttributeLogicalName)))
            {
                throw new Exception("At least OptionSetName or both the EntityName and AttributeName must not be provided");
            }

            string key = "";
            if (!string.IsNullOrWhiteSpace(req.OptionSetName))
                key = req.OptionSetName;
            else
                key = string.Format("{0}#{1}", req.EntityLogicalName, req.AttributeLogicalName);

            if (!ctx.OptionSetValuesMetadata.ContainsKey(key))
                ctx.OptionSetValuesMetadata.Add(key, new OptionSetMetadata());

            var optionSetMetadata = ctx.OptionSetValuesMetadata[key];
            optionSetMetadata.Options.Add(new OptionMetadata()
            {
                MetadataId = Guid.NewGuid(),
                Value = req.Value,
                Label = req.Label
            });

            if (!string.IsNullOrEmpty(req.EntityLogicalName))
            {
                var entityMetadata = ctx.GetEntityMetadataByName(req.EntityLogicalName);
                if (entityMetadata != null)
                {
                    var attribute = entityMetadata
                            .Attributes
                            .FirstOrDefault(a => a.LogicalName == req.AttributeLogicalName);

                    if (attribute == null)
                    {
                        throw new Exception($"You are trying to insert an option set value for entity '{req.EntityLogicalName}' with entity metadata associated but the attribute '{req.AttributeLogicalName}' doesn't exist in metadata");
                    }

                    if (!(attribute is EnumAttributeMetadata))
                    {
                        throw new Exception($"You are trying to insert an option set value for entity '{req.EntityLogicalName}' with entity metadata associated but the attribute '{req.AttributeLogicalName}' is not a valid option set field (not a subtype of EnumAttributeMetadata)");
                    }

                    var enumAttribute = attribute as EnumAttributeMetadata;

                    var options = enumAttribute.OptionSet == null ? new OptionMetadataCollection() : enumAttribute.OptionSet.Options;

                    options.Add(new OptionMetadata(req.Label, req.Value));
                    enumAttribute.OptionSet = new OptionSetMetadata(options);

                    entityMetadata.SetAttribute(enumAttribute);
                    ctx.SetEntityMetadata(entityMetadata);
                }
            }
            return new InsertOptionValueResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(InsertOptionValueRequest);
        }
    }
}