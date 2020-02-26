using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace FakeXrmEasy
{
    public class XrmOrderByAttributeComparer : IComparer<object>
    {
        public int Compare(Object objectA, Object objectB)
        {
            if (objectA == null && objectB == null) return 0;  //Equal

            if (objectA == null)
                return -1;
            if (objectB == null)
                return 1;

            Type attributeType = objectA.GetType();

            if (attributeType == typeof(OptionSetValue))
            {
                // we'll want the text value
                OptionSetValue attributeValueA = (OptionSetValue)(objectA);
                OptionSetValue attributeValueB = (OptionSetValue)(objectB);
                return attributeValueA.Value.CompareTo(attributeValueB.Value);
            }
            else if (attributeType == typeof(EntityReference)
#if FAKE_XRM_EASY
                    || attributeType == typeof(Microsoft.Xrm.Client.CrmEntityReference)
#endif
                )
            {
                // Name might well be Null in an entity reference?
                EntityReference entityRefA = (EntityReference)objectA;
                EntityReference entityRefB = (EntityReference)objectB;

                if (entityRefA.Name == null && entityRefB.Name == null) return 0;  //Equal

                if (entityRefA.Name == null)
                    return -1;
                if (entityRefB.Name == null)
                    return 1;

                return entityRefA.Name.CompareTo(entityRefB.Name);
            }
            else if (attributeType == typeof(Money))
            {
                Decimal valueA = ((Money)objectA).Value;
                Decimal valueB = ((Money)objectB).Value;
                var x = valueA.CompareTo(valueB);
                return x;
            }
            else if (attributeType == typeof(string))
            {
                return String.Compare(objectA.ToString(), objectB.ToString());
            }
            else if (attributeType == typeof(int))
            {
                return ((int)objectA).CompareTo(((int)objectB));
            }
            else if (attributeType == typeof(DateTime))
            {
                return ((DateTime)objectA).CompareTo((DateTime)objectB);
            }
            else if (attributeType == typeof(Guid))
            {
                return ((Guid)objectA).CompareTo((Guid)objectB);
            }
            else if (attributeType == typeof(decimal))
            {
                return ((decimal)objectA).CompareTo((decimal)objectB);
            }
            else if (attributeType == typeof(double))
            {
                return ((double)objectA).CompareTo((double)objectB);
            }
            else if (attributeType == typeof(float))
            {
                return ((float)objectA).CompareTo((float)objectB);
            }
            else if (attributeType == typeof(bool))
            {
                return ((bool)objectA).CompareTo((bool)objectB);
            }
            else if (attributeType == typeof(AliasedValue))
            {
                return Compare((objectA as AliasedValue)?.Value, (objectB as AliasedValue)?.Value);
            }
            else
            {
                return 0;
            }
        }
    }
}