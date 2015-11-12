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

        public IPlugin ExecutePluginWith<T>(ParameterCollection inputParameters,
                                     ParameterCollection outputParameters,
                                     EntityImageCollection preEntityImages,
                                     EntityImageCollection postEntityImages) where T : IPlugin, new()
        {
            var fakedServiceProvider = GetFakedServiceProvider(inputParameters, outputParameters, preEntityImages, postEntityImages);

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

        public IPlugin ExecutePluginWith<T>(ParameterCollection inputParameters,
                                     ParameterCollection outputParameters,
                                     EntityImageCollection preEntityImages,
                                     EntityImageCollection postEntityImages,
                                     string unsecureConfiguration,
                                     string secureConfiguration) where T : IPlugin, new()
        {
            var fakedServiceProvider = GetFakedServiceProvider(inputParameters, outputParameters, preEntityImages, postEntityImages);
            var fakedPlugin = A.Fake<IPlugin>();

            A.CallTo(() => fakedPlugin.Execute(A<IServiceProvider>._))
                .Invokes((IServiceProvider provider) =>
                {
                    var pluginType = typeof (T);
                    var constructors = pluginType.GetConstructors().ToList();

                    if (!constructors.Any(
                            constructor => constructor.GetParameters().ToList().Count == 2
                                && constructor.GetParameters().All(param => param.ParameterType == typeof(string))))
                    {
                        throw new ArgumentException("The plugin you are trying to execute does not specify a constructor for passing in two configuration strings.");
                    }
                    
                    var plugin = (T)Activator.CreateInstance(pluginType, unsecureConfiguration, secureConfiguration);
                    plugin.Execute(fakedServiceProvider);
                });

            fakedPlugin.Execute(fakedServiceProvider); //Execute the plugin
            return fakedPlugin;
        }

        /// <summary>
        /// Executes the plugin of type T against the faked context for an entity target
        /// and returns the faked plugin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public IPlugin ExecutePluginWithTarget<T>(object target) where T: IPlugin, new()
        {
            //Add the target entity to the InputParameters
            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);

            return this.ExecutePluginWith<T>(inputParameters, null, null, null);
        }

        public IPlugin ExecutePluginWithTargetAndPreEntityImages<T>(object target, EntityImageCollection preEntityImages) where T : IPlugin, new()
        {
            //Add the target entity to the InputParameters
            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);

            return this.ExecutePluginWith<T>(inputParameters, null, preEntityImages, null);
        }

        protected IPluginExecutionContext GetFakedPluginContext(ParameterCollection inputParameters,
                                                            ParameterCollection outputParameters,
                                                            EntityImageCollection preEntityImages,
                                                            EntityImageCollection postEntityImages)
        {
            var context = A.Fake<IPluginExecutionContext>();

            A.CallTo(() => context.Depth).ReturnsLazily(() => 1);
            A.CallTo(() => context.IsExecutingOffline).ReturnsLazily(() => false);
            A.CallTo(() => context.InputParameters).ReturnsLazily(() => inputParameters != null ? inputParameters : new ParameterCollection());
            A.CallTo(() => context.OutputParameters).ReturnsLazily(() => outputParameters != null ? outputParameters : new ParameterCollection());
            A.CallTo(() => context.PreEntityImages).ReturnsLazily(() => preEntityImages);
            A.CallTo(() => context.PostEntityImages).ReturnsLazily(() => postEntityImages);

            //Create message will pass an Entity as the target but this is not always true
            //For instance, a Delete request will receive an EntityReference
            if (inputParameters != null &&
                inputParameters.ContainsKey("Target") &&
                inputParameters["Target"] is Entity)
            {
                var target = inputParameters["Target"] as Entity;
                A.CallTo(() => context.PrimaryEntityId).ReturnsLazily(() => target.Id);
                A.CallTo(() => context.PrimaryEntityName).ReturnsLazily(() => target.LogicalName);
            }

            return context;
        }
        protected IServiceProvider GetFakedServiceProvider(ParameterCollection inputParameters, 
                                                            ParameterCollection outputParameters,
                                                            EntityImageCollection preEntityImages,
                                                            EntityImageCollection postEntityImages)
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
                       return GetFakedPluginContext(inputParameters, outputParameters, preEntityImages, postEntityImages);   
                   }
                   else if (t.Equals(typeof(IOrganizationServiceFactory)))
                   {
                       var fakedServiceFactory = A.Fake<IOrganizationServiceFactory>();
                       A.CallTo(() => fakedServiceFactory.CreateOrganizationService(A<Guid?>._))
                            .ReturnsLazily((Guid? g) =>
                            {
                                return GetFakedOrganizationService();
                            });
                       return fakedServiceFactory;
                   }
                   throw new PullRequestException("The specified service type is not supported");
               });

            return fakedServiceProvider;
        }
    }
}