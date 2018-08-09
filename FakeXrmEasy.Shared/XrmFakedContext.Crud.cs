using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Messages;
using FakeXrmEasy.Extensions;
using System.Reflection;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmContext
    {
        protected const int EntityActiveStateCode = 0;
        protected const int EntityInactiveStateCode = 1;

        #region CRUD
        /// <summary>
        /// A fake retrieve method that will query the FakedContext to retrieve the specified
        /// entity and Guid, or null, if the entity was not found
        /// </summary>
        /// <param name="context">The faked context</param>
        /// <param name="fakedService">The faked service where the Retrieve method will be faked</param>
        /// <returns></returns>
        protected static void FakeRetrieve(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Retrieve(A<string>._, A<Guid>._, A<ColumnSet>._))
                .ReturnsLazily((string entityName, Guid id, ColumnSet columnSet) =>
                {
                    if (string.IsNullOrWhiteSpace(entityName))
                    {
                        throw new InvalidOperationException("The entity logical name must not be null or empty.");
                    }

                    if (id == Guid.Empty)
                    {
                        throw new InvalidOperationException("The id must not be empty.");
                    }

                    if (columnSet == null)
                    {
                        throw new InvalidOperationException("The columnset parameter must not be null.");
                    }

                    // Don't fail with invalid operation exception, if no record of this entity exists, but entity is known
                    if (!context.Data.ContainsKey(entityName))
                    {
                        if (context.ProxyTypesAssembly == null)
                        {
                            throw new InvalidOperationException($"The entity logical name {entityName} is not valid.");
                        }

                        if (!context.ProxyTypesAssembly.GetTypes().Any(type => context.FindReflectedType(entityName) != null))
                        {
                            throw new InvalidOperationException($"The entity logical name {entityName} is not valid.");
                        }
                    }

                    //Return the subset of columns requested only
                    var reflectedType = context.FindReflectedType(entityName);
                    
                    //Entity logical name exists, so , check if the requested entity exists
                    if (context.Data.ContainsKey(entityName) && context.Data[entityName] != null
                        && context.Data[entityName].ContainsKey(id))
                    {
                        //Entity found => return only the subset of columns specified or all of them
                        var foundEntity = context.Data[entityName][id].Clone(reflectedType);
                        if (columnSet.AllColumns) { 
                            foundEntity.ApplyDateBehaviour(context);
                            return foundEntity;
                        }
                        else
                        {
                            var projected = foundEntity.ProjectAttributes(columnSet, context);
                            projected.ApplyDateBehaviour(context);
                            return projected;
                        }
                    }
                    else
                    {
                        // Entity not found in the context => FaultException
                        throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{entityName} With Id = {id:D} Does Not Exist");
                    }
                });
        }
        /// <summary>
        /// Fakes the Create message
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        protected static void FakeCreate(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Create(A<Entity>._))
                .ReturnsLazily((Entity e) =>
                {
                    return context.CreateEntity(e);
                });
        }

        protected static void FakeUpdate(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Update(A<Entity>._))
                .Invokes((Entity e) =>
                {
                    context.UpdateEntity(e);
                });
        }

        protected void UpdateEntity(Entity e)
        {
            ValidateEntity(e);

            // Update specific validations: The entity record must exist in the context
            if (Data.ContainsKey(e.LogicalName) &&
                Data[e.LogicalName].ContainsKey(e.Id))
            {
                if (this.UsePipelineSimulation)
                {
                    ExecutePipelineStage("Update", ProcessingStepStage.Preoperation, ProcessingStepMode.Synchronous, e);
                }

                // Add as many attributes to the entity as the ones received (this will keep existing ones)
                var cachedEntity = Data[e.LogicalName][e.Id];
                foreach (var sAttributeName in e.Attributes.Keys.ToList())
                {
                    var attribute = e[sAttributeName];
                    if (attribute is DateTime)
                    {
                        cachedEntity[sAttributeName] = ConvertToUtc((DateTime) e[sAttributeName]);
                    }
                    else
                    {
                        cachedEntity[sAttributeName] = attribute;
                    }
                }

                // Update ModifiedOn
                cachedEntity["modifiedon"] = DateTime.UtcNow;
                cachedEntity["modifiedby"] = CallerId;

                if (this.UsePipelineSimulation)
                {
                    ExecutePipelineStage("Update", ProcessingStepStage.Postoperation, ProcessingStepMode.Synchronous, e);

                    var clone = e.Clone(e.GetType());
                    ExecutePipelineStage("Update", ProcessingStepStage.Postoperation, ProcessingStepMode.Asynchronous, clone);
                }
            }
            else
            {
                // The entity record was not found, return a CRM-ish update error message
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{e.LogicalName} with Id {e.Id} Does Not Exist");
            }
        }

        /// <summary>
        /// Fakes the delete method. Very similar to the Retrieve one
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        protected static void FakeDelete(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Delete(A<string>._, A<Guid>._))
                .Invokes((string entityName, Guid id) =>
                {
                    if (string.IsNullOrWhiteSpace(entityName))
                    {
                        throw new InvalidOperationException("The entity logical name must not be null or empty.");
                    }

                    if (id == Guid.Empty)
                    {
                        throw new InvalidOperationException("The id must not be empty.");
                    }

                    var entityReference = new EntityReference(entityName, id);

                    context.DeleteEntity(entityReference);
                });
        }

        protected void DeleteEntity(EntityReference er)
        {
            // Don't fail with invalid operation exception, if no record of this entity exists, but entity is known
            if (!this.Data.ContainsKey(er.LogicalName))
            {
                if (this.ProxyTypesAssembly == null)
                {
                    throw new InvalidOperationException($"The entity logical name {er.LogicalName} is not valid.");
                }

                if (!this.ProxyTypesAssembly.GetTypes().Any(type => this.FindReflectedType(er.LogicalName) != null))
                {
                    throw new InvalidOperationException($"The entity logical name {er.LogicalName} is not valid.");
                }
            }

            // Entity logical name exists, so , check if the requested entity exists
            if (this.Data.ContainsKey(er.LogicalName) && this.Data[er.LogicalName] != null &&
                this.Data[er.LogicalName].ContainsKey(er.Id))
            {
                if (this.UsePipelineSimulation)
                {
                    ExecutePipelineStage("Delete", ProcessingStepStage.Preoperation, ProcessingStepMode.Synchronous, er);
                }

                // Entity found => return only the subset of columns specified or all of them
                this.Data[er.LogicalName].Remove(er.Id);

                if (this.UsePipelineSimulation)
                {
                    ExecutePipelineStage("Delete", ProcessingStepStage.Postoperation, ProcessingStepMode.Synchronous, er);
                    ExecutePipelineStage("Delete", ProcessingStepStage.Postoperation, ProcessingStepMode.Asynchronous, er);
                }
            }
            else
            {
                // Entity not found in the context => throw not found exception
                // The entity record was not found, return a CRM-ish update error message
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), $"{er.LogicalName} with Id {er.Id} Does Not Exist");
            }
        }
        #endregion

        #region Other protected methods
        protected void EnsureEntityNameExistsInMetadata(string sEntityName)
        {
            if (Relationships.Values.Any(value => new[] { value.Entity1LogicalName, value.Entity2LogicalName, value.IntersectEntity }.Contains(sEntityName, StringComparer.InvariantCultureIgnoreCase)))
            {
                return;
            }

            // Entity metadata is checked differently when we are using a ProxyTypesAssembly => we can infer that from the generated types assembly
            if (ProxyTypesAssembly != null)
            {
                var subClassType = FindReflectedType(sEntityName);
                if (subClassType == null)
                {
                    throw new Exception($"Entity {sEntityName} does not exist in the metadata cache");
                }
            }
            //else if (!Data.ContainsKey(sEntityName))
            //{
            //    //No Proxy Types Assembly
            //    throw new Exception(string.Format("Entity {0} does not exist in the metadata cache", sEntityName));
            //};
        }

        protected void AddEntityDefaultAttributes(Entity e)
        {
            // Add createdon, modifiedon, createdby, modifiedby properties
            if (CallerId == null)
            {
                CallerId = new EntityReference("systemuser", Guid.NewGuid()); // Create a new instance by default
            }

            var isManyToManyRelationshipEntity = e.LogicalName != null && this.Relationships.ContainsKey(e.LogicalName);

            EntityInitializerService.Initialize(e, CallerId.Id, this, isManyToManyRelationshipEntity);
        }

        protected void ValidateEntity(Entity e)
        {
            if (e == null)
            {
                throw new InvalidOperationException("The entity must not be null");
            }

            // Validate the entity
            if (string.IsNullOrWhiteSpace(e.LogicalName))
            {
                throw new InvalidOperationException("The LogicalName property must not be empty");
            }

            if (e.Id == Guid.Empty)
            {
                throw new InvalidOperationException("The Id property must not be empty");
            }
        }

        protected internal Guid CreateEntity(Entity e)
        {
            if (e == null)
            {
                throw new InvalidOperationException("The entity must not be null");
            }

            var clone = e.Clone(e.GetType());

            if (clone.Id == Guid.Empty)
            {
                clone.Id = Guid.NewGuid(); // Add default guid if none present
            }

            // Hack for Dynamic Entities where the Id property doesn't populate the "entitynameid" primary key
            var primaryKeyAttribute = $"{e.LogicalName}id";
            if (!clone.Attributes.ContainsKey(primaryKeyAttribute))
            {
                clone[primaryKeyAttribute] = clone.Id;
            }

            ValidateEntity(clone);

            // Create specific validations
            if (clone.Id != Guid.Empty && Data.ContainsKey(clone.LogicalName) &&
                Data[clone.LogicalName].ContainsKey(clone.Id))
            {
                throw new InvalidOperationException($"There is already a record of entity {clone.LogicalName} with id {clone.Id}, can't create with this Id.");
            }

            // Create specific validations
            if (clone.Attributes.ContainsKey("statecode"))
            {
                throw new InvalidOperationException($"When creating an entity with logical name '{clone.LogicalName}', or any other entity, it is not possible to create records with the statecode property. Statecode must be set after creation.");
            }

            AddEntityWithDefaults(clone, false, this.UsePipelineSimulation);

            if (e.RelatedEntities.Count > 0)
            {
                foreach (var relationshipSet in e.RelatedEntities)
                {
                    var relationship = relationshipSet.Key;

                    var entityReferenceCollection = new EntityReferenceCollection();

                    foreach (var relatedEntity in relationshipSet.Value.Entities)
                    {
                        var relatedId = CreateEntity(relatedEntity);
                        entityReferenceCollection.Add(new EntityReference(relatedEntity.LogicalName, relatedId));
                    }

                    if (FakeMessageExecutors.ContainsKey(typeof(AssociateRequest)))
                    {
                        var request = new AssociateRequest
                        {
                            Target = clone.ToEntityReference(),
                            Relationship = relationship,
                            RelatedEntities = entityReferenceCollection
                        };
                        FakeMessageExecutors[typeof(AssociateRequest)].Execute(request, this);
                    }
                    else
                    {
                        throw PullRequestException.NotImplementedOrganizationRequest(typeof(AssociateRequest));
                    }
                }
            }

            return clone.Id;
        }

        protected internal void AddEntityWithDefaults(Entity e, bool clone = false, bool usePluginPipeline = false)
        {
            // Create the entity with defaults
            AddEntityDefaultAttributes(e);

            if (usePluginPipeline)
            {
                ExecutePipelineStage("Create", ProcessingStepStage.Preoperation, ProcessingStepMode.Synchronous, e);               
            }

            // Store
            AddEntity(clone ? e.Clone(e.GetType()) : e);

            if (usePluginPipeline)
            {
                ExecutePipelineStage("Create", ProcessingStepStage.Postoperation, ProcessingStepMode.Synchronous, e);
                ExecutePipelineStage("Create", ProcessingStepStage.Postoperation, ProcessingStepMode.Asynchronous, e);
            }
        }

        protected internal void AddEntity(Entity e)
        {
            //Automatically detect proxy types assembly if an early bound type was used.
            if (ProxyTypesAssembly == null &&
                e.GetType().IsSubclassOf(typeof(Entity)))
            {
                ProxyTypesAssembly = Assembly.GetAssembly(e.GetType());
            }

            ValidateEntity(e); //Entity must have a logical name and an Id

            foreach (var sAttributeName in e.Attributes.Keys.ToList())
            {
                var attribute = e[sAttributeName];
                if (attribute is DateTime)
                {
                    e[sAttributeName] = ConvertToUtc((DateTime)e[sAttributeName]);
                }
            }

            //Add the entity collection
            if (!Data.ContainsKey(e.LogicalName))
            {
                Data.Add(e.LogicalName, new Dictionary<Guid, Entity>());
            }

            if (Data[e.LogicalName].ContainsKey(e.Id))
            {
                Data[e.LogicalName][e.Id] = e;
            }
            else
            {
                Data[e.LogicalName].Add(e.Id, e);
            }

            //Update metadata for that entity
            if (!AttributeMetadataNames.ContainsKey(e.LogicalName))
                AttributeMetadataNames.Add(e.LogicalName, new Dictionary<string, string>());

            //Update attribute metadata
            if (ProxyTypesAssembly != null)
            {
                //If the context is using a proxy types assembly then we can just guess the metadata from the generated attributes
                var type = FindReflectedType(e.LogicalName);
                if (type != null)
                {
                    var props = type.GetProperties();
                    foreach (var p in props)
                    {
                        if (!AttributeMetadataNames[e.LogicalName].ContainsKey(p.Name))
                            AttributeMetadataNames[e.LogicalName].Add(p.Name, p.Name);
                    }
                }
                else
                    throw new Exception(string.Format("Couldnt find reflected type for {0}", e.LogicalName));

            }
            else
            {
                //If dynamic entities are being used, then the only way of guessing if a property exists is just by checking
                //if the entity has the attribute in the dictionary
                foreach (var attKey in e.Attributes.Keys)
                {
                    if (!AttributeMetadataNames[e.LogicalName].ContainsKey(attKey))
                        AttributeMetadataNames[e.LogicalName].Add(attKey, attKey);
                }
            }

            
        }

        protected internal bool AttributeExistsInMetadata(string sEntityName, string sAttributeName)
        {
            var relationships = this.Relationships.Values.Where(value => new[] { value.Entity1LogicalName, value.Entity2LogicalName, value.IntersectEntity }.Contains(sEntityName, StringComparer.InvariantCultureIgnoreCase)).ToArray();
            if (relationships.Any(e => e.Entity1Attribute == sAttributeName || e.Entity2Attribute == sAttributeName))
            {
                return true;
            }

            //Early bound types
            if (ProxyTypesAssembly != null)
            {
                //Check if attribute exists in the early bound type 
                var earlyBoundType = FindReflectedType(sEntityName);
                if (earlyBoundType != null)
                {
                    //Get that type properties
                    var attributeFound = earlyBoundType
                        .GetProperties()
                        .Where(pi => pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true).Length > 0)
                        .Where(pi => (pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true)[0] as AttributeLogicalNameAttribute).LogicalName.Equals(sAttributeName))
                        .FirstOrDefault();

                    if (attributeFound != null)
                        return true;

                    if(attributeFound == null && EntityMetadata.ContainsKey(sEntityName))
                    {
                        //Try with metadata
                        return AttributeExistsInInjectedMetadata(sEntityName, sAttributeName);
                    }
                    else
                    {
                        return false;
                    }
                }
                //Try with metadata
                return false;
            }

            if(EntityMetadata.ContainsKey(sEntityName))
            {
                //Try with metadata
                return AttributeExistsInInjectedMetadata(sEntityName, sAttributeName);
            }

            //Dynamic entities and not entity metadata injected for entity => just return true if not found
            return true;
        }

        protected internal bool AttributeExistsInInjectedMetadata(string sEntityName, string sAttributeName)
        {
            var attributeInMetadata = FindAttributeTypeInInjectedMetadata(sEntityName, sAttributeName);
            return attributeInMetadata != null;
        }

        protected internal DateTime ConvertToUtc(DateTime attribute)
        {
            return DateTime.SpecifyKind(attribute, DateTimeKind.Utc);
        }
        #endregion
    }
}
