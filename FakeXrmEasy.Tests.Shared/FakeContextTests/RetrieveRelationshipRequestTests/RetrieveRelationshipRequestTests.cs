using Crm;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveRelationshipRequestTests
{
    public class RetrieveRelationshipRequestTests
    {
        [Fact]
        public void When_Retrieving_ManyToMany_then_Attributes_are_present()
        {
            var fakedContext = new XrmFakedContext();
            var orgService = fakedContext.GetOrganizationService();

            // account N:N leads
            var schemaName = "accountleads_association";
            var fakedRelationship = new XrmFakedRelationship
            {
                IntersectEntity = "accountleads",
                Entity1LogicalName = Account.EntityLogicalName,
                Entity1Attribute = "accountid",
                Entity2LogicalName = Lead.EntityLogicalName,
                Entity2Attribute = "leadid",
                RelationshipType = XrmFakedRelationship.enmFakeRelationshipType.ManyToMany
            };
            fakedContext.AddRelationship(schemaName, fakedRelationship);

            var request = new RetrieveRelationshipRequest {Name = schemaName};
            var response = (RetrieveRelationshipResponse) orgService.Execute(request);

            Assert.IsType<ManyToManyRelationshipMetadata>(response.RelationshipMetadata);
            var relationship = (ManyToManyRelationshipMetadata) response.RelationshipMetadata;
            Assert.Equal(schemaName, relationship.SchemaName);
            Assert.Equal(fakedRelationship.Entity1LogicalName, relationship.Entity1LogicalName);
            Assert.Equal(fakedRelationship.Entity1Attribute, relationship.Entity1IntersectAttribute);
            Assert.Equal(fakedRelationship.Entity2LogicalName, relationship.Entity2LogicalName);
            Assert.Equal(fakedRelationship.Entity2Attribute, relationship.Entity2IntersectAttribute);
            Assert.Equal(fakedRelationship.IntersectEntity, relationship.IntersectEntityName);
        }

        [Fact]
        public void When_Retrieving_OneToMany_then_Attributes_are_present()
        {
            var fakedContext = new XrmFakedContext();
            var orgService = fakedContext.GetOrganizationService();

            // account 1:N contacts
            var schemaName = "contact_customer_accounts";
            var fakedRelationship = new XrmFakedRelationship
            {
                Entity1LogicalName = Contact.EntityLogicalName,
                Entity1Attribute = "parentcustomerid",
                Entity2LogicalName = Account.EntityLogicalName,
                Entity2Attribute = "accountid",
                RelationshipType = XrmFakedRelationship.enmFakeRelationshipType.OneToMany
            };
            fakedContext.AddRelationship(schemaName, fakedRelationship);

            var request = new RetrieveRelationshipRequest {Name = schemaName};
            var response = (RetrieveRelationshipResponse) orgService.Execute(request);

            Assert.IsType<OneToManyRelationshipMetadata>(response.RelationshipMetadata);
            var relationship = (OneToManyRelationshipMetadata) response.RelationshipMetadata;
            Assert.Equal(schemaName, relationship.SchemaName);
            Assert.Equal(fakedRelationship.Entity1LogicalName, relationship.ReferencingEntity);
            Assert.Equal(fakedRelationship.Entity1Attribute, relationship.ReferencingAttribute);
            Assert.Equal(fakedRelationship.Entity2LogicalName, relationship.ReferencedEntity);
            Assert.Equal(fakedRelationship.Entity2Attribute, relationship.ReferencedAttribute);
#if FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
            Assert.Equal(fakedRelationship.IntersectEntity, relationship.ReferencedEntityNavigationPropertyName);
#endif
        }
    }
}