using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

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
        public static Entity ProjectAttributes(this Entity e, ColumnSet columnSet)
        {
            if (columnSet == null) return e;

            if (columnSet.AllColumns)
            {
                return e; //return all the original attributes
            }
            else
            {
                //Return selected list of attributes
                var projected = new Entity(e.LogicalName) { Id = e.Id };

                foreach (var attKey in columnSet.Columns)
                {
                    if(e.Attributes.ContainsKey(attKey))
                        projected[attKey] = e[attKey];
                }
                return projected;
            }
        }

        /// <summary>
        /// Extension method to join the attributes of entity e and otherEntity
        /// </summary>
        /// <param name="e"></param>
        /// <param name="otherEntity"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static Entity JoinAttributes(this Entity e, Entity otherEntity, ColumnSet columnSet, string alias)
        {
            if (otherEntity == null) return e; //Left Join where otherEntity was not matched

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
                    e[alias + "." + attKey] = new AliasedValue(alias, attKey, otherEntity[attKey]);
                }
            }
            return e;
        }

        public static Entity JoinAttributes(this Entity e, IEnumerable<Entity> otherEntities, ColumnSet columnSet, string alias)
        {
            foreach (var otherEntity in otherEntities) { 
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
                        e[alias + "." + attKey] = new AliasedValue(alias, attKey, otherEntity[attKey]);
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
        public static Guid KeySelector(this Entity e, string sAttributeName)
        {
            sAttributeName = sAttributeName.ToLower();

            if (!e.Attributes.ContainsKey(sAttributeName))
            {
                //Check if it is the primery key
                if (sAttributeName.Contains("id") &&
                   e.LogicalName.ToLower().Equals(sAttributeName.Substring(0, sAttributeName.Length - 2)))
                {
                    return e.Id;
                }
                return Guid.Empty; //Atrribute is null or doesn´t exists so it can´t be joined
            } 

            if (e[sAttributeName] is EntityReference) 
                return (e[sAttributeName] as EntityReference).Id;
            if (e[sAttributeName] is Guid)
                return ((Guid)e[sAttributeName]);

            return Guid.Empty;
        }
        
         
    }
}
