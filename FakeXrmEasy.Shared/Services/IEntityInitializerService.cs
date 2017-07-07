using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Services
{
    public interface IEntityInitializerService
    {
        Entity Initialize(Entity e);

        Entity Initialize(Entity e, Guid gCallerId);
    }
}