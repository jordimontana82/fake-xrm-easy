using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FakeXrmEasy.Extensions
{
    public static class EntityMetadataExtensions
    {
        public static void SetAttributeCollection(this EntityMetadata entityMetadata, AttributeMetadata[] attributes)
        {
            //AttributeMetadata is internal set in a sealed class so... just doing this

            entityMetadata.GetType().GetProperty("Attributes").SetValue(entityMetadata, attributes, null);
        }

        public static void SetAttributeCollection(this EntityMetadata entityMetadata, IEnumerable<AttributeMetadata> attributes)
        {
            entityMetadata.GetType().GetProperty("Attributes").SetValue(entityMetadata, attributes.ToList().ToArray(), null);
        }

        public static void SetSealedPropertyValue(this EntityMetadata entityMetadata, string sPropertyName, object value)
        {
            entityMetadata.GetType().GetProperty(sPropertyName).SetValue(entityMetadata, value, null);
        }

        public static void SetSealedPropertyValue(this AttributeMetadata attributeMetadata, string sPropertyName, object value)
        {
            attributeMetadata.GetType().GetProperty(sPropertyName).SetValue(attributeMetadata, value, null);
        }
    }
}
