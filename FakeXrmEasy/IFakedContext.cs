using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy
{
    public interface IFakedContext
    {

        /// <summary>
        /// Deprecated and should not be used
        /// </summary>
        /// <param name="data"></param>
        void Build(XrmFakedContext data);

        /// <summary>
        /// Receives a list of entities, that are used to initialize the context with those
        /// </summary>
        /// <param name="entities"></param>
        void Initialize(IEnumerable<Entity> entities);

        /// <summary>
        /// Returns a faked organization service that will execute CRUD in-memoery operations against this faked context 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IOrganizationService GetFakedOrganizationService(XrmFakedContext context);

        /// <summary>
        /// Receives a strong-typed entity type and returns a Queryable of that type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> CreateQuery<T>() where T : Entity; 
    }

  
}
