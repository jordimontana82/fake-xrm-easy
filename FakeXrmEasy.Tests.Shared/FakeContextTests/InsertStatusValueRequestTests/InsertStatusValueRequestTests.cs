using Crm;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using Xunit;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk.Messages;
using System.Linq;


namespace FakeXrmEasy.Tests.FakeContextTests.InsertStatusValueRequestTests
{
    public class InsertStatusValueRequestTests
    {
        [Fact]
        public void Should_update_entity_metadata_with_status()
        {
            var label = "dummy status";
            var attributeName = "statuscode";
            var value = 1;
            var statecode = 1;
            var ctx = new XrmFakedContext();

            var entityMetadata = new EntityMetadata()
            {
                LogicalName = "contact"
            };
            StatusAttributeMetadata enumAttribute = new StatusAttributeMetadata() {LogicalName = attributeName};
            entityMetadata.SetAttributeCollection(new List<AttributeMetadata>() {enumAttribute});

            ctx.InitializeMetadata(entityMetadata);

            var req = new InsertStatusValueRequest()
            {
                EntityLogicalName = Contact.EntityLogicalName,
                AttributeLogicalName = attributeName,
                Label = new Microsoft.Xrm.Sdk.Label(label, 0),
                Value = value,
                StateCode = statecode
            };

            var service = ctx.GetOrganizationService();
            service.Execute(req);

            //Check the status metadata was updated
            var key = $"{Contact.EntityLogicalName}#{attributeName}";
            Assert.True(ctx.StatusAttributeMetadata.ContainsKey(key));

            var option = ctx.StatusAttributeMetadata[key].OptionSet.Options.FirstOrDefault();            
            Assert.Equal(label, option.Label.LocalizedLabels[0].Label);

            // Get a list of Status values for the Status Reason fields from its metadata
            RetrieveAttributeRequest attReq = new RetrieveAttributeRequest
            {
                EntityLogicalName = "contact",
                LogicalName = "statuscode",
                RetrieveAsIfPublished = true
            };

            RetrieveAttributeResponse attResponse = (RetrieveAttributeResponse) service.Execute(attReq);

            StatusAttributeMetadata statusAttributeMetadata = (StatusAttributeMetadata) attResponse.AttributeMetadata;                   

            Assert.NotNull(statusAttributeMetadata.OptionSet);
            Assert.NotNull(statusAttributeMetadata.OptionSet.Options);
            Assert.Equal(1, statusAttributeMetadata.OptionSet.Options.Count(o => o.Label.LocalizedLabels[0].Label == label));            
            Assert.Equal(statecode, ((StatusOptionMetadata)statusAttributeMetadata.OptionSet.Options[0]).State);

            Assert.Equal(statecode,((StatusOptionMetadata)statusAttributeMetadata.OptionSet.Options.FirstOrDefault(o => o.Label.LocalizedLabels[0].Label == label)).State);
        }
    }
}