using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeXrmEasy.Metadata
{
    internal class MetadataGenerator
    {
        public static IEnumerable<EntityMetadata> FromEarlyBoundEntities(Assembly earlyBoundEntitiesAssembly)
        {
            List<EntityMetadata> entityMetadatas = new List<EntityMetadata>();
            foreach (var earlyBoundEntity in earlyBoundEntitiesAssembly.GetTypes())
            {
                EntityLogicalNameAttribute entityLogicalNameAttribute = GetCustomAttribute<EntityLogicalNameAttribute>(earlyBoundEntity);
                if (entityLogicalNameAttribute == null) continue;
                EntityMetadata metadata = new EntityMetadata();
                metadata.LogicalName = entityLogicalNameAttribute.LogicalName;

                FieldInfo entityTypeCode = earlyBoundEntity.GetField("EntityTypeCode", BindingFlags.Static | BindingFlags.Public);
                if (entityTypeCode != null)
                {
                    metadata.SetFieldValue("_objectTypeCode", entityTypeCode.GetValue(null));
                }

                List<AttributeMetadata> attributeMetadatas = new List<AttributeMetadata>();
                List<ManyToManyRelationshipMetadata> manyToManyRelationshipMetadatas = new List<ManyToManyRelationshipMetadata>();
                List<OneToManyRelationshipMetadata> oneToManyRelationshipMetadatas = new List<OneToManyRelationshipMetadata>();
                List<OneToManyRelationshipMetadata> manyToOneRelationshipMetadatas = new List<OneToManyRelationshipMetadata>();

                var properties = earlyBoundEntity.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    .Where(x => Attribute.IsDefined(x, typeof(AttributeLogicalNameAttribute))
                                             || Attribute.IsDefined(x, typeof(RelationshipSchemaNameAttribute)));

                foreach (var property in properties)
                {
                    RelationshipSchemaNameAttribute relationshipSchemaNameAttribute = (RelationshipSchemaNameAttribute)Attribute.GetCustomAttribute(property, typeof(RelationshipSchemaNameAttribute));
                    AttributeLogicalNameAttribute attributeLogicalNameAttribute = (AttributeLogicalNameAttribute)Attribute.GetCustomAttribute(property, typeof(AttributeLogicalNameAttribute));

                    if (relationshipSchemaNameAttribute == null)
                    {
                        if (property.Name == "Id")
                        {
                            metadata.SetFieldValue("_primaryIdAttribute", attributeLogicalNameAttribute.LogicalName);
                        }
                        else
                        {
#if !FAKE_XRM_EASY
                            if (property.PropertyType.Name == "Byte[]")
                            {
                                metadata.SetFieldValue("_primaryImageAttribute", attributeLogicalNameAttribute.LogicalName);
                            }
#endif

                            AttributeMetadata attributeMetadata = CreateAttributeMetadata(property.PropertyType);
                            if (attributeMetadata == null) continue;
                            attributeMetadata.SetFieldValue("_entityLogicalName", entityLogicalNameAttribute.LogicalName);
                            attributeMetadata.SetFieldValue("_logicalName", attributeLogicalNameAttribute.LogicalName);

                            attributeMetadatas.Add(attributeMetadata);
                        }
                    }
                    else
                    {
                        if (property.PropertyType.Name == "IEnumerable`1")
                        {
                            PropertyInfo peerProperty = property.PropertyType.GetGenericArguments()[0].GetProperties().SingleOrDefault(x => x.PropertyType == earlyBoundEntity && GetCustomAttribute<RelationshipSchemaNameAttribute>(x)?.SchemaName == relationshipSchemaNameAttribute.SchemaName);
                            if (peerProperty == null || peerProperty.PropertyType.Name == "IEnumerable`1") // N:N relationship
                            {
                                ManyToManyRelationshipMetadata relationshipMetadata = new ManyToManyRelationshipMetadata();
                                relationshipMetadata.SchemaName = relationshipSchemaNameAttribute.SchemaName;

                                manyToManyRelationshipMetadatas.Add(relationshipMetadata);
                            }
                            else // 1:N relationship
                            {
                                AddOneToManyRelationshipMetadata(earlyBoundEntity, property, property.PropertyType.GetGenericArguments()[0], peerProperty, oneToManyRelationshipMetadatas);
                            }
                        }
                        else //N:1 Property
                        {
                            AddOneToManyRelationshipMetadata(property.PropertyType, property.PropertyType.GetProperties().SingleOrDefault(x => x.PropertyType.GetGenericArguments().SingleOrDefault() == earlyBoundEntity && GetCustomAttribute<RelationshipSchemaNameAttribute>(x)?.SchemaName == relationshipSchemaNameAttribute.SchemaName), earlyBoundEntity, property, manyToOneRelationshipMetadatas);
                        }
                    }
                }
                if (attributeMetadatas.Any())
                {
                    metadata.SetSealedPropertyValue("Attributes", attributeMetadatas.ToArray());
                }
                if (manyToManyRelationshipMetadatas.Any())
                {
                    metadata.SetSealedPropertyValue("ManyToManyRelationships", manyToManyRelationshipMetadatas.ToArray());
                }
                if (manyToOneRelationshipMetadatas.Any())
                {
                    metadata.SetSealedPropertyValue("ManyToOneRelationships", manyToOneRelationshipMetadatas.ToArray());
                }
                if (oneToManyRelationshipMetadatas.Any())
                {
                    metadata.SetSealedPropertyValue("OneToManyRelationships", oneToManyRelationshipMetadatas.ToArray());
                }
                entityMetadatas.Add(metadata);
            }
            return entityMetadatas;
        }

        private static T GetCustomAttribute<T>(MemberInfo member) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(member, typeof(T));
        }

        private static AttributeMetadata CreateAttributeMetadata(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case "String":
                    return new StringAttributeMetadata();
                case "EntityReference":
                    return new LookupAttributeMetadata();
                case "CrmEntityReference":
                    return new LookupAttributeMetadata();
                case "OptionSetValue":
                    return new PicklistAttributeMetadata();
                case "Money":
                    return new MoneyAttributeMetadata();
                case "Nullable`1":
                    switch (propertyType.GetGenericArguments()[0].Name)
                    {
                        case "Int32":
                            return new IntegerAttributeMetadata();
                        case "Double":
                            return new DoubleAttributeMetadata();
                        case "Boolean":
                            return new BooleanAttributeMetadata();
                        case "Decimal":
                            return new DecimalAttributeMetadata();
                        case "DateTime":
                            return new DateTimeAttributeMetadata();
                        case "Guid":
                            return new LookupAttributeMetadata();
                        case "Int64":
                            return new BigIntAttributeMetadata();
                        case "statecode":
                            return new StateAttributeMetadata();
                        default:
                            if (propertyType.GetGenericArguments()[0].BaseType == typeof(Enum))
                            {
                                return new StateAttributeMetadata();
                            }
                            else
                            {
                                throw new Exception($"Type {propertyType.Name}{propertyType.GetGenericArguments()[0].Name} has not been mapped to an AttributeMetadata.");
                            }

                    }
                case "IEnumerable`1":
                    var partyList = new LookupAttributeMetadata();
                    partyList.SetSealedPropertyValue("AttributeType", AttributeTypeCode.PartyList);
                    return partyList;
                case "BooleanManagedProperty":
                    var booleanManaged = new BooleanAttributeMetadata();
                    booleanManaged.SetSealedPropertyValue("AttributeType", AttributeTypeCode.ManagedProperty);
                    return booleanManaged;
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013
                case "Guid":
                    return new UniqueIdentifierAttributeMetadata();
#endif
#if !FAKE_XRM_EASY
                case "Byte[]":
                    return new ImageAttributeMetadata();
#endif
#if FAKE_XRM_EASY_9
                case "OptionSetValueCollection":
                    return new MultiSelectPicklistAttributeMetadata();
#endif
                default:
                    throw new Exception($"Type {propertyType.Name} has not been mapped to an AttributeMetadata.");
            }
        }

        private static void AddOneToManyRelationshipMetadata(Type referencingEntity, PropertyInfo referencingAttribute, Type referencedEntity, PropertyInfo referencedAttribute, List<OneToManyRelationshipMetadata> relationshipMetadatas)
        {
            if (referencingEntity == null || referencingAttribute == null || referencedEntity == null || referencedAttribute == null) return;
            OneToManyRelationshipMetadata relationshipMetadata = new OneToManyRelationshipMetadata();
            relationshipMetadata.SchemaName = GetCustomAttribute<RelationshipSchemaNameAttribute>(referencingAttribute).SchemaName;
            relationshipMetadata.ReferencingEntity = GetCustomAttribute<EntityLogicalNameAttribute>(referencingEntity).LogicalName;
            relationshipMetadata.ReferencingAttribute = GetCustomAttribute<AttributeLogicalNameAttribute>(referencingAttribute)?.LogicalName;
            relationshipMetadata.ReferencedEntity = GetCustomAttribute<EntityLogicalNameAttribute>(referencedEntity).LogicalName;
            relationshipMetadata.ReferencedAttribute = GetCustomAttribute<AttributeLogicalNameAttribute>(referencedAttribute).LogicalName;

            relationshipMetadatas.Add(relationshipMetadata);
        }
    }
}
