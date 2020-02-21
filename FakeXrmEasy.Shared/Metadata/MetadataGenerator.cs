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

                int? entityTypeCode;
                if (earlyBoundEntity.TryGetEntityTypeCode(out entityTypeCode))
                {
                    metadata.SetFieldValue("_objectTypeCode", entityTypeCode);
                }

                List<AttributeMetadata> attributeMetadatas = new List<AttributeMetadata>();
                List<ManyToManyRelationshipMetadata> manyToManyRelationshipMetadatas = new List<ManyToManyRelationshipMetadata>();
                List<OneToManyRelationshipMetadata> oneToManyRelationshipMetadatas = new List<OneToManyRelationshipMetadata>();
                List<OneToManyRelationshipMetadata> manyToOneRelationshipMetadatas = new List<OneToManyRelationshipMetadata>();

                var idProperty = earlyBoundEntity.GetProperty("Id");
                AttributeLogicalNameAttribute attributeLogicalNameAttribute;
                if (idProperty != null && (attributeLogicalNameAttribute = GetCustomAttribute<AttributeLogicalNameAttribute>(idProperty)) != null)
                {
                    metadata.SetFieldValue("_primaryIdAttribute", attributeLogicalNameAttribute.LogicalName);
                }

                var properties = earlyBoundEntity.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    .Where(x => x.Name != "Id" && Attribute.IsDefined(x, typeof(AttributeLogicalNameAttribute))
                                             || Attribute.IsDefined(x, typeof(RelationshipSchemaNameAttribute)));

                foreach (var property in properties)
                {
                    RelationshipSchemaNameAttribute relationshipSchemaNameAttribute = GetCustomAttribute<RelationshipSchemaNameAttribute>(property);
                    attributeLogicalNameAttribute = GetCustomAttribute<AttributeLogicalNameAttribute>(property);

                    if (relationshipSchemaNameAttribute == null)
                    {
#if !FAKE_XRM_EASY
                        if (property.PropertyType == typeof(byte[]))
                        {
                            metadata.SetFieldValue("_primaryImageAttribute", attributeLogicalNameAttribute.LogicalName);
                        }
#endif
                        AttributeMetadata attributeMetadata;
                        if (attributeLogicalNameAttribute.LogicalName == "statecode")
                        {
                            attributeMetadata = new StateAttributeMetadata();
                        }
                        else if (attributeLogicalNameAttribute.LogicalName == "statuscode")
                        {
                            attributeMetadata = new StatusAttributeMetadata();
                        }
                        else if (attributeLogicalNameAttribute.LogicalName == metadata.PrimaryIdAttribute)
                        {
                            attributeMetadata = new AttributeMetadata();
                            attributeMetadata.SetSealedPropertyValue("AttributeType", AttributeTypeCode.Uniqueidentifier);
                        }
                        else
                        {
                            attributeMetadata = CreateAttributeMetadata(property.PropertyType);
                        }

                        attributeMetadata.SetFieldValue("_entityLogicalName", entityLogicalNameAttribute.LogicalName);
                        attributeMetadata.SetFieldValue("_logicalName", attributeLogicalNameAttribute.LogicalName);

                        attributeMetadatas.Add(attributeMetadata);
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
            if (typeof(string) == propertyType)
            {
                return new StringAttributeMetadata();
            }
            else if (typeof(EntityReference).IsAssignableFrom(propertyType))
            {
                return new LookupAttributeMetadata();
            }
#if FAKE_XRM_EASY || FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365
            else if (typeof(Microsoft.Xrm.Client.CrmEntityReference).IsAssignableFrom(propertyType))
            {
                return new LookupAttributeMetadata();
            }
#endif
            else if (typeof(OptionSetValue).IsAssignableFrom(propertyType))
            {
                return new PicklistAttributeMetadata();
            }
            else if (typeof(Money).IsAssignableFrom(propertyType))
            {
                return new MoneyAttributeMetadata();
            }
            else if (propertyType.IsGenericType)
            {
                Type genericType = propertyType.GetGenericArguments().FirstOrDefault();
                if (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (typeof(int) == genericType)
                    {
                        return new IntegerAttributeMetadata();
                    }
                    else if (typeof(double) == genericType)
                    {
                        return new DoubleAttributeMetadata();
                    }
                    else if (typeof(bool) == genericType)
                    {
                        return new BooleanAttributeMetadata();
                    }
                    else if (typeof(decimal) == genericType)
                    {
                        return new DecimalAttributeMetadata();
                    }
                    else if (typeof(DateTime) == genericType)
                    {
                        return new DateTimeAttributeMetadata();
                    }
                    else if (typeof(Guid) == genericType)
                    {
                        return new LookupAttributeMetadata();
                    }
                    else if (typeof(long) == genericType)
                    {
                        return new BigIntAttributeMetadata();
                    }
                    else if (typeof(Enum).IsAssignableFrom(genericType))
                    {
                        return new StateAttributeMetadata();
                    }
                    else
                    {
                        throw new Exception($"Type {propertyType.Name}{genericType.Name} has not been mapped to an AttributeMetadata.");
                    }
                }
                else if (propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var partyList = new LookupAttributeMetadata();
                    partyList.SetSealedPropertyValue("AttributeType", AttributeTypeCode.PartyList);
                    return partyList;
                }
                else
                {
                    throw new Exception($"Type {propertyType.Name}{genericType.Name} has not been mapped to an AttributeMetadata.");
                }
            }
            else if (typeof(BooleanManagedProperty) == propertyType)
            {
                var booleanManaged = new BooleanAttributeMetadata();
                booleanManaged.SetSealedPropertyValue("AttributeType", AttributeTypeCode.ManagedProperty);
                return booleanManaged;
            }
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013
            else if (typeof(Guid) == propertyType)
            {
                return new UniqueIdentifierAttributeMetadata();
            }
#endif
#if !FAKE_XRM_EASY
            else if (typeof(byte[]) == propertyType)
            {

                return new ImageAttributeMetadata();
            }
#endif
#if FAKE_XRM_EASY_9
            else if (typeof(OptionSetValueCollection).IsAssignableFrom(propertyType))
            {
                return new MultiSelectPicklistAttributeMetadata();
            }
#endif
            else
            {
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
