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
        /// Extension method to join the attributes of entity e and otherEntity
        /// </summary>
        /// <param name="e"></param>
        /// <param name="otherEntity"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static Entity JoinAttributes(this Entity e, Entity otherEntity, ColumnSet columnSet, string alias)
        {
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
        
         
    }
}
