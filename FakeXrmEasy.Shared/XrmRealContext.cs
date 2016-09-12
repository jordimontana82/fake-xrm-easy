using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using System.Configuration;

#if FAKE_XRM_EASY_2016
using Microsoft.Xrm.Tooling.Connector;
#else 
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
#endif


namespace FakeXrmEasy
{
    /// <summary>
    /// Reuse unit test syntax to test against a real CRM organisation
    /// It uses a real CRM organisation service instance
    /// </summary>
    public class XrmRealContext: XrmFakedContext, IXrmContext
    {
        public XrmRealContext()
        {
            //Don't setup fakes in this case.
        }

        public override IOrganizationService GetOrganizationService()
        {
            if (_service != null)
                return _service;

            _service = GetOrgService();
            return _service;
        }

        public override void Initialize(IEnumerable<Entity> entities)
        {
            //Does nothing...  otherwise it would create records in a real org db
        }

        protected IOrganizationService GetOrgService()
        {
            var connection = ConfigurationManager.ConnectionStrings["fakexrmeasy-connection"];
            if (connection == null)
                throw new Exception("A connectionstring parameter with name 'fakexrmeasy-connection' must exist");

            if(string.IsNullOrWhiteSpace(connection.ConnectionString))
            {
                throw new Exception("The connectionString property must not be blank");
            }

#if FAKE_XRM_EASY_2016
            
            // Connect to the CRM web service using a connection string.
            CrmServiceClient client = new Microsoft.Xrm.Tooling.Connector.CrmServiceClient(connection.ConnectionString);
            return client;

#else
            CrmConnection crmConnection = CrmConnection.Parse(connection.ConnectionString);
            OrganizationService service = new OrganizationService(crmConnection);
            return service;
#endif

        }
    }
}
