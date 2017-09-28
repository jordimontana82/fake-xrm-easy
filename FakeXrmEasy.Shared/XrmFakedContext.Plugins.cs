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
    public partial class XrmFakedContext : IXrmContext
    {
        public XrmFakedPluginExecutionContext GetDefaultPluginContext()
        {
            var userId = CallerId != null ? CallerId.Id : Guid.NewGuid();
            return new XrmFakedPluginExecutionContext()
            {
                Depth = 1,
                IsExecutingOffline = false,
                MessageName = "Create",
                UserId = userId,
                InitiatingUserId = userId,
                InputParameters = new ParameterCollection(),
                OutputParameters = new ParameterCollection(),
                SharedVariables = new ParameterCollection(),
                PreEntityImages = new EntityImageCollection(),
                PostEntityImages = new EntityImageCollection()
            };
        }

        protected IPluginExecutionContext GetFakedPluginContext(XrmFakedPluginExecutionContext ctx)
        {
            var context = A.Fake<IPluginExecutionContext>();

            PopulateExecutionContextPropertiesFromFakedContext(context, ctx);

            A.CallTo(() => context.ParentContext).ReturnsLazily(() => ctx.ParentContext);
            A.CallTo(() => context.Stage).ReturnsLazily(() => ctx.Stage);

            return context;
        }

        protected void PopulateExecutionContextPropertiesFromFakedContext(IExecutionContext context, XrmFakedPluginExecutionContext ctx)
        {
            var newUserId = Guid.NewGuid();

            A.CallTo(() => context.Depth).ReturnsLazily(() => ctx.Depth <= 0 ? 1 : ctx.Depth);
            A.CallTo(() => context.IsExecutingOffline).ReturnsLazily(() => ctx.IsExecutingOffline);
            A.CallTo(() => context.InputParameters).ReturnsLazily(() => ctx.InputParameters);
            A.CallTo(() => context.OutputParameters).ReturnsLazily(() => ctx.OutputParameters);
            A.CallTo(() => context.PreEntityImages).ReturnsLazily(() => ctx.PreEntityImages);
            A.CallTo(() => context.PostEntityImages).ReturnsLazily(() => ctx.PostEntityImages);
            A.CallTo(() => context.MessageName).ReturnsLazily(() => ctx.MessageName);
            A.CallTo(() => context.Mode).ReturnsLazily(() => ctx.Mode);
            A.CallTo(() => context.OrganizationName).ReturnsLazily(() => ctx.OrganizationName);
            A.CallTo(() => context.OrganizationId).ReturnsLazily(() => ctx.OrganizationId);
            A.CallTo(() => context.InitiatingUserId).ReturnsLazily(() => ctx.InitiatingUserId == Guid.Empty ? newUserId : ctx.InitiatingUserId);
            A.CallTo(() => context.UserId).ReturnsLazily(() => ctx.UserId == Guid.Empty ? newUserId : ctx.UserId);
            A.CallTo(() => context.PrimaryEntityName).ReturnsLazily(() => ctx.PrimaryEntityName);
            A.CallTo(() => context.SecondaryEntityName).ReturnsLazily(() => ctx.SecondaryEntityName);
            A.CallTo(() => context.SharedVariables).ReturnsLazily(() => ctx.SharedVariables);
            A.CallTo(() => context.BusinessUnitId).ReturnsLazily(() => ctx.BusinessUnitId);
            A.CallTo(() => context.CorrelationId).ReturnsLazily(() => ctx.CorrelationId);

            //Create message will pass an Entity as the target but this is not always true
            //For instance, a Delete request will receive an EntityReference
            if (ctx.InputParameters != null &&
                ctx.InputParameters.ContainsKey("Target") &&
                ctx.InputParameters["Target"] is Entity)
            {
                var target = ctx.InputParameters["Target"] as Entity;
                A.CallTo(() => context.PrimaryEntityId).ReturnsLazily(() => target.Id);
                A.CallTo(() => context.PrimaryEntityName).ReturnsLazily(() => target.LogicalName);
            }
        }
        protected IExecutionContext GetFakedExecutionContext(XrmFakedPluginExecutionContext ctx)
        {
            var context = A.Fake<IExecutionContext>();

            PopulateExecutionContextPropertiesFromFakedContext(context, ctx);

            return context;
        }

        public IPlugin ExecutePluginWith<T>(XrmFakedPluginExecutionContext ctx, T instance) where T : IPlugin, new()
        {
            var fakedServiceProvider = GetFakedServiceProvider(ctx);

            var fakedPlugin = A.Fake<IPlugin>();
            A.CallTo(() => fakedPlugin.Execute(A<IServiceProvider>._))
                .Invokes((IServiceProvider provider) =>
                {
                    var plugin = instance;
                    plugin.Execute(fakedServiceProvider);
                });

            fakedPlugin.Execute(fakedServiceProvider); //Execute the plugin
            return fakedPlugin;
        }

        public IPlugin ExecutePluginWith<T>(XrmFakedPluginExecutionContext ctx = null) where T : IPlugin, new()
        {
            if (ctx == null)
                ctx = GetDefaultPluginContext();

            return this.ExecutePluginWith(ctx, new T());
        }

        public IPlugin ExecutePluginWith<T>(ParameterCollection inputParameters,
                                     ParameterCollection outputParameters,
                                     EntityImageCollection preEntityImages,
                                     EntityImageCollection postEntityImages) where T : IPlugin, new()
        {
            var ctx = new XrmFakedPluginExecutionContext();
            ctx.InputParameters = inputParameters;
            ctx.PreEntityImages = preEntityImages;
            ctx.OutputParameters = outputParameters;
            ctx.PostEntityImages = postEntityImages;
            
            var fakedServiceProvider = GetFakedServiceProvider(ctx);

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

        public IPlugin ExecutePluginWithConfigurations<T>(XrmFakedPluginExecutionContext plugCtx,
                                     string unsecureConfiguration,
                                     string secureConfiguration) where T : class, IPlugin 
        {
            var pluginType = typeof(T);
            var constructors = pluginType.GetConstructors().ToList();

            if (!constructors.Any(
                            constructor => constructor.GetParameters().ToList().Count == 2
                                && constructor.GetParameters().All(param => param.ParameterType == typeof(string))))
            {
                throw new ArgumentException("The plugin you are trying to execute does not specify a constructor for passing in two configuration strings.");
            }

            var pluginInstance = (T)Activator.CreateInstance(typeof(T), unsecureConfiguration, secureConfiguration);
            return this.ExecutePluginWithConfigurations<T>(plugCtx, pluginInstance, unsecureConfiguration, secureConfiguration);
          
        }

        public IPlugin ExecutePluginWithConfigurations<T>(XrmFakedPluginExecutionContext plugCtx, 
                                     T instance,
                                     string unsecureConfiguration,
                                     string secureConfiguration) where T : class, IPlugin
        {
            var fakedServiceProvider = GetFakedServiceProvider(plugCtx);

            var fakedPlugin = A.Fake<IPlugin>();

            A.CallTo(() => fakedPlugin.Execute(A<IServiceProvider>._))
                .Invokes((IServiceProvider provider) =>
                {
                    var pluginType = typeof(T);
                    var constructors = pluginType.GetConstructors().ToList();

                    if (!constructors.Any(
                            constructor => constructor.GetParameters().ToList().Count == 2
                                && constructor.GetParameters().All(param => param.ParameterType == typeof(string))))
                    {
                        throw new ArgumentException("The plugin you are trying to execute does not specify a constructor for passing in two configuration strings.");
                    }

                    var plugin = instance;
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
        public IPlugin ExecutePluginWithTarget<T>(Entity target) where T: IPlugin, new()
        {
            //Add the target entity to the InputParameters
            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);

            XrmFakedPluginExecutionContext ctx = GetDefaultPluginContext();
            ctx.InputParameters = inputParameters;

            return this.ExecutePluginWith<T>(ctx);
        }

        public IPlugin ExecutePluginWithTargetAndPreEntityImages<T>(object target, EntityImageCollection preEntityImages) where T : IPlugin, new()
        {
            //Add the target entity to the InputParameters
            ParameterCollection inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);

            XrmFakedPluginExecutionContext ctx = GetDefaultPluginContext();
            ctx.InputParameters = inputParameters;
            ctx.PreEntityImages = preEntityImages;

            return this.ExecutePluginWith<T>(ctx);
        }

        protected IServiceProvider GetFakedServiceProvider(XrmFakedPluginExecutionContext plugCtx)
        {
            var fakedServiceProvider = A.Fake<IServiceProvider>();

            A.CallTo(() => fakedServiceProvider.GetService(A<Type>._))
               .ReturnsLazily((Type t) =>
               {
                   if (t.Equals(typeof(IOrganizationService)))
                   {
                       //Return faked or real organization service
                       return GetOrganizationService();
                   }
                   else if (t.Equals(typeof(ITracingService)))
                   {
                       return TracingService;
                   }
                   else if (t.Equals(typeof(IPluginExecutionContext)))
                   {
                       return GetFakedPluginContext(plugCtx);
                   }
                   else if (t.Equals(typeof(IExecutionContext)))
                   {
                       return GetFakedExecutionContext(plugCtx);
                   }
                   else if (t.Equals(typeof(IOrganizationServiceFactory)))
                   {
                       var fakedServiceFactory = A.Fake<IOrganizationServiceFactory>();
                       A.CallTo(() => fakedServiceFactory.CreateOrganizationService(A<Guid?>._))
                            .ReturnsLazily((Guid? g) =>
                            {
                                return GetOrganizationService();
                            });
                       return fakedServiceFactory;
                   }
                   else if (t.Equals(typeof(IServiceEndpointNotificationService)))
                   {
                       var fakedNotificationService = A.Fake<IServiceEndpointNotificationService>();
                       return fakedNotificationService;
                   }
                   throw new PullRequestException("The specified service type is not supported");
               });

            return fakedServiceProvider;
        }

        public XrmFakedTracingService GetFakeTracingService()
        {
            return TracingService;
        }

    }
}
