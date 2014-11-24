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
    public class FakeContextTestUpdate
    {
        [Fact]
        public void When_a_null_entity_is_updated_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(null));
            Assert.Equal(ex.Message, "The entity must not be null");
        }

        [Fact]
        public void When_an_entity_is_updated_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("account") { Id = Guid.Empty };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(e));
            Assert.Equal(ex.Message, "The Id property must not be empty");
        }

        [Fact]
        public void When_an_entity_is_updated_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var e = new Entity("") { Id = Guid.NewGuid() };

            var ex = Assert.Throws<InvalidOperationException>(() => service.Update(e));
            Assert.Equal(ex.Message, "The LogicalName property must not be empty");
        }

        [Fact]
        public void When_updating_an_entity_the_context_should_reflect_changes()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            
            var e = new Entity("account") { Id = Guid.Empty };
            e["name"] = "Before update";
            var guid = service.Create(e);

            Assert.Equal(context.Data["account"][guid]["name"], "Before update");

            //now update the name
            e.Id = guid;
            e["name"] = "After update";
            service.Update(e);

            Assert.Equal(context.Data["account"][guid]["name"], "After update");
        }

        

        
    }
}
