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
    }
}