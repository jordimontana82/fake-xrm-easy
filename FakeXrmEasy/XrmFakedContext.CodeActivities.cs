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
    public partial class XrmFakedContext : IXrmFakedContext
    {
        /// <summary>
        /// Executes a code activity against this context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IDictionary<string, object> ExecuteCodeActivity<T>(Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new()
        {
            return this.ExecuteCodeActivity<T>(null, inputs, instance);
        }


        /// <summary>
        /// Executes a code activity passing the primary entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IDictionary<string, object> ExecuteCodeActivity<T>(Entity primaryEntity, Dictionary<string, object> inputs, T instance = null) where T : CodeActivity, new()
        {
            WorkflowInvoker invoker = null;
            WorkflowInstanceExtensionManager mngr = null;
            string sDebug = "";
            try
            {
                sDebug = "Creating instance..." + System.Environment.NewLine;
                if (instance == null) 
                    instance = new T();
                invoker = new WorkflowInvoker(instance);
                sDebug += "Invoker created" + System.Environment.NewLine;
                sDebug += "Adding extensions..." + System.Environment.NewLine;
                invoker.Extensions.Add<ITracingService>(() => new XrmFakedTracingService());
                invoker.Extensions.Add<IWorkflowContext>(() =>
                {
                    var fakedWorkflowContext = A.Fake<IWorkflowContext>();
                    if (primaryEntity != null)
                    {
                        A.CallTo(() => fakedWorkflowContext.PrimaryEntityId).ReturnsLazily(() => primaryEntity.Id);
                        A.CallTo(() => fakedWorkflowContext.PrimaryEntityName).ReturnsLazily(() => primaryEntity.LogicalName);
                    }

                    return fakedWorkflowContext;
                });
                invoker.Extensions.Add<IOrganizationServiceFactory>(() => {
                    var fakedServiceFactory = A.Fake<IOrganizationServiceFactory>();
                    A.CallTo(() => fakedServiceFactory.CreateOrganizationService(A<Guid?>._))
                         .ReturnsLazily((Guid? g) =>
                         {
                             return GetFakedOrganizationService();
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
                //if(string.IsNullOrWhiteSpace(typeName) ||
                //    typeName.Equals("System.Activities.WorkflowApplication"))
                //{
                //    //Try again
                //    System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "System.Activities.dll"));
                //    System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Microsoft.Xrm.Sdk.dll"));
                //    System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Microsoft.Xrm.Sdk.Workflow.dll"));
                //    try
                //    {
                //        return invoker.Invoke(inputs);
                //    }
                //    catch (TypeLoadException tlex2)
                //    {
                //        if (tlex2.InnerException != null)
                //            throw new Exception("TypeLoadException with innerexception " + tlex2.InnerException.ToString());
                //        else
                //            throw new Exception("TypeLoadException with innerexception null");
                //    }
                    
                //}
                //else
                    throw new TypeLoadException("When loading type: " + typeName + "." + tlex.Message + "in domain directory: " + AppDomain.CurrentDomain.BaseDirectory + "Debug=" + sDebug);
            }
        }

    }
}