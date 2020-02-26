namespace FakeXrmEasy
{
    public class XrmFakedRelationship
    {

        private string entity1Attribute = string.Empty;
        private string entity2Attribute = string.Empty;

        /// <summary>
        /// Schema name of the many to many intersect entity
        /// </summary>
        public string IntersectEntity { get; set; }

        /// <summary>
        /// Entity name and attribute of the first entity participating in the relationship
        /// </summary>
        public string Entity1Attribute
        {
            get
            {
                if (entity1Attribute == entity2Attribute && Entity1LogicalName == Entity2LogicalName)
                {
                    return entity1Attribute + "one";
                }
                else
                {
                    return entity1Attribute;
                }
            }
            set { entity1Attribute = value; }
        }

        public string Entity1LogicalName { get; set; }

        public string Entity2LogicalName { get; set; }

        /// <summary>
        /// Entity name and attribute of the second entity participating in the relationship
        /// </summary>
        public string Entity2Attribute
        {
            get
            {
                if (entity1Attribute == entity2Attribute && Entity1LogicalName == Entity2LogicalName)
                {
                    return entity2Attribute + "two";
                }
                else
                {
                    return entity2Attribute;
                }
            }
            set { entity2Attribute = value; }
        }

        public XrmFakedRelationship()
        {
            RelationshipType = enmFakeRelationshipType.ManyToMany;
        }

        public enum enmFakeRelationshipType
        {
            ManyToMany = 0,
            OneToMany = 1
        }

        public enmFakeRelationshipType RelationshipType { get; set; }

        /// <summary>
        /// Initializes a N:N relationship type
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entity1Attribute"></param>
        /// <param name="entity2Attribute"></param>
        /// <param name="entity1LogicalName"></param>
        /// <param name="entity2LogicalName"></param>
        public XrmFakedRelationship(string entityName, string entity1Attribute, string entity2Attribute, string entity1LogicalName, string entity2LogicalName)
        {
            IntersectEntity = entityName;
            Entity1Attribute = entity1Attribute;
            Entity2Attribute = entity2Attribute;
            Entity1LogicalName = entity1LogicalName;
            Entity2LogicalName = entity2LogicalName;
            RelationshipType = enmFakeRelationshipType.ManyToMany;
        }

        public XrmFakedRelationship(string entity1Attribute, string entity2Attribute, string entity1LogicalName, string entity2LogicalName)
        {
            Entity1Attribute = entity1Attribute;
            Entity2Attribute = entity2Attribute;
            Entity1LogicalName = entity1LogicalName;
            Entity2LogicalName = entity2LogicalName;
            RelationshipType = enmFakeRelationshipType.OneToMany;
        }
    }
}