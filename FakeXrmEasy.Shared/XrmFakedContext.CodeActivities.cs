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
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities.Hosting;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmContext
    {
        public XrmFakedWorkflowContext GetDefaultWorkflowContext()
        {
            var userId = CallerId != null ? CallerId.Id : Guid.NewGuid();
            return new XrmFakedWorkflowContext()
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

        /// <summary>
        /// Executes a code activity against this context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IDictionary<string, object> ExecuteCodeActivity<T>(Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new()
        {
            var wfContext = GetDefaultWorkflowContext();
            return this.ExecuteCodeActivity<T>(wfContext, inputs, instance);
        }


        public IDictionary<string, object> ExecuteCodeActivity<T>(Entity primaryEntity, Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new()
        {
            var wfContext = GetDefaultWorkflowContext();
            wfContext.PrimaryEntityId = primaryEntity.Id;
            wfContext.PrimaryEntityName = primaryEntity.LogicalName;

            return this.ExecuteCodeActivity<T>(wfContext, inputs, instance);
        }

        /// <summary>
        /// Executes a code activity passing the primary entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IDictionary<string, object> ExecuteCodeActivity<T>(XrmFakedWorkflowContext wfContext, Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new()
        {
            WorkflowInvoker invoker = null;
            string sDebug = "";
            try
            {
                sDebug = "Creating instance..." + System.Environment.NewLine;
                if (instance == null) 
                    instance = new T();
                invoker = new WorkflowInvoker(instance);
                sDebug += "Invoker created" + System.Environment.NewLine;
                sDebug += "Adding extensions..." + System.Environment.NewLine;
                invoker.Extensions.Add<ITracingService>(() =>
                {
                    _tracingService = new XrmFakedTracingService();
                    return _tracingService;
                });
                invoker.Extensions.Add<IWorkflowContext>(() =>
                {
                    return wfContext;
                });
                invoker.Extensions.Add<IOrganizationServiceFactory>(() => {
                    var fakedServiceFactory = A.Fake<IOrganizationServiceFactory>();
                    A.CallTo(() => fakedServiceFactory.CreateOrganizationService(A<Guid?>._))
                         .ReturnsLazily((Guid? g) =>
                         {
                             return GetOrganizationService();
                         });
                    return fakedServiceFactory;
                });

                sDebug += "Adding extensions...ok." + System.Environment.NewLine;
                sDebug += "Invoking activity..." + System.Environment.NewLine;
                return invoker.Invoke(inputs);
            }
            catch (TypeLoadException tlex)
            {
                var typeName = tlex.TypeName != null ? tlex.TypeName : "(null)";
                throw new TypeLoadException("When loading type: " + typeName + "." + tlex.Message + "in domain directory: " + AppDomain.CurrentDomain.BaseDirectory + "Debug=" + sDebug);
            }
        }

    }
}