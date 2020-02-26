﻿using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using FakeXrmEasy.Extensions;
using System.Reflection;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using FakeXrmEasy.Metadata;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmContext
    {
        /// <summary>
        /// Stores some minimal metadata info if dynamic entities are used and no injected metadata was used
        /// </summary>
        protected internal Dictionary<string, Dictionary<string, string>> AttributeMetadataNames { get; set; }

        /// <summary>
        /// Stores fake global option set metadata
        /// </summary>
        public Dictionary<string, OptionSetMetadata> OptionSetValuesMetadata { get; set; }

        /// <summary>
        /// Stores fake global status values metadata
        /// </summary>
        public Dictionary<string, StatusAttributeMetadata> StatusAttributeMetadata { get; set; }

        /// <summary>
        /// Stores fake entity metadata
        /// </summary>
        protected internal Dictionary<string, EntityMetadata> EntityMetadata { get; set; }


        public void InitializeMetadata(IEnumerable<EntityMetadata> entityMetadataList)
        {
            if (entityMetadataList == null)
            {
                throw new Exception("Entity metadata parameter can not be null");
            }

            //  this.EntityMetadata = new Dictionary<string, EntityMetadata>();
            foreach (var eMetadata in entityMetadataList)
            {
                if (string.IsNullOrWhiteSpace(eMetadata.LogicalName))
                {
                    throw new Exception("An entity metadata record must have a LogicalName property.");
                }

                if (EntityMetadata.ContainsKey(eMetadata.LogicalName))
                {
                    throw new Exception("An entity metadata record with the same logical name was previously added. ");
                }
                EntityMetadata.Add(eMetadata.LogicalName, eMetadata.Copy());
            }
        }

        public void InitializeMetadata(EntityMetadata entityMetadata)
        {
            this.InitializeMetadata(new List<EntityMetadata>() { entityMetadata });
        }

        public void InitializeMetadata(Assembly earlyBoundEntitiesAssembly)
        {
            IEnumerable<EntityMetadata> entityMetadatas = MetadataGenerator.FromEarlyBoundEntities(earlyBoundEntitiesAssembly);
            if (entityMetadatas.Any())
            {
                this.InitializeMetadata(entityMetadatas);
            }
        }

        public IQueryable<EntityMetadata> CreateMetadataQuery()
        {
            return this.EntityMetadata.Values
                    .Select(em => em.Copy())
                    .ToList()
                    .AsQueryable();
        }

        public EntityMetadata GetEntityMetadataByName(string sLogicalName)
        {
            if (EntityMetadata.ContainsKey(sLogicalName))
                return EntityMetadata[sLogicalName].Copy();

            return null;
        }

        public void SetEntityMetadata(EntityMetadata em)
        {
            if (this.EntityMetadata.ContainsKey(em.LogicalName))
                this.EntityMetadata[em.LogicalName] = em.Copy();
            else
                this.EntityMetadata.Add(em.LogicalName, em.Copy());
        }

        public AttributeMetadata GetAttributeMetadataFor(string sEntityName, string sAttributeName, Type attributeType)
        {
            if (EntityMetadata.ContainsKey(sEntityName))
            {
                var entityMetadata = GetEntityMetadataByName(sEntityName);
                var attribute = entityMetadata.Attributes
                                .Where(a => a.LogicalName.Equals(sAttributeName))
                                .FirstOrDefault();

                if (attribute != null)
                    return attribute;
            }

            if (attributeType == typeof(string))
            {
                return new StringAttributeMetadata(sAttributeName);
            }
            //Default
            return new StringAttributeMetadata(sAttributeName);
        }

    }
}