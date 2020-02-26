using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Diagnostics;
using System.Linq;
using FakeXrmEasy.Extensions;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class InsertStatusValueRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is InsertStatusValueRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as InsertStatusValueRequest;

            Debug.Assert(req != null, nameof(req) + " != null");
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

            var key = !string.IsNullOrWhiteSpace(req.OptionSetName) ? req.OptionSetName : $"{req.EntityLogicalName}#{req.AttributeLogicalName}";

            if (!ctx.StatusAttributeMetadata.ContainsKey(key))
                ctx.StatusAttributeMetadata.Add(key, new StatusAttributeMetadata());

            var statusValuesMetadata = ctx.StatusAttributeMetadata[key];
            //statusValuesMetadata.
            statusValuesMetadata.OptionSet = new OptionSetMetadata();
            statusValuesMetadata.OptionSet.Options.Add(new StatusOptionMetadata()
            {
                MetadataId = Guid.NewGuid(),
                Value = req.Value,
                Label = req.Label,
                State = req.StateCode,
                Description = req.Label
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
                    
                    options.Add(new StatusOptionMetadata(){Value = req.Value, Label = req.Label, State = req.StateCode, Description = req.Label});

                    enumAttribute.OptionSet = new OptionSetMetadata(options);                    

                    entityMetadata.SetAttribute(enumAttribute);
                    ctx.SetEntityMetadata(entityMetadata);
                }
            }
            return new InsertStatusValueResponse();
        }       

        public Type GetResponsibleRequestType()
        {
            return typeof(InsertStatusValueRequest);
        }
    }
}