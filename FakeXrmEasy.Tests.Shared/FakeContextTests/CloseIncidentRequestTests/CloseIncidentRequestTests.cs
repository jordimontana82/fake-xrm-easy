using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.CloseIncidentRequestTests
{
    public class CloseIncidentRequestTests
    {
        private const int StatusProblemSolved = 5;

        [Fact]
        public void When_a_request_is_called_Incident_Is_Resolved()
        {
            var context = new XrmFakedContext();

            var incident = new Entity
            {
                LogicalName = Crm.Incident.EntityLogicalName,
                Id = Guid.NewGuid()
            };

            context.Initialize(new[]
            {
                incident
            });

            var executor = new CloseIncidentRequestExecutor();

            Entity incidentResolution = new Entity
            {
                LogicalName = Crm.IncidentResolution.EntityLogicalName,
                ["subject"] = "subject",
                ["incidentid"] = new EntityReference(Crm.Incident.EntityLogicalName, incident.Id)
            };

            CloseIncidentRequest closeIncidentRequest = new CloseIncidentRequest
            {
                IncidentResolution = incidentResolution,
                Status = new OptionSetValue(StatusProblemSolved)
            };

            executor.Execute(closeIncidentRequest, context);

            var retrievedIncident = context.Data[Crm.Incident.EntityLogicalName].Values.Single();

            Assert.Equal(StatusProblemSolved, retrievedIncident.GetAttributeValue<OptionSetValue>("statuscode").Value);
            Assert.Equal((int)Crm.IncidentState.Resolved, retrievedIncident.GetAttributeValue<OptionSetValue>("statecode").Value);
        }

        [Fact]
        public void When_a_request_with_invalid_incidentid_is_called_exception_is_raised()
        {
            var context = new XrmFakedContext();
            context.Initialize(new Entity(Crm.Incident.EntityLogicalName) { Id = Guid.NewGuid() });
            var executor = new CloseIncidentRequestExecutor();

            Entity incidentResolution = new Entity
            {
                LogicalName = Crm.IncidentResolution.EntityLogicalName,
                ["subject"] = "subject",
                ["incidentid"] = new EntityReference(Crm.Incident.EntityLogicalName, Guid.NewGuid())
            };

            CloseIncidentRequest closeIncidentRequest = new CloseIncidentRequest
            {
                IncidentResolution = incidentResolution,
                Status = new OptionSetValue(StatusProblemSolved)
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(closeIncidentRequest, context));
        }

        [Fact]
        public void When_a_request_without_incident_resolution_is_called_exception_is_raised()
        {
            var context = new XrmFakedContext();

            var incident = new Entity
            {
                LogicalName = Crm.Incident.EntityLogicalName,
                Id = Guid.NewGuid()
            };

            context.Initialize(new[]
            {
                incident
            });

            var executor = new CloseIncidentRequestExecutor();

            CloseIncidentRequest closeIncidentRequest = new CloseIncidentRequest
            {
                IncidentResolution = null,
                Status = new OptionSetValue(StatusProblemSolved)
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(closeIncidentRequest, context));
        }

        [Fact]
        public void When_a_request_without_status_is_called_exception_is_raised()
        {
            var context = new XrmFakedContext();

            var incident = new Entity
            {
                LogicalName = Crm.Incident.EntityLogicalName,
                Id = Guid.NewGuid()
            };

            context.Initialize(new[]
            {
                incident
            });

            Entity incidentResolution = new Entity
            {
                LogicalName = Crm.IncidentResolution.EntityLogicalName,
                ["subject"] = "subject",
                ["incidentid"] = new EntityReference(Crm.Incident.EntityLogicalName, incident.Id)
            };

            var executor = new CloseIncidentRequestExecutor();

            CloseIncidentRequest closeIncidentRequest = new CloseIncidentRequest
            {
                IncidentResolution = incidentResolution,
                Status = null
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => executor.Execute(closeIncidentRequest, context));
        }

        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new CloseIncidentRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }
    }
}