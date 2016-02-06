using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy
{
    public class XrmFakedRelationship
    {

        /// <summary>
        /// Schema name of the many to many intersect entity
        /// </summary>
        public string IntersectEntity { get; set; }

       /// <summary>
        /// Entity name and attribute of the first entity participating in the relationship
        /// </summary>
        public string Entity1Attribute { get; set; }

        public string Entity1LogicalName { get; set; }

        public string Entity2LogicalName { get; set; }

        /// <summary>
        /// Entity name and attribute of the second entity participating in the relationship
        /// </summary>
        public string Entity2Attribute { get; set; }

        public XrmFakedRelationship() { }

		public XrmFakedRelationship(string entityName, string entity1Attribute, string entity2Attribute, string entity1LogicalName, string entity2LogicalName)
		{
			IntersectEntity = entityName;
            Entity1Attribute = entity1Attribute;
            Entity2Attribute = entity2Attribute;
		    Entity1LogicalName = entity1LogicalName;
		    Entity2LogicalName = entity2LogicalName;
		}
    }
}
