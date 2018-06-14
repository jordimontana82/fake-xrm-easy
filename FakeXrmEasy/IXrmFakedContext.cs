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
        /// Returns a faked plugin that will be executed against this faked context and the entity passed as the target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWithTarget<T>(Entity target) where T : IPlugin, new();


        /// <summary>
        /// Returns a faked plugin with a target and the specified pre entity images
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWithTargetAndPreEntityImages<T>(object target, EntityImageCollection preEntityImages) where T : IPlugin, new();

        /// <summary>
        /// Returns a faked plugin with a target and the specified post entity images
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWithTargetAndPostEntityImages<T>(object target, EntityImageCollection postEntityImages) where T : IPlugin, new();

        /// <summary>
        /// Execute a plugin with input and output params, as well as entity images
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPlugin ExecutePluginWith<T>(ParameterCollection inputParameters,
                                     ParameterCollection outputParameters,
                                     EntityImageCollection preEntityImages,
                                     EntityImageCollection postEntityImages) where T : IPlugin, new();


        /// <summary>
        /// Returns a plugin context with default properties one can override
        /// </summary>
        /// <returns></returns>
        XrmFakedPluginExecutionContext GetDefaultPluginContext();

        /// <summary>
        /// Executes a plugin passing a custom context. This is useful whenever we need to mock more complex plugin contexts (ex: passing MessageName, plugin Depth, InitiatingUserId etc...)
        /// </summary>
        /// <typeparam name="T">Must be a plugin</typeparam>
        /// <param name="ctx"></param>
        /// <returns></returns>
        IPlugin ExecutePluginWith<T>(XrmFakedPluginExecutionContext ctx) where T : IPlugin, new();


        /// <summary>
        /// Executes a plugin with a custom context and custom configurations (configurations aren't inherent properties of the context so they need to be passed separately)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="plugCtx"></param>
        /// <param name="unsecureConfiguration"></param>
        /// <param name="secureConfiguration"></param>
        /// <returns></returns>
        IPlugin ExecutePluginWithConfigurations<T>(XrmFakedPluginExecutionContext plugCtx,
                                     string unsecureConfiguration,
                                     string secureConfiguration) where T : IPlugin, new();


        /// <summary>
        /// Executes a code activity against this context
        /// An optional instance can be passed. 
        /// This is useful when the codeactivity requires additional mocks that could be stored in the codeactivity itself
        /// </summary>
        /// <typeparam name="T"></typeparam>
        IDictionary <string, object> ExecuteCodeActivity<T>(Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new();


        /// <summary>
        /// Executes a code activity passing the primary entity
        /// This is useful when the codeactivity requires additional mocks that could be stored in the codeactivity itself
        /// </summary>
        /// <typeparam name="T"></typeparam>
        IDictionary<string, object> ExecuteCodeActivity<T>(Entity primaryEntity, Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new();
        
    }

  
}
