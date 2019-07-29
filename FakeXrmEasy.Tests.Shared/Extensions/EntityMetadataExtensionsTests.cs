using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Xunit;
using System.Collections.Generic;

namespace FakeXrmEasy.Tests.Extensions
{
    public class EntityMetadataExtensionsTests
    {
        [Fact]
        public void SetSealedPropertyValue_should_update_entity_metadata()
        {
            var entityMetadata = new EntityMetadata();
            entityMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "Account");
            Assert.Equal("Account", entityMetadata.PrimaryNameAttribute);
        }

        [Fact]
        public void SetSealedPropertyValue_should_update_attribute_metadata()
        {
            var fakeAttribute = new StringAttributeMetadata();

            fakeAttribute.SetSealedPropertyValue("IsManaged", false);
            Assert.Equal(false, fakeAttribute.IsManaged);
        }

        [Fact]
        public void SetSealedPropertyValue_should_update_manytomanyrelationship_metadata()
        {
            var fakeRelationship = new ManyToManyRelationshipMetadata();

            fakeRelationship.SetSealedPropertyValue("Entity2LogicalName", "role");
            Assert.Equal("role", fakeRelationship.Entity2LogicalName);
        }

        [Fact]
        public void SetSealedPropertyValue_should_update_onetomanyrelationship_metadata()
        {
            var fakeRelationship = new OneToManyRelationshipMetadata();

            fakeRelationship.SetSealedPropertyValue("ReferencingEntity", "account");
            Assert.Equal("account", fakeRelationship.ReferencingEntity);
        }

        [Fact]
        public void SetAttributeCollection_should_update_attributes()
        {
            var entityMetadata = new EntityMetadata();
            var fakeAttribute = new StringAttributeMetadata() { LogicalName = "name" };
            var fakeAttribute2 = new StringAttributeMetadata() { LogicalName = "name2" };


            entityMetadata.SetAttributeCollection(new List<AttributeMetadata>() { fakeAttribute, fakeAttribute2 });
            Assert.Equal(2, entityMetadata.Attributes.Length);
            Assert.Equal("name", entityMetadata.Attributes[0].LogicalName);
            Assert.Equal("name2", entityMetadata.Attributes[1].LogicalName);
        }

        [Fact]
        public void SetAttribute_should_not_throw_error()
        {
            var entityMetadata = new EntityMetadata();
            var fakeAttribute = new StringAttributeMetadata() { LogicalName = "name" };


            entityMetadata.SetAttribute(fakeAttribute);
            Assert.Equal(1, entityMetadata.Attributes.Length);
            Assert.Equal("name", entityMetadata.Attributes[0].LogicalName);
        }
    }
}
