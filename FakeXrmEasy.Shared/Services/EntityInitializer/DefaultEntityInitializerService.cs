using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xrm.Sdk;

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

            if (!e.Attributes.ContainsKey("createdon"))
                e["createdon"] = DateTime.UtcNow;

            if (!e.Attributes.ContainsKey("modifiedon"))
                e["modifiedon"] = DateTime.UtcNow;

            if (!e.Attributes.ContainsKey("createdby"))
                e["createdby"] = CallerId;

            if (!e.Attributes.ContainsKey("modifiedby"))
                e["modifiedby"] = CallerId;

            if (!e.Attributes.ContainsKey("statecode"))
                e["statecode"] = new OptionSetValue(0); //Active by default


            return e;
        }

        public Entity Initialize(Entity e)
        {
            return this.Initialize(e, Guid.NewGuid());
        }
    }
}
