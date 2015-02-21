using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Messages;
using System.Dynamic;
using System.Linq.Expressions;
using FakeXrmEasy.Extensions;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmFakedContext
    {

        /// <summary>
        /// Executes the plugin of type T against the faked context for an entity target
        /// and returns the faked plugin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public IPlugin ExecutePluginWithTarget<T>(Entity target) where T: IPlugin, new()
        {
            var fakedServiceProvider = GetFakedServiceProvider(target);

            var fakedPlugin = A.Fake<IPlugin>();
            A.CallTo(() => fakedPlugin.Execute(A<IServiceProvider>._))
                .Invokes((IServiceProvider provider) =>
                {
                    var plugin = new T();
                    plugin.Execute(fakedServiceProvider);
                });

            fakedPlugin.Execute(fakedServiceProvider); //Execute the plugin
            return fakedPlugin;
        }

        protected IPluginExecutionContext GetFakedPluginContext(Entity target)
        {
            var context = A.Fake<IPluginExecutionContext>();

            //Add the target entity to the InputParameters
            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);
            
            A.CallTo(() => context.Depth).ReturnsLazily(() => 1);
            A.CallTo(() => context.IsExecutingOffline).ReturnsLazily(() => false);
            A.CallTo(() => context.PrimaryEntityId).ReturnsLazily(() => target.Id);
            A.CallTo(() => context.PrimaryEntityName).ReturnsLazily(() => target.LogicalName);
            A.CallTo(() => context.InputParameters).ReturnsLazily(() => inputParameters);
            
            return context;
        }
        protected IServiceProvider GetFakedServiceProvider(Entity target)
        {

            var fakedServiceProvider = A.Fake<IServiceProvider>();

            A.CallTo(() => fakedServiceProvider.GetService(A<Type>._))
               .ReturnsLazily((Type t) =>
               {
                   if (t.Equals(typeof(IOrganizationService))) {
                       //Return faked organization service
                       return GetFakedOrganizationService();
                   } 
                   else if (t.Equals(typeof(ITracingService)))
                   {
                       return new XrmFakedTracingService();
                   }
                   else if (t.Equals(typeof(IPluginExecutionContext)))
                   {
                       return GetFakedPluginContext(target);   
                   }
                   throw new PullRequestException("The specified service type is not supported");
               });

            return fakedServiceProvider;
        }
    }
}