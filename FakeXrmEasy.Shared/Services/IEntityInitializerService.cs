using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Services
{
    public interface IEntityInitializerService
    {
        Entity Initialize(Entity e, XrmFakedContext ctx, bool isManyToManyRelationshipEntity = false);
        Entity Initialize(Entity e, Guid gCallerId, XrmFakedContext ctx, bool isManyToManyRelationshipEntity = false);
    }



}
