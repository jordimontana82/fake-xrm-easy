using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy
{
    public interface IFakedContextFactory
    {
        void Build(XrmFakedContext data);
        void Initialize(IEnumerable<Entity> entities);
        IOrganizationService GetFakedOrganizationService(XrmFakedContext context);
    }

  
}
