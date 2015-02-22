using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace FakeXrmEasy
{
    public interface IXrmFakedContext
    {
        /// <summary>
        /// Receives a list of entities, that are used to initialize the context with those
        /// </summary>
        /// <param name="entities"></param>
        void Initialize(IEnumerable<Entity> entities);

        /// <summary>
        /// Returns a faked organization service that will execute CRUD in-memory operations and other requests against this faked context 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IOrganizationService GetFakedOrganizationService();

        /// <summary>
        /// Receives a strong-typed entity type and returns a Queryable of that type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> CreateQuery<T>() where T : Entity;

        /// <summary>
        /// Returns a faked plugin that will be executed against this faked context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWithTarget<T>(object target) where T : IPlugin, new();


        /// <summary>
        /// Returns a faked plugin with a target and the specified pre entity images
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWithTargetAndPreEntityImages<T>(object target, EntityImageCollection preEntityImages) where T : IPlugin, new();


        /// <summary>
        /// Most flexible plugin execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWith<T>(ParameterCollection inputParameters,
                                     ParameterCollection outputParameters,
                                     EntityImageCollection preEntityImages,
                                     EntityImageCollection postEntityImages) where T : IPlugin, new();

        /// <summary>
        /// Executes a code activity against this context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        IDictionary<string, object> ExecuteCodeActivity<T>(Dictionary<string, object> inputs) where T : CodeActivity, new();


        /// <summary>
        /// Executes a code activity passing the primary entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        IDictionary<string, object> ExecuteCodeActivity<T>(Entity primaryEntity, Dictionary<string, object> inputs) where T : CodeActivity, new();
        
    }

  
}
