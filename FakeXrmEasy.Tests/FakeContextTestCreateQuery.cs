using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestCreateQuery
    {
        public class Contact : Entity
        {
            public Contact() {
                LogicalName = "contact";
            }
            public Guid? contactid
            {
                get { return Id; }
            }
        }

        [Fact]
        public void After_adding_a_contact_the_create_query_returns_it()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Contact() { Id = guid }
            }.AsQueryable();

            context.Initialize(data);
            
            //Find the contact
            var contact = (from c in context.CreateQuery<Contact>()
                           where c.contactid == guid
                           select c).FirstOrDefault();

            
            Assert.False(contact == null);
            Assert.Equal(guid, contact.Id);
        }

        
        
    }
}
