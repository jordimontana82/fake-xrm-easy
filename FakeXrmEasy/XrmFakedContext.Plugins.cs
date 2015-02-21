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

        public IPlugin GetFakedPlugin<T>() where T : IPlugin
        {
            var fakedPlugin = A.Fake<T>();
            return fakedPlugin;
        }

        public void ExecutePlugin(IPlugin fakedPlugin)
        {
            var fakedServiceProvider = GetFakedServiceProvider();
            fakedPlugin.Execute(fakedServiceProvider);
        }

        protected IServiceProvider GetFakedServiceProvider()
        {

            var fakedServiceProvider = A.Fake<IServiceProvider>();

            A.CallTo(() => fakedServiceProvider.GetService(A<Type>._))
               .ReturnsLazily((Type t) =>
               {
                   if (t is IOrganizationService)
                       return GetFakedOrganizationService();

                   throw new PullRequestException("The specified service type is not supported");
               });

            return fakedServiceProvider;
        }
    }
}