using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class ValidateReferencesTests
    {
        [Fact]
        public void When_context_is_initialised_validate_references_is_disabled_by_default()
        {
            var context = new XrmFakedContext();
            Assert.False(context.ValidateReferences);
        }

        [Fact]
        public void An_entity_which_references_another_non_existent_entity_can_be_created_when_validate_is_false()
        {
            var context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            Guid otherEntity = Guid.NewGuid();
            Entity entity = new Entity("entity");

            entity["otherEntity"] = new EntityReference("entity", otherEntity);

            Guid created = service.Create(entity);

            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Retrieve("entity", otherEntity, new ColumnSet(true)));

            Assert.NotEqual(Guid.Empty, created);
            Assert.Equal($"{entity.LogicalName} With Id = {otherEntity:D} Does Not Exist", ex.Message);
        }

        [Fact]
        public void An_entity_which_references_another_non_existent_entity_can_not_be_created_when_validate_is_true()
        {
            var context = new XrmFakedContext();
            context.ValidateReferences = true;
            IOrganizationService service = context.GetOrganizationService();

            Guid otherEntity = Guid.NewGuid();
            Entity entity = new Entity("entity");

            entity["otherEntity"] = new EntityReference("entity", otherEntity);

            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Create(entity));

            Assert.Equal($"{entity.LogicalName} With Id = {otherEntity:D} Does Not Exist", ex.Message);
        }

        [Fact]
        public void An_entity_which_references_another_existent_entity_can_be_created_when_validate_is_true()
        {
            var context = new XrmFakedContext();
            context.ValidateReferences = true;
            IOrganizationService service = context.GetOrganizationService();

            Entity otherEntity = new Entity("otherEntity");
            otherEntity.Id = Guid.NewGuid();
            context.Initialize(otherEntity);

            Entity entity = new Entity("entity");
            entity["otherEntity"] = otherEntity.ToEntityReference();

            Guid created = service.Create(entity);

            Entity otherEntityInContext = service.Retrieve("otherEntity", otherEntity.Id, new ColumnSet(true));

            Assert.NotEqual(Guid.Empty, created);
            Assert.Equal(otherEntity.Id, otherEntityInContext.Id);
        }

        [Fact]
        public void An_entity_which_references_another_non_existent_entity_can_be_updated_when_validate_is_false()
        {
            var context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            Entity entity = new Entity("entity");
            entity.Id = Guid.NewGuid();
            context.Initialize(entity);

            Guid otherEntityId = Guid.NewGuid();
            entity["otherEntity"] = new EntityReference("entity", otherEntityId);

            service.Update(entity);

            Entity updated = service.Retrieve(entity.LogicalName, entity.Id, new ColumnSet(true));
            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Retrieve("entity", otherEntityId, new ColumnSet(true)));
            Assert.Equal(otherEntityId, updated.GetAttributeValue<EntityReference>("otherEntity").Id);
            Assert.Equal($"{entity.LogicalName} With Id = {otherEntityId:D} Does Not Exist", ex.Message);
        }

        [Fact]
        public void An_entity_which_references_another_non_existent_entity_can_not_be_updated_when_validate_is_true()
        {
            var context = new XrmFakedContext();
            context.ValidateReferences = true;
            IOrganizationService service = context.GetOrganizationService();

            Entity entity = new Entity("entity");
            entity.Id = Guid.NewGuid();
            context.Initialize(entity);

            Guid otherEntityId = Guid.NewGuid();
            entity["otherEntity"] = new EntityReference("entity", otherEntityId);

            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Update(entity));
            Assert.Equal($"{entity.LogicalName} With Id = {otherEntityId:D} Does Not Exist", ex.Message);
        }

        [Fact]
        public void An_entity_which_references_another_existent_entity_can_be_updated_when_validate_is_true()
        {
            var context = new XrmFakedContext();
            context.ValidateReferences = true;
            IOrganizationService service = context.GetOrganizationService();

            Entity otherEntity = new Entity("otherEntity");
            otherEntity.Id = Guid.NewGuid();

            Entity entity = new Entity("entity");
            entity.Id = Guid.NewGuid();

            context.Initialize(new Entity[] { otherEntity, entity });
            entity["otherEntity"] = otherEntity.ToEntityReference();

            service.Update(entity);

            Entity otherEntityInContext = service.Retrieve("otherEntity", otherEntity.Id, new ColumnSet(true));
            Entity updated = service.Retrieve(entity.LogicalName, entity.Id, new ColumnSet(true));

            Assert.Equal(otherEntity.Id, updated.GetAttributeValue<EntityReference>("otherEntity").Id);
            Assert.Equal(otherEntity.Id, otherEntityInContext.Id);
        }
    }
}
