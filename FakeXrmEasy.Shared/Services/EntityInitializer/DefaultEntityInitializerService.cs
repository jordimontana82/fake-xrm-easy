using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xrm.Sdk;
using FakeXrmEasy.Extensions;

namespace FakeXrmEasy.Services
{
    public class DefaultEntityInitializerService : IEntityInitializerService
    {
        public Entity Initialize(Entity e, Guid gCallerId)
        {
            //Validate primary key for dynamic entities
            var primaryKey = string.Format("{0}id", e.LogicalName);
            if (!e.Attributes.ContainsKey(primaryKey))
            {
                e[primaryKey] = e.Id;
            }

            var CallerId = new EntityReference("systemuser", gCallerId); //Create a new instance by default

            var now = DateTime.UtcNow;
            e.SetValueIfEmpty("createdon", now);
            e.SetValueIfEmpty("modifiedon", now);
            e.SetValueIfEmpty("createdby", CallerId);
            e.SetValueIfEmpty("modifiedby", CallerId);
            e.SetValueIfEmpty("statecode", new OptionSetValue(0)); //Active by default

            return e;
        }

        public Entity Initialize(Entity e)
        {
            return this.Initialize(e, Guid.NewGuid());
        }
    }
}
