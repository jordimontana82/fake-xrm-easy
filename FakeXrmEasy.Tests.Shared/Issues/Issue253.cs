using System;
using System.Collections.Generic;
using System.Text;

using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Reflection;
using Xunit;


namespace FakeXrmEasy.Tests.Issues
{
    public class Issue253
    {
        [Fact]
        public void Test_Fetch_Less_Than_Operator_With_String_Late_Bound()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            Entity college = new Entity()
            {
                Id = Guid.NewGuid(),
                LogicalName = "bsp_college",
                Attributes = { { "bsp_name", "Brasenose" } }
            };

            fakedContext.Initialize(new List<Entity>() { college });

            string FetchXml = @"<fetch mapping='logical'> 
                    <entity name='bsp_college'>
	                    <attribute name='bsp_name'/>
	                    <filter type='and'>
		                    <condition attribute='bsp_name' operator='lt' value='C' />
	                    </filter>
                    </entity>
                    </fetch>";

            EntityCollection ec = fakedService.RetrieveMultiple(new FetchExpression(FetchXml));

            Assert.Equal(ec.Entities.Count, 1);
        }

        [Fact]
        public void Test_Fetch_Less_Than_Operator_With_String_Early_Bound()
        {
            var fakedContext = new XrmFakedContext() { ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account)) };
            var fakedService = fakedContext.GetOrganizationService();

            Entity account = new Account() {
                Id = Guid.NewGuid(),
                Name = "Bob"
            };

            fakedContext.Initialize(new List<Entity>() { account });

            string FetchXml = @"<fetch mapping='logical'> 
                        <entity name='account'>
	                        <attribute name='name'/>
	                        <filter type='and'>
		                        <condition attribute='name' operator='lt' value='C' />
	                        </filter>
                        </entity>
                        </fetch>";

            EntityCollection ec = fakedService.RetrieveMultiple(new FetchExpression(FetchXml));

            Assert.Equal(ec.Entities.Count, 1);
        }
    }
}
