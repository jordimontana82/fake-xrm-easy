using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue156
    {
        [Fact]
        public void When_I_run_connection_fetchXml_it_should_return_all_matching_record1id()
        {
            // Arrange
            // Create Contacts
            var contact = new Entity("contact");
            contact.Id = Guid.NewGuid();
            contact.Attributes.Add("firstname", "First");
            contact.Attributes.Add("lastname", "User");
            contact.Attributes.Add("statecode", new OptionSetValue(0));

            // Create Other User
            var otherContact = new Entity("contact");
            otherContact.Id = Guid.NewGuid();
            otherContact.Attributes.Add("firstname", "Other");
            otherContact.Attributes.Add("lastname", "User");
            otherContact.Attributes.Add("statecode", new OptionSetValue(0));

            // Connection Role
            var conRole = new Entity("connectionrole");
            conRole.Id = Guid.NewGuid();
            conRole.Attributes.Add("name", "Contact");
            conRole.Attributes.Add("statecode", new OptionSetValue(0));

            // Create connection with disclosure AND CONTACT
            var conn = new Entity("connection");
            conn.Id = Guid.NewGuid();
            conn.Attributes.Add("record1id", contact.ToEntityReference()); // discloser
            conn.Attributes.Add("record2id", otherContact.ToEntityReference()); // contact
            conn.Attributes.Add("record2roleid", conRole.ToEntityReference()); // Connection Role
            conn.Attributes.Add("statecode", new OptionSetValue(0));

            // Create Faked Context
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Connection));
            //ctx.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            ctx.Initialize(new List<Entity>() { contact, otherContact, conRole, conn });

            // Fetch Xml
            string fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                                      <entity name='connection'>
                                                        <attribute name='record2id' />
                                                        <attribute name='record2roleid' />
                                                        <attribute name='connectionid' />
                                                        <attribute name='record1id' />
                                                        <filter type='and'>
                                                          <condition attribute='record1id' operator='eq' uitype='contact' value= '{0}' />
                                                          <condition attribute='statecode' operator='eq' value='0' />
                                                        </filter>
                                                        <link-entity name='contact' from='contactid' to='record2id' alias='ah'>
                                                          <filter type='and'>
                                                            <condition attribute='lastname' operator='not-null' />
                                                            <condition attribute='statecode' operator='eq' value='0' />
                                                          </filter>
                                                        </link-entity>
                                                      </entity>
                                                    </fetch>";
            fetchXml = string.Format(fetchXml, contact.Id);

            // Act
            EntityCollection getConnectionListResults = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            // Assert
            Assert.True(getConnectionListResults.Entities.Count > 0);
        }
    }
}