using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using System.Configuration;

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
            
            return null; // TO DO
#else
            CrmConnection crmConnection = CrmConnection.Parse(connection.ConnectionString);
            OrganizationService service = new OrganizationService(crmConnection);
            return service;
#endif

        }
    }
}
