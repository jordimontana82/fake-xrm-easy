using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext
    {
        /// <summary>
        /// Stores some minimal metadata info if dynamic entities are used and no injected metadata was used
        /// </summary>
        protected Dictionary<string, Dictionary<string, string>> AttributeMetadataNames { get; set; }

        /// <summary>
        /// Stores fake global option set metadata
        /// </summary>
        public Dictionary<string, OptionSetMetadata> OptionSetValuesMetadata { get; set; }

        /// <summary>
        /// Stores fake entity metadata
        /// </summary>
        protected Dictionary<string, EntityMetadata> EntityMetadata { get; set; }

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
