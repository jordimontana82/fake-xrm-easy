using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk.Messages;
using Crm;
using Microsoft.Xrm.Sdk;
using System.Reflection;
using System.Linq;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue278
    {
        [Fact]
        public void Reproduce_issue_278()
        {
            string attributeName = "statuscode";
            string label = "A faked label";

            XrmFakedContext fakedContext = new XrmFakedContext
            {
                ProxyTypesAssembly = (Assembly.GetAssembly(typeof(Contact)))
            };

            var entityMetadata = new EntityMetadata()
            {
                LogicalName = "contact"
            };

            StatusAttributeMetadata enumAttribute = new StatusAttributeMetadata() { LogicalName = attributeName };

            entityMetadata.SetAttributeCollection(new List<AttributeMetadata>() { enumAttribute });

            var req = new InsertOptionValueRequest()
            {
                EntityLogicalName = Contact.EntityLogicalName,
                AttributeLogicalName = attributeName,
                Label = new Label(label, 0)
            };

            fakedContext.InitializeMetadata(entityMetadata);

            var fakedService = fakedContext.GetOrganizationService();
            fakedService.Execute(req);


            //Check the optionsetmetadata was updated
            var key = string.Format("{0}#{1}", Contact.EntityLogicalName, attributeName);

            Assert.True(fakedContext.OptionSetValuesMetadata.ContainsKey(key));

            var option = fakedContext.OptionSetValuesMetadata[key].Options.FirstOrDefault();

            Assert.Equal(label, option.Label.LocalizedLabels[0].Label);

            // Get a list of Option Set values for the Status Reason fields from its metadata
            RetrieveAttributeRequest attReq = new RetrieveAttributeRequest();
            attReq.EntityLogicalName = "contact";
            attReq.LogicalName = "statuscode";
            attReq.RetrieveAsIfPublished = true;

            RetrieveAttributeResponse attResponse = (RetrieveAttributeResponse)fakedService.Execute(attReq);

            // Cast as StatusAttributeMetadata
            StatusAttributeMetadata statusAttributeMetadata = (StatusAttributeMetadata)attResponse.AttributeMetadata;

            Assert.Equal(label, statusAttributeMetadata.OptionSet.Options.First().Label.LocalizedLabels[0].Label);
            //Assert.Equal(label, statusAttributeMetadata.OptionSet.Options.First().Label.UserLocalizedLabel.Label); This one is null when using the above Label constructor
        }
    }
}
