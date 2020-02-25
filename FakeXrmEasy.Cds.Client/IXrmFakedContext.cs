using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeXrmEasy
{
    public interface IXrmContext
    {
        /// <summary>
        /// Receives a list of entities, that are used to initialize the context with those
        /// </summary>
        /// <param name="entities"></param>
        void Initialize(IEnumerable<Entity> entities);

        /// <summary>
        /// Returns an instance of an organization service
        /// </summary>
        /// <returns></returns>
        IOrganizationService GetOrganizationService();

        /// <summary>
        /// DEPRECATED: Consider using GetOrganizationService instead
        /// </summary>
        /// <returns></returns>
        IOrganizationService GetFakedOrganizationService();

        ///// <summary>
        ///// Returns a faked organization service proxy that will execute CRUD in-memory operations and other requests against this faked context
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //OrganizationServiceProxy GetFakedOrganizationServiceProxy();


        /// <summary>
        /// Returns a faked service endpoint notification service
        /// </summary>
        /// <returns></returns>
        IServiceEndpointNotificationService GetFakedServiceEndpointNotificationService();

        /// <summary>
        /// Receives a strong-typed entity type and returns a Queryable of that type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> CreateQuery<T>()
            where T : Entity;
    }
}