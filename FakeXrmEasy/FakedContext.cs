using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy
{
    /// <summary>
    /// A fake context that stores In-Memory entites indexed by logical name and then Entity records, simulating
    /// how entities are persisted in Tables (with the logical name) and then the records themselves
    /// where the Primary Key is the Guid
    /// </summary>
    public class XrmFakedContext: IFakedContextFactory
    {
        public Dictionary<string, Dictionary<Guid, Entity>> Data { get; set; }
        public XrmFakedContext()
        {
            Data = new Dictionary<string, Dictionary<Guid, Entity>>();
        }

        public void Build(XrmFakedContext data)
        {
            this.Data = data.Data;
        }

        /// <summary>
        /// Initializes the context with the provided entities
        /// </summary>
        /// <param name="col"></param>
        public void Initialize(IEnumerable<Entity> entities)
        {
            if (entities == null)
            {
                throw new InvalidOperationException("The entities parameter must be not null");
            }

            foreach (var e in entities)
            {
                //Validate the entity
                if (string.IsNullOrWhiteSpace(e.LogicalName))
                {
                    throw new InvalidOperationException("An entity must not have a null or empty LogicalName property.");
                }

                if (e.Id == Guid.Empty)
                {
                    throw new InvalidOperationException("An entity with an empty Id can't be added");
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
            }
        }

        /// <summary>
        /// Returns a faked organization service that works against this context
        /// </summary>
        /// <returns></returns>
        public IOrganizationService GetFakedOrganizationService()
        {
            return GetFakedOrganizationService(this);
        }

        /// <summary>
        /// Defines a faked organization service that intercepts CRUD operations to make them work against
        /// the faked context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IOrganizationService GetFakedOrganizationService(XrmFakedContext context)
        {
            if (context == null)
            {
                throw new InvalidOperationException("The faked context must not be null.");
            }

            var fakedService = A.Fake<IOrganizationService>();

            //Fake Retrieve method
            FakeRetrieve(context, fakedService);
            return fakedService;
        }

        /// <summary>
        /// A fake retrieve method that will query the FakedContext to retrieve the specified
        /// entity and Guid, or null, if the entity was not found
        /// </summary>
        /// <param name="context">The faked context</param>
        /// <param name="fakedService">The faked service where the Retrieve method will be faked</param>
        /// <returns></returns>
        public static void FakeRetrieve(XrmFakedContext context, IOrganizationService fakedService)
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

                    if (!context.Data.ContainsKey(entityName))
                        throw new InvalidOperationException(string.Format("The entity logical name {0} is not valid.", entityName));

                    //Entity logical name exists, so , check if the requested entity exists
                    if(context.Data[entityName] != null
                        && context.Data[entityName].ContainsKey(id))
                    {
                        //Entity found => return only the subset of columns specified or all of them
                        if(columnSet.AllColumns)
                            return context.Data[entityName][id];
                        else
                        {
                            //Return the subset of columns requested only
                            var newEntity = new Entity(entityName);
                            newEntity.Id = id;

                            //Add attributes
                            var foundEntity = context.Data[entityName][id];
                            foreach (var column in columnSet.Columns)
                            {
                                newEntity[column] = foundEntity[column];
                            }
                            return newEntity;
                        }
                    }
                    else
                    {
                        //Entity not found in the context => return null
                        return null;
                    }
                });
        }
    }
}
