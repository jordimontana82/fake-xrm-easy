using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmContext
    {
        public bool UsePipelineSimulation { get; set; }

        /// <summary>
        /// Registers the <typeparamref name="TPlugin"/> as a SDK Message Processing Step for the Entity <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TPlugin">The plugin to register the step for.</typeparam>
        /// <typeparam name="TEntity">The entity to filter this step for.</typeparam>
        /// <param name="message">The message that should trigger the execution of plugin.</param>
        /// <param name="stage">The stage when the plugin should be executed.</param>
        /// <param name="mode">The mode in which the plugin should be executed.</param>
        /// <param name="rank">The order in which this plugin should be executed in comparison to other plugins registered with the same <paramref name="message"/> and <paramref name="stage"/>.</param>
        /// <param name="filteringAttributes">When not one of these attributes is present in the execution context, the execution of the plugin is prevented.</param>
        public void RegisterPluginStep<TPlugin, TEntity>(string message, ProcessingStepStage stage = ProcessingStepStage.Postoperation, ProcessingStepMode mode = ProcessingStepMode.Synchronous, int rank = 1, string[] filteringAttributes = null)
            where TPlugin : IPlugin
            where TEntity : Entity, new()
        {
            var entity = new TEntity();
            var entityTypeCode = (int)entity.GetType().GetField("EntityTypeCode").GetValue(entity);

            RegisterPluginStep<TPlugin>(message, stage, mode, rank, filteringAttributes, entityTypeCode);
        }

        /// <summary>
        /// Registers the <typeparamref name="TPlugin"/> as a SDK Message Processing Step.
        /// </summary>
        /// <typeparam name="TPlugin">The plugin to register the step for.</typeparam>
        /// <param name="message">The message that should trigger the execution of plugin.</param>
        /// <param name="stage">The stage when the plugin should be executed.</param>
        /// <param name="mode">The mode in which the plugin should be executed.</param>
        /// <param name="rank">The order in which this plugin should be executed in comparison to other plugins registered with the same <paramref name="message"/> and <paramref name="stage"/>.</param>
        /// <param name="filteringAttributes">When not one of these attributes is present in the execution context, the execution of the plugin is prevented.</param>
        /// <param name="primaryEntityTypeCode">The entity type code to filter this step for.</param>
        public void RegisterPluginStep<TPlugin>(string message, ProcessingStepStage stage = ProcessingStepStage.Postoperation, ProcessingStepMode mode = ProcessingStepMode.Synchronous, int rank = 1, string[] filteringAttributes = null, int? primaryEntityTypeCode = null)
            where TPlugin : IPlugin
        {
            // Message
            var sdkMessage = this.CreateQuery("sdkmessage").FirstOrDefault(sm => string.Equals(sm.GetAttributeValue<string>("name"), message));
            if (sdkMessage == null)
            {
                sdkMessage = new Entity("sdkmessage")
                {
                    Id = Guid.NewGuid(),
                    ["name"] = message
                };
                this.AddEntityWithDefaults(sdkMessage);
            }

            // Plugin Type
            var type = typeof(TPlugin);
            var assemblyName = type.Assembly.GetName();

            var pluginType = this.CreateQuery("plugintype").FirstOrDefault(pt => string.Equals(pt.GetAttributeValue<string>("typename"), type.FullName) && string.Equals(pt.GetAttributeValue<string>("asemblyname"), assemblyName.Name));
            if (pluginType == null)
            {
                pluginType = new Entity("plugintype")
                {
                    Id = Guid.NewGuid(),
                    ["name"] = type.FullName,
                    ["typename"] = type.FullName,
                    ["assemblyname"] = assemblyName.Name,
                    ["major"] = assemblyName.Version.Major,
                    ["minor"] = assemblyName.Version.Minor,
                    ["version"] = assemblyName.Version.ToString(),
                };
                this.AddEntityWithDefaults(pluginType);
            }

            // Filter
            Entity sdkFilter = null;
            if (primaryEntityTypeCode.HasValue)
            {
                sdkFilter = new Entity("sdkmessagefilter")
                {
                    Id = Guid.NewGuid(),
                    ["primaryobjecttypecode"] = primaryEntityTypeCode
                };
                this.AddEntityWithDefaults(sdkFilter);
            }

            // Message Step
            var sdkMessageProcessingStep = new Entity("sdkmessageprocessingstep")
            {
                Id = Guid.NewGuid(),
                ["eventhandler"] = pluginType.ToEntityReference(),
                ["sdkmessageid"] = sdkMessage.ToEntityReference(),
                ["sdkmessagefilterid"] = sdkFilter?.ToEntityReference(),
                ["filteringattributes"] = filteringAttributes != null ? string.Join(",", filteringAttributes) : null,
                ["mode"] = new OptionSetValue((int)mode),
                ["stage"] = new OptionSetValue((int)stage),
                ["rank"] = rank
            };
            this.AddEntityWithDefaults(sdkMessageProcessingStep);
        }

        private void ExecutePipelineStage(string method, ProcessingStepStage stage, ProcessingStepMode mode, Entity entity)
        {
            var plugins = GetStepsForStage(method, stage, mode, entity);

            ExecutePipelinePlugins(plugins, entity);
        }

        private void ExecutePipelineStage(string method, ProcessingStepStage stage, ProcessingStepMode mode, EntityReference entityReference)
        {
            var entityType = FindReflectedType(entityReference.LogicalName);
            if (entityType == null)
            {
                return;
            }

            var plugins = GetStepsForStage(method, stage, mode, (Entity)Activator.CreateInstance(entityType));

            ExecutePipelinePlugins(plugins, entityReference);
        }

        private void ExecutePipelinePlugins(IEnumerable<Entity> plugins, object target)
        {
            foreach (var plugin in plugins)
            {
                var pluginMethod = GetPluginMethod(plugin);

                var pluginContext = this.GetDefaultPluginContext();
                pluginContext.Mode = plugin.GetAttributeValue<OptionSetValue>("mode").Value;
                pluginContext.Stage = plugin.GetAttributeValue<OptionSetValue>("stage").Value;
                pluginContext.MessageName = (string)plugin.GetAttributeValue<AliasedValue>("sdkmessage.name").Value;
                pluginContext.InputParameters = new ParameterCollection
                {
                    { "Target", target }
                };
                pluginContext.OutputParameters = new ParameterCollection();
                pluginContext.PreEntityImages = new EntityImageCollection();
                pluginContext.PostEntityImages = new EntityImageCollection();

                pluginMethod.Invoke(this, new object[] { pluginContext });
            }
        }

        private static MethodInfo GetPluginMethod(Entity pluginEntity)
        {
            var assemblyName = (string)pluginEntity.GetAttributeValue<AliasedValue>("plugintype.assemblyname").Value;
            var assembly = AppDomain.CurrentDomain.Load(assemblyName);

            var pluginTypeName = (string)pluginEntity.GetAttributeValue<AliasedValue>("plugintype.typename").Value;
            var pluginType = assembly.GetType(pluginTypeName);

            var methodInfo = typeof(XrmFakedContext).GetMethod("ExecutePluginWith", new[] { typeof(XrmFakedPluginExecutionContext) });
            var pluginMethod = methodInfo.MakeGenericMethod(pluginType);

            return pluginMethod;
        }

        private IEnumerable<Entity> GetStepsForStage(string method, ProcessingStepStage stage, ProcessingStepMode mode, Entity entity)
        {
            var query = new QueryExpression("sdkmessageprocessingstep")
            {
                ColumnSet = new ColumnSet("configuration", "filteringattributes", "stage", "mode"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("stage", ConditionOperator.Equal, (int)stage),
                        new ConditionExpression("mode", ConditionOperator.Equal, (int)mode)
                    }
                },
                Orders =
                {
                    new OrderExpression("rank", OrderType.Ascending)
                },
                LinkEntities =
                {
                    new LinkEntity("sdkmessageprocessingstep", "sdkmessagefilter", "sdkmessagefilterid", "sdkmessagefilterid", JoinOperator.LeftOuter)
                    {
                        EntityAlias = "sdkmessagefilter",
                        Columns = new ColumnSet("primaryobjecttypecode")
                    },
                    new LinkEntity("sdkmessageprocessingstep", "sdkmessage", "sdkmessageid", "sdkmessageid", JoinOperator.Inner)
                    {
                        EntityAlias = "sdkmessage",
                        Columns = new ColumnSet("name"),
                        LinkCriteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("name", ConditionOperator.Equal, method)
                            }
                        }
                    },
                    new LinkEntity("sdkmessageprocessingstep", "plugintype", "eventhandler", "plugintypeid", JoinOperator.Inner)
                    {
                        EntityAlias = "plugintype",
                        Columns = new ColumnSet("assemblyname", "typename")
                    }
                }
            };

            var entityTypeCode = (int?)entity.GetType().GetField("EntityTypeCode")?.GetValue(entity);

            var plugins = this.Service.RetrieveMultiple(query).Entities.AsEnumerable();
            plugins = plugins.Where(p =>
            {
                var primaryObjectTypeCode = p.GetAttributeValue<AliasedValue>("sdkmessagefilter.primaryobjecttypecode");

                return primaryObjectTypeCode == null || entityTypeCode.HasValue && (int)primaryObjectTypeCode.Value == entityTypeCode.Value;
            });

            // Todo: Filter on attributes

            return plugins;
        }
    }
}