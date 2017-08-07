using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Services
{
    public interface IEntityInitializerService
    {
        Entity Initialize(Entity e, bool isManyToManyRelationshipEntity = false);
        Entity Initialize(Entity e, Guid gCallerId, bool isManyToManyRelationshipEntity = false);
    }



}
