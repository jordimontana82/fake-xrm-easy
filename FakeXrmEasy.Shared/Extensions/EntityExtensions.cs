using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using FakeXrmEasy.OrganizationFaults;

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

        public static void ProjectAttributes(Entity e, Entity projected, LinkEntity le, XrmFakedContext context)
        {
            var sAlias = string.IsNullOrWhiteSpace(le.EntityAlias) ? le.LinkToEntityName : le.EntityAlias;

            if (le.Columns.AllColumns && le.Columns.Columns.Count == 0)
            {
                foreach (var attKey in e.Attributes.Keys)
                {
                    if(attKey.StartsWith(sAlias + "."))
                    {
                        projected[attKey] = e[attKey];
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
                }
            }
            

            foreach (var nestedLinkedEntity in le.LinkEntities)
            {
                ProjectAttributes(e, projected, nestedLinkedEntity, context);
            }
        }

        public static Entity ProjectAttributes(this Entity e, QueryExpression qe, XrmFakedContext context)
        {
            if (qe.ColumnSet == null) return e;

            if (qe.ColumnSet.AllColumns)
            {
                return e; //return all the original attributes
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
                        OrganizationServiceFaultQueryBuilderNoAttributeException.Throw(attKey);
                    }

                    if (e.Attributes.ContainsKey(attKey))
                        projected[attKey] = e[attKey];
                    else
                    {
                        projected[attKey] = null;
                    }
                }

                //Plus attributes from joins
                foreach(var le in qe.LinkEntities)
                {
                    ProjectAttributes(e, projected, le, context);
                }
                //foreach (var attKey in e.Attributes.Keys)
                //{
                //    if(e[attKey] is AliasedValue && !projected.Attributes.ContainsKey(attKey))
                //        projected[attKey] = e[attKey];
                //}
                return projected;
            }
        }

        public static Entity Clone(this Entity e)
        {
            var cloned = new Entity(e.LogicalName);
            foreach (var attKey in e.Attributes.Keys)
            {
                cloned[attKey] = e[attKey];
            }
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
                    e[alias + "." + attKey] = new AliasedValue(alias, attKey, otherEntity[attKey]);
                }
            }
            else
            {
                //Return selected list of attributes
                foreach (var attKey in columnSet.Columns)
                {
                    if (!context.AttributeExistsInMetadata(otherEntity.LogicalName, attKey))
                    {
                        OrganizationServiceFaultQueryBuilderNoAttributeException.Throw(attKey);
                    }

                    if (otherEntity.Attributes.ContainsKey(attKey))
                    {
                        e[alias + "." + attKey] = new AliasedValue(alias, attKey, otherEntity[attKey]);
                    }
                    else
                    {
                        e[alias + "." + attKey] = new AliasedValue(alias, attKey, null);
                    }
                    
                }
            }
            return e;
        }

        public static Entity JoinAttributes(this Entity e, IEnumerable<Entity> otherEntities, ColumnSet columnSet, string alias, XrmFakedContext context)
        {
            foreach (var otherEntity in otherEntities) {
                var otherClonedEntity = otherEntity.Clone(); //To avoid joining entities from/to the same entities, which would cause collection modified exceptions

                if (columnSet.AllColumns)
                {
                    foreach (var attKey in otherClonedEntity.Attributes.Keys)
                    {
                        e[alias + "." + attKey] = new AliasedValue(alias, attKey, otherClonedEntity[attKey]);
                    }
                }
                else
                {
                    //Return selected list of attributes
                    foreach (var attKey in columnSet.Columns)
                    {
                        if (!context.AttributeExistsInMetadata(otherEntity.LogicalName, attKey))
                        {
                            OrganizationServiceFaultQueryBuilderNoAttributeException.Throw(attKey);
                        }

                        if (otherClonedEntity.Attributes.ContainsKey(attKey))
                        {
                            e[alias + "." + attKey] = new AliasedValue(alias, attKey, otherClonedEntity[attKey]);
                        }
                        else {
                            e[alias + "." + attKey] = new AliasedValue(alias, attKey, null);
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
        public static Guid KeySelector(this Entity e, string sAttributeName, XrmFakedContext context)
        {
            if(sAttributeName.Contains("."))
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
            if(e[sAttributeName] is AliasedValue)
            {
                keyValue = (e[sAttributeName] as AliasedValue).Value;
            }
            else
            {
                keyValue = e[sAttributeName];
            }

            if (keyValue is EntityReference) 
                return (keyValue as EntityReference).Id;
            if (keyValue is Guid)
                return ((Guid)keyValue);

            return Guid.Empty;
        }
        
         
    }
}
