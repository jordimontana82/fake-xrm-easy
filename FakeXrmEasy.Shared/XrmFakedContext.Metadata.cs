using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext
    {
        /// <summary>
        /// Fakes the RetrieveAttributeRequest that checks if an attribute exists for a given entity
        /// For simpicity, it asumes all attributes exist
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        protected static OrganizationResponse FakeRetrieveAttributeRequest(XrmFakedContext context, IOrganizationService fakedService, RetrieveAttributeRequest req)
        {
            var response = new RetrieveAttributeResponse
            {


            };
            return response;
        }
    }
}
