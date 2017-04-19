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
        /// A fake retrieve method that will query the FakedContext to retrieve the specified
        /// entity and Guid, or null, if the entity was not found
        /// </summary>
        /// <param name="context">The faked context</param>
        /// <param name="fakedService">The faked service where the Retrieve method will be faked</param>
        /// <returns></returns>
        protected static void FakeRetrieveIntegration(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Retrieve(A<string>._, A<Guid>._, A<ColumnSet>._))
                .ReturnsLazily((string entityName, Guid id, ColumnSet columnSet) =>
                {
                    return context._integrationService.Retrieve(entityName, id, columnSet);
                });
        }
        /// <summary>
        /// Fakes the Create message
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        protected static void FakeCreateIntegration(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Create(A<Entity>._))
                .ReturnsLazily((Entity e) =>
                {
                    return context._integrationService.Create(e);
                });
        }

        protected static void FakeUpdateIntegration(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Update(A<Entity>._))
                .Invokes((Entity e) =>
                {
                    context._integrationService.Update(e);
                });

        }

        /// <summary>
        /// Fakes the delete method. Very similar to the Retrieve one
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        protected static void FakeDeleteIntegration(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Delete(A<string>._, A<Guid>._))
                .Invokes((string entityName, Guid id) =>
                {
                    context._integrationService.Delete(entityName, id);
                });
        }
    }
}