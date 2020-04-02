﻿using FakeXrmEasy.Metadata;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeXrmEasy.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Extension method to add an attribute and return the entity itself
        /// </summary>
        /// <param name="e"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Entity AddAttribute(this Entity e, string key, object value)
        {
            e.Attributes.Add(key, value);
            return e;
        }

        /// <summary>
        /// Projects the attributes of entity e so that only the attributes specified in the columnSet are returned
        /// </summary>
        /// <param name="e"></param>
        /// <param name="columnSet"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static Entity ProjectAttributes(this Entity e, ColumnSet columnSet, XrmFakedContext context)
        {
            return ProjectAttributes(e, new QueryExpression() { ColumnSet = columnSet }, context);
        }

        public static void ApplyDateBehaviour(this Entity e, XrmFakedContext context)
        {
#if FAKE_XRM_EASY || FAKE_XRM_EASY_2013
            return; //Do nothing... DateBehavior wasn't available for versions <= 2013
#else

            if (context.DateBehaviour.Count == 0 || e.LogicalName == null || !context.DateBehaviour.ContainsKey(e.LogicalName))
            {
                return;
            }

            var entityDateBehaviours = context.DateBehaviour[e.LogicalName];
            foreach (var attribute in entityDateBehaviours.Keys)
            {
                if (!e.Attributes.ContainsKey(attribute))
                {
                    continue;
                }

                switch (entityDateBehaviours[attribute])
                {
                    case DateTimeAttributeBehavior.DateOnly:
                        var currentValue = (DateTime)e[attribute];
                        e[attribute] = new DateTime(currentValue.Year, currentValue.Month, currentValue.Day, 0, 0, 0, DateTimeKind.Utc);
                        break;

                    default:
                        break;
                }
            }
#endif
        }

        public static void ProjectAttributes(Entity e, Entity projected, LinkEntity le, XrmFakedContext context)
        {
            var sAlias = string.IsNullOrWhiteSpace(le.EntityAlias) ? le.LinkToEntityName : le.EntityAlias;

            if (le.Columns.AllColumns)
            {
                foreach (var attKey in e.Attributes.Keys)
                {
                    if (attKey.StartsWith(sAlias + "."))
                    {
                        projected[attKey] = e[attKey];
                    }
                }

                foreach (var attKey in e.FormattedValues.Keys)
                {
                    if (attKey.StartsWith(sAlias + "."))
                    {
                        projected.FormattedValues[attKey] = e.FormattedValues[attKey];
                    }
                }
            }
            else
            {
                foreach (var attKey in le.Columns.Columns)
                {
                    var linkedAttKey = sAlias + "." + attKey;
                    if (e.Attributes.ContainsKey(linkedAttKey))
                        projected[linkedAttKey] = e[linkedAttKey];

                    if (e.FormattedValues.ContainsKey(linkedAttKey))
                        projected.FormattedValues[linkedAttKey] = e.FormattedValues[linkedAttKey];
                }

            }

            foreach (var nestedLinkedEntity in le.LinkEntities)
            {
                ProjectAttributes(e, projected, nestedLinkedEntity, context);
            }
        }

        public static Entity ProjectAttributes(this Entity e, QueryExpression qe, XrmFakedContext context)
        {
            if (qe.ColumnSet == null || qe.ColumnSet.AllColumns)
            {
                return RemoveNullAttributes(e); //return all the original attributes
            }
            else
            {
                //Return selected list of attributes in a projected entity
                Entity projected = null;

                //However, if we are using proxy types, we must create a instance of the appropiate class
                if (context.ProxyTypesAssembly != null)
                {
                    var subClassType = context.FindReflectedType(e.LogicalName);
                    if (subClassType != null)
                    {
                        var instance = Activator.CreateInstance(subClassType);
                        projected = (Entity)instance;
                        projected.Id = e.Id;
                    }
                    else
                        projected = new Entity(e.LogicalName) { Id = e.Id }; //fallback to generic type if type not found
                }
                else
                    projected = new Entity(e.LogicalName) { Id = e.Id };


                foreach (var attKey in qe.ColumnSet.Columns)
                {
                    //Check if attribute really exists in metadata
                    if (!context.AttributeExistsInMetadata(e.LogicalName, attKey))
                    {
                        FakeOrganizationServiceFault.Throw(ErrorCodes.QueryBuilderNoAttribute, string.Format("The attribute {0} does not exist on this entity.", attKey));
                    }

                    if (e.Attributes.ContainsKey(attKey) && e.Attributes[attKey] != null)
                    {
                        projected[attKey] = CloneAttribute(e[attKey], context);

                        string formattedValue = "";

                        if (e.FormattedValues.TryGetValue(attKey, out formattedValue))
                        {
                            projected.FormattedValues[attKey] = formattedValue;
                        }
                    }
                }


                //Plus attributes from joins
                foreach (var le in qe.LinkEntities)
                {
                    ProjectAttributes(RemoveNullAttributes(e), projected, le, context);
                }
                return RemoveNullAttributes(projected);
            }
        }

        public static Entity RemoveNullAttributes(Entity entity)
        {
            IList<string> nullAttributes = entity.Attributes
                .Where(attribute => attribute.Value == null ||
                                  (attribute.Value is AliasedValue && (attribute.Value as AliasedValue).Value == null))
                .Select(attribute => attribute.Key).ToList();
            foreach (var nullAttribute in nullAttributes)
            {
                entity.Attributes.Remove(nullAttribute);
            }
            return entity;
        }

        public static object CloneAttribute(object attributeValue, XrmFakedContext context = null)
        {
            if (attributeValue == null)
                return null;

            var type = attributeValue.GetType();
            if (type == typeof(string))
                return new string((attributeValue as string).ToCharArray());
            else if (type == typeof(EntityReference)
#if FAKE_XRM_EASY
                            || type == typeof(Microsoft.Xrm.Client.CrmEntityReference)
#endif
                    )
            {
                var original = (attributeValue as EntityReference);
                var clone = new EntityReference(original.LogicalName, original.Id);

                if (context != null && !string.IsNullOrEmpty(original.LogicalName) && context.EntityMetadata.ContainsKey(original.LogicalName) && !string.IsNullOrEmpty(context.EntityMetadata[original.LogicalName].PrimaryNameAttribute) &&
                    context.Data.ContainsKey(original.LogicalName) && context.Data[original.LogicalName].ContainsKey(original.Id))
                {
                    clone.Name = context.Data[original.LogicalName][original.Id].GetAttributeValue<string>(context.EntityMetadata[original.LogicalName].PrimaryNameAttribute);
                }
                else
                {
                    clone.Name = CloneAttribute(original.Name) as string;
                }

#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
                if (original.KeyAttributes != null)
                {
                    clone.KeyAttributes = new KeyAttributeCollection();
                    clone.KeyAttributes.AddRange(original.KeyAttributes.Select(kvp => new KeyValuePair<string, object>(CloneAttribute(kvp.Key) as string, kvp.Value)).ToArray());
                }
#endif
                return clone;
            }
            else if (type == typeof(BooleanManagedProperty))
            {
                var original = (attributeValue as BooleanManagedProperty);
                return new BooleanManagedProperty(original.Value);
            }
            else if (type == typeof(OptionSetValue))
            {
                var original = (attributeValue as OptionSetValue);
                return new OptionSetValue(original.Value);
            }
            else if (type == typeof(AliasedValue))
            {
                var original = (attributeValue as AliasedValue);
                return new AliasedValue(original.EntityLogicalName, original.AttributeLogicalName, CloneAttribute(original.Value));
            }
            else if (type == typeof(Money))
            {
                var original = (attributeValue as Money);
                return new Money(original.Value);
            }
            else if (attributeValue.GetType() == typeof(EntityCollection))
            {
                var collection = attributeValue as EntityCollection;
                return new EntityCollection(collection.Entities.Select(e => e.Clone(e.GetType())).ToList());
            }
            else if (attributeValue is IEnumerable<Entity>)
            {
                var enumerable = attributeValue as IEnumerable<Entity>;
                return enumerable.Select(e => e.Clone(e.GetType())).ToArray();
            }
#if !FAKE_XRM_EASY
            else if (type == typeof(byte[]))
            {
                var original = (attributeValue as byte[]);
                var copy = new byte[original.Length];
                original.CopyTo(copy, 0);
                return copy;
            }
#endif
#if FAKE_XRM_EASY_9
            else if (attributeValue is OptionSetValueCollection)
            {
                var original = (attributeValue as OptionSetValueCollection);
                var copy = new OptionSetValueCollection(original.ToArray());
                return copy;
            }
#endif
            else if (type == typeof(int) || type == typeof(Int64))
                return attributeValue; //Not a reference type
            else if (type == typeof(decimal))
                return attributeValue; //Not a reference type
            else if (type == typeof(double))
                return attributeValue; //Not a reference type
            else if (type == typeof(float))
                return attributeValue; //Not a reference type
            else if (type == typeof(byte))
                return attributeValue; //Not a reference type
            else if (type == typeof(float))
                return attributeValue; //Not a reference type
            else if (type == typeof(bool))
                return attributeValue; //Not a reference type
            else if (type == typeof(Guid))
                return attributeValue; //Not a reference type
            else if (type == typeof(DateTime))
                return attributeValue; //Not a reference type
            else if (attributeValue is Enum)
                return attributeValue; //Not a reference type

            throw new Exception(string.Format("Attribute type not supported when trying to clone attribute '{0}'", type.ToString()));
        }

        public static Entity Clone(this Entity e, XrmFakedContext context = null)
        {
            var cloned = new Entity(e.LogicalName);
            cloned.Id = e.Id;
            cloned.LogicalName = e.LogicalName;

            if (e.FormattedValues != null)
            {
                var formattedValues = new FormattedValueCollection();
                foreach (var key in e.FormattedValues.Keys)
                    formattedValues.Add(key, e.FormattedValues[key]);

                cloned.Inject("FormattedValues", formattedValues);
            }

            foreach (var attKey in e.Attributes.Keys)
            {
                cloned[attKey] = e[attKey] != null ? CloneAttribute(e[attKey], context) : null;
            }
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
            foreach (var attKey in e.KeyAttributes.Keys)
            {
                cloned.KeyAttributes[attKey] = e.KeyAttributes[attKey] != null ? CloneAttribute(e.KeyAttributes[attKey]) : null;
            }
#endif
            return cloned;
        }

        public static T Clone<T>(this Entity e) where T : Entity
        {
            return (T)e.Clone(typeof(T));
        }

        public static Entity Clone(this Entity e, Type t, XrmFakedContext context = null)
        {
            if (t == null)
                return e.Clone(context);

            var cloned = Activator.CreateInstance(t) as Entity;
            cloned.Id = e.Id;
            cloned.LogicalName = e.LogicalName;

            if (e.FormattedValues != null)
            {
                var formattedValues = new FormattedValueCollection();
                foreach (var key in e.FormattedValues.Keys)
                    formattedValues.Add(key, e.FormattedValues[key]);

                cloned.Inject("FormattedValues", formattedValues);
            }

            foreach (var attKey in e.Attributes.Keys)
            {
                cloned[attKey] = e[attKey] != null ? CloneAttribute(e[attKey], context) : null;
            }

#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
            foreach (var attKey in e.KeyAttributes.Keys)
            {
                cloned.KeyAttributes[attKey] = e.KeyAttributes[attKey] != null ? CloneAttribute(e.KeyAttributes[attKey]) : null;
            }
#endif
            return cloned;
        }

        /// <summary>
        /// Extension method to join the attributes of entity e and otherEntity
        /// </summary>
        /// <param name="e"></param>
        /// <param name="otherEntity"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static Entity JoinAttributes(this Entity e, Entity otherEntity, ColumnSet columnSet, string alias, XrmFakedContext context)
        {
            if (otherEntity == null) return e; //Left Join where otherEntity was not matched

            otherEntity = otherEntity.Clone(); //To avoid joining entities from/to the same entities, which would cause collection modified exceptions

            if (columnSet.AllColumns)
            {
                foreach (var attKey in otherEntity.Attributes.Keys)
                {
                    e[alias + "." + attKey] = new AliasedValue(otherEntity.LogicalName, attKey, otherEntity[attKey]);
                }

                foreach (var attKey in otherEntity.FormattedValues.Keys)
                {
                    e.FormattedValues[alias + "." + attKey] = otherEntity.FormattedValues[attKey];
                }
            }
            else
            {
                //Return selected list of attributes
                foreach (var attKey in columnSet.Columns)
                {
                    if (!context.AttributeExistsInMetadata(otherEntity.LogicalName, attKey))
                    {
                        FakeOrganizationServiceFault.Throw(ErrorCodes.QueryBuilderNoAttribute, string.Format("The attribute {0} does not exist on this entity.", attKey));
                    }

                    if (otherEntity.Attributes.ContainsKey(attKey))
                    {
                        e[alias + "." + attKey] = new AliasedValue(otherEntity.LogicalName, attKey, otherEntity[attKey]);
                    }
                    else
                    {
                        e[alias + "." + attKey] = new AliasedValue(otherEntity.LogicalName, attKey, null);
                    }

                    if (otherEntity.FormattedValues.ContainsKey(attKey))
                    {
                        e.FormattedValues[alias + "." + attKey] = otherEntity.FormattedValues[attKey];
                    }
                }
            }
            return e;
        }

        public static Entity JoinAttributes(this Entity e, IEnumerable<Entity> otherEntities, ColumnSet columnSet, string alias, XrmFakedContext context)
        {
            foreach (var otherEntity in otherEntities)
            {
                var otherClonedEntity = otherEntity.Clone(); //To avoid joining entities from/to the same entities, which would cause collection modified exceptions

                if (columnSet.AllColumns)
                {
                    foreach (var attKey in otherClonedEntity.Attributes.Keys)
                    {
                        e[alias + "." + attKey] = new AliasedValue(otherEntity.LogicalName, attKey, otherClonedEntity[attKey]);
                    }

                    foreach (var attKey in otherEntity.FormattedValues.Keys)
                    {
                        e.FormattedValues[alias + "." + attKey] = otherEntity.FormattedValues[attKey];
                    }
                }
                else
                {
                    //Return selected list of attributes
                    foreach (var attKey in columnSet.Columns)
                    {
                        if (!context.AttributeExistsInMetadata(otherEntity.LogicalName, attKey))
                        {
                            FakeOrganizationServiceFault.Throw(ErrorCodes.QueryBuilderNoAttribute, string.Format("The attribute {0} does not exist on this entity.", attKey));
                        }

                        if (otherClonedEntity.Attributes.ContainsKey(attKey))
                        {
                            e[alias + "." + attKey] = new AliasedValue(otherEntity.LogicalName, attKey, otherClonedEntity[attKey]);
                        }
                        else
                        {
                            e[alias + "." + attKey] = new AliasedValue(otherEntity.LogicalName, attKey, null);
                        }

                        if (otherEntity.FormattedValues.ContainsKey(attKey))
                        {
                            e.FormattedValues[alias + "." + attKey] = otherEntity.FormattedValues[attKey];
                        }
                    }
                }
            }
            return e;
        }

        /// <summary>
        /// Returns the key for the attribute name selected (could an entity reference or a primary key or a guid)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sAttributeName"></param>
        /// <returns></returns>
        public static object KeySelector(this Entity e, string sAttributeName, XrmFakedContext context)
        {
            if (sAttributeName.Contains("."))
            {
                //Do not lowercase the alias prefix
                var splitted = sAttributeName.Split('.');
                sAttributeName = string.Format("{0}.{1}", splitted[0], splitted[1].ToLower());
            }
            else
            {
                sAttributeName = sAttributeName.ToLower();
            }

            if (!e.Attributes.ContainsKey(sAttributeName))
            {
                //Check if it is the primary key
                if (sAttributeName.Contains("id") &&
                   e.LogicalName.ToLower().Equals(sAttributeName.Substring(0, sAttributeName.Length - 2)))
                {
                    return e.Id;
                }
                return Guid.Empty; //Atrribute is null or doesn´t exists so it can´t be joined
            }

            object keyValue = null;
            AliasedValue aliasedValue;
            if ((aliasedValue = e[sAttributeName] as AliasedValue) != null)
            {
                keyValue = aliasedValue.Value;
            }
            else
            {
                keyValue = e[sAttributeName];
            }

            EntityReference entityReference = keyValue as EntityReference;
            if (entityReference != null)
                return entityReference.Id;

            OptionSetValue optionSetValue = keyValue as OptionSetValue;
            if (optionSetValue != null)
                return optionSetValue.Value;

            Money money = keyValue as Money;
            if (money != null)
                return money.Value;

            return keyValue;
        }

        /// <summary>
        /// Extension method to "hack" internal set properties on sealed classes via reflection
        /// </summary>
        /// <param name="e"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void Inject(this Entity e, string property, object value)
        {
            e.GetType().GetProperty(property).SetValue(e, value, null);
        }

        public static void SetValueIfEmpty(this Entity e, string property, object value)
        {
            var containsKey = e.Attributes.ContainsKey(property);
            if (!containsKey || containsKey && e[property] == null)
            {
                e[property] = value;
            }
        }

        /// <summary>
        /// ToEntityReference implementation which converts an entity into an entity reference with key attribute info as well
        /// </summary>
        /// <param name="e">Entity to convert to an Entity Reference</param>
        /// <returns></returns>
        public static EntityReference ToEntityReferenceWithKeyAttributes(this Entity e)
        {
            var result = e.ToEntityReference();
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
            result.KeyAttributes = e.KeyAttributes;
#endif
            return result;
        }


    }
}