using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class XrmFakedRelationshipTests
    {
        [Fact]
        public void When_creating_relationship_with_first_constructor_properties_are_set_correctly()
        {
            var rel = new XrmFakedRelationship("entity1Attribute", "entity2Attribute", "entity1LogicalName", "entity2LogicalName");

            Assert.Equal(rel.Entity1LogicalName, "entity1LogicalName");
            Assert.Equal(rel.Entity2LogicalName, "entity2LogicalName");
            Assert.Equal(rel.Entity1Attribute, "entity1Attribute");
            Assert.Equal(rel.Entity2Attribute, "entity2Attribute");
            Assert.Equal(rel.RelationshipType, XrmFakedRelationship.enmFakeRelationshipType.OneToMany);
        }

        [Fact]
        public void When_creating_relationship_with_second_constructor_properties_are_set_correctly()
        {
            var rel = new XrmFakedRelationship("intersectName", "entity1Attribute", "entity2Attribute", "entity1LogicalName", "entity2LogicalName");

            Assert.Equal(rel.Entity1LogicalName, "entity1LogicalName");
            Assert.Equal(rel.Entity2LogicalName, "entity2LogicalName");
            Assert.Equal(rel.Entity1Attribute, "entity1Attribute");
            Assert.Equal(rel.Entity2Attribute, "entity2Attribute");
            Assert.Equal(rel.IntersectEntity, "intersectName");
            Assert.Equal(rel.RelationshipType, XrmFakedRelationship.enmFakeRelationshipType.ManyToMany);
        }

        [Fact]
        public void Self_referential_relationships_can_be_created()
        {
            var context = new XrmFakedContext();


            var exampleMetadata = new EntityMetadata()
            {
                LogicalName = "test_entity"
            };

            var nameAttribute = new StringAttributeMetadata()
            {
                LogicalName = "name",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired)
            };

            var idAttribute = new AttributeMetadata()
            {
                LogicalName = "test_entityid",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired),
            };
            exampleMetadata.SetAttributeCollection(new AttributeMetadata[] { idAttribute, nameAttribute });

            context.InitializeMetadata(new[] { exampleMetadata });

            context.AddRelationship("test_entity_entity", new XrmFakedRelationship
            {
                IntersectEntity = "test_entity_entity",
                Entity1LogicalName = exampleMetadata.LogicalName,
                Entity1Attribute = idAttribute.LogicalName,
                Entity2LogicalName = exampleMetadata.LogicalName,
                Entity2Attribute = idAttribute.LogicalName,
                RelationshipType = XrmFakedRelationship.enmFakeRelationshipType.ManyToMany
            });

            var record1 = new Entity(exampleMetadata.LogicalName)
            {
                Id = Guid.NewGuid(),
                [nameAttribute.LogicalName] = "First Record"
            };

            var record2 = new Entity(exampleMetadata.LogicalName)
            {
                Id = Guid.NewGuid(),
                [nameAttribute.LogicalName] = "Second Record"
            };

            context.Initialize(new[] { record1, record2 });


            var service = context.GetOrganizationService();

            var relationship = new Relationship("test_entity_entity");

            var ex = Record.Exception(() => service.Associate(
                exampleMetadata.LogicalName,
                record1.Id,
                relationship,
                new EntityReferenceCollection(
                    new[] { record2.ToEntityReference() }
                    )
                ));

            Assert.Null(ex);
        }

        [Fact]
        public void Relationships_between_two_different_entities_can_be_created()
        {
            var context = new XrmFakedContext();


            var exampleMetadata = new EntityMetadata()
            {
                LogicalName = "test_entity",
            };

            var nameAttribute = new StringAttributeMetadata()
            {
                LogicalName = "name",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired)
            };

            var idAttribute = new AttributeMetadata()
            {
                LogicalName = "test_entityid",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired),
            };
            exampleMetadata.SetAttributeCollection(new AttributeMetadata[] { idAttribute, nameAttribute });


            var otherMetadata = new EntityMetadata()
            {
                LogicalName = "test_other",
            };

            var otherNameAttribute = new StringAttributeMetadata()
            {
                LogicalName = "name",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired)
            };

            var otherIdAttribute = new AttributeMetadata()
            {
                LogicalName = "test_otherid",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired),
            };
            otherMetadata.SetAttributeCollection(new AttributeMetadata[] { otherIdAttribute, otherNameAttribute });

            context.InitializeMetadata(new[] { exampleMetadata });

            context.AddRelationship("test_entity_other", new XrmFakedRelationship
            {
                IntersectEntity = "test_entity_other",
                Entity1LogicalName = exampleMetadata.LogicalName,
                Entity1Attribute = idAttribute.LogicalName,
                Entity2LogicalName = otherMetadata.LogicalName,
                Entity2Attribute = otherIdAttribute.LogicalName,
                RelationshipType = XrmFakedRelationship.enmFakeRelationshipType.ManyToMany
            });

            var record1 = new Entity(exampleMetadata.LogicalName)
            {
                Id = Guid.NewGuid(),
                [nameAttribute.LogicalName] = "First Record"
            };

            var record2 = new Entity(otherMetadata.LogicalName)
            {
                Id = Guid.NewGuid(),
                [nameAttribute.LogicalName] = "Second Record"
            };

            context.Initialize(new[] { record1, record2 });

            var service = context.GetOrganizationService();

            var relationship = new Relationship("test_entity_other");

            var ex = Record.Exception(() => service.Associate(
                   exampleMetadata.LogicalName,
                   record1.Id,
                   relationship,
                   new EntityReferenceCollection(
                       new[] { record2.ToEntityReference() }
                       )
                   ));

            Assert.Null(ex);
        }
    }
}