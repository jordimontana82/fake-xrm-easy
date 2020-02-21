using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeXrmEasy.Extensions;
using FakeXrmEasy.Models;
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
        public void RegisterPluginStep<TPlugin, TEntity>(string message, ProcessingStepStage stage = ProcessingStepStage.Postoperation, ProcessingStepMode mode = ProcessingStepMode.Synchronous, int rank = 1, string[] filteringAttributes = null, IEnumerable<PluginImageDefinition> registeredImages = null)
            where TPlugin : IPlugin
            where TEntity : Entity, new()
        {
            int entityTypeCode = typeof(TEntity).GetEntityTypeCode();
            
            RegisterPluginStep<TPlugin>(message, stage, mode, rank, filteringAttributes, entityTypeCode, registeredImages);
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
        public void RegisterPluginStep<TPlugin>(string message, ProcessingStepStage stage = ProcessingStepStage.Postoperation, ProcessingStepMode mode = ProcessingStepMode.Synchronous, int rank = 1, string[] filteringAttributes = null, int? primaryEntityTypeCode = null, IEnumerable<PluginImageDefinition> registeredImages = null)
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

            // Message Step Image(s)
            if (registeredImages != null)
            {
                foreach (var pluginImage in registeredImages)
                {
                    var sdkMessageProcessingStepImage = new Entity("sdkmessageprocessingstepimage")
                    {
                        Id = Guid.NewGuid(),
                        ["name"] = pluginImage.Name,
                        ["sdkmessageprocessingstepid"] = sdkMessageProcessingStep.ToEntityReference(),
                        ["imagetype"] = new OptionSetValue((int)pluginImage.ImageType),
                        ["attributes"] = pluginImage.Attributes != null ? string.Join(",", pluginImage.Attributes) : null,
                    };
                    this.AddEntityWithDefaults(sdkMessageProcessingStepImage);
                }
            }
        }

        private void ExecutePipelineStage(string method, ProcessingStepStage stage, ProcessingStepMode mode, Entity entity, Entity previousValues = null, Entity resultingAttributes = null)
        {
            var plugins = GetStepsForStage(method, stage, mode, entity);

            ExecutePipelinePlugins(plugins, entity, previousValues, resultingAttributes);
        }

        private void ExecutePipelineStage(string method, ProcessingStepStage stage, ProcessingStepMode mode, EntityReference entityReference, Entity previousValues, Entity resultingAttributes = null)
        {
            var entityType = FindReflectedType(entityReference.LogicalName);
            if (entityType == null)
            {
                return;
            }

            var plugins = GetStepsForStage(method, stage, mode, (Entity)Activator.CreateInstance(entityType));

            ExecutePipelinePlugins(plugins, entityReference, previousValues, resultingAttributes);
        }

        private void ExecutePipelinePlugins(IEnumerable<Entity> plugins, object target, Entity previousValues, Entity resultingAttributes)
        {
            foreach (var plugin in plugins)
            {
                var pluginMethod = GetPluginMethod(plugin);

                IEnumerable<Entity> preImageDefinitions = null;
                if (previousValues != null)
                {
                    preImageDefinitions = GetImageDefintions(plugin.Id, ProcessingStepImageType.PreImage);
                }

                IEnumerable<Entity> postImageDefinitions = null;
                if (resultingAttributes != null)
                {
                    postImageDefinitions = GetImageDefintions(plugin.Id, ProcessingStepImageType.PostImage);
                }

                var pluginContext = this.GetDefaultPluginContext();
                pluginContext.Mode = plugin.GetAttributeValue<OptionSetValue>("mode").Value;
                pluginContext.Stage = plugin.GetAttributeValue<OptionSetValue>("stage").Value;
                pluginContext.MessageName = (string)plugin.GetAttributeValue<AliasedValue>("sdkmessage.name").Value;
                pluginContext.InputParameters = new ParameterCollection
                {
                    { "Target", target }
                };
                pluginContext.OutputParameters = new ParameterCollection();
                pluginContext.PreEntityImages = GetEntityImageCollection(preImageDefinitions, previousValues);
                pluginContext.PostEntityImages = GetEntityImageCollection(postImageDefinitions, resultingAttributes);

                pluginMethod.Invoke(this, new object[] { pluginContext });
            }
        }

        private EntityImageCollection GetEntityImageCollection(IEnumerable<Entity> imageDefinitions, Entity values)
        {
            EntityImageCollection collection = new EntityImageCollection();

            if (values != null && imageDefinitions != null)
            {
                foreach (Entity imageDefinition in imageDefinitions)
                {
                    string name = imageDefinition.GetAttributeValue<string>("name");
                    if (string.IsNullOrEmpty(name))
                    {
                        name = string.Empty;
                    }

                    string attributes = imageDefinition.GetAttributeValue<string>("attributes");

                    Entity preImage = values.Clone(values.GetType());
                    if (!string.IsNullOrEmpty(attributes))
                    {
                        string[] specifiedAttributes = attributes.Split(',');

                        foreach (KeyValuePair<string, object> attr in values.Attributes.Where(x => !specifiedAttributes.Contains(x.Key)))
                        {
                            preImage.Attributes.Remove(attr.Key);
                        }
                    }

                    collection.Add(name, preImage);
                }
            }

            return collection;
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

            int? entityTypeCode = null;
            entity.TryGetEntityTypeCode(out entityTypeCode);

            var plugins = this.Service.RetrieveMultiple(query).Entities.AsEnumerable();
            plugins = plugins.Where(p =>
            {
                var primaryObjectTypeCode = p.GetAttributeValue<AliasedValue>("sdkmessagefilter.primaryobjecttypecode");

                return primaryObjectTypeCode == null || entityTypeCode.HasValue && (int)primaryObjectTypeCode.Value == entityTypeCode.Value;
            });

            // Todo: Filter on attributes

            return plugins;
        }

        private IEnumerable<Entity> GetImageDefintions(Guid stepId, ProcessingStepImageType imageType)
        {
            var query = new QueryExpression("sdkmessageprocessingstepimage")
            {
                ColumnSet = new ColumnSet("name", "imagetype", "attributes"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("sdkmessageprocessingstepid", ConditionOperator.Equal, stepId)
                    }
                },
                Orders =
                {
                    new OrderExpression("rank", OrderType.Ascending)
                }
            };

            FilterExpression filter = new FilterExpression(LogicalOperator.Or)
            {
                Conditions = { new ConditionExpression("imagetype", ConditionOperator.Equal, (int)ProcessingStepImageType.Both) }
            };

            if (imageType == ProcessingStepImageType.PreImage || imageType == ProcessingStepImageType.PostImage)
            {
                filter.AddCondition(new ConditionExpression("imagetype", ConditionOperator.Equal, (int)imageType));
            }

            query.Criteria.AddFilter(filter);

            return this.Service.RetrieveMultiple(query).Entities.AsEnumerable();
        }

    }
}