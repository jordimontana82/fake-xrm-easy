using Crm;
using FakeXrmEasy.FakeMessageExecutors.CustomExecutors;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.CustomRequestTests.NavigateToNextEntityRequestTests
{
    public class NavigateToNextEntityRequestTests
    {
        [Fact]
        public void Test_if_Entity_moved_to_next_stage_in_workflow()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            // Entities

            // Process ( Workflow )

            var workflow = new Workflow()
            {
                Id = Guid.NewGuid()
            };

            // Current Stage with Entity

            var contract = new Contract()
            {
                Id = Guid.NewGuid()
            };

            var opp = new Opportunity()
            {
                Id = Guid.NewGuid()
            };

            var currentStage = new ProcessStage()
            {
                Id = Guid.NewGuid()
            };
            currentStage.ProcessId = workflow.ToEntityReference();

            opp.StageId = currentStage.Id;

            // Next Stage with Entity

            var nextStage = new ProcessStage()
            {
                Id = Guid.NewGuid()
            };
            nextStage.ProcessId = workflow.ToEntityReference();

            context.Initialize(new Entity[] { workflow, contract, opp, currentStage, nextStage });

            // Build Request

            OrganizationRequest request = new OrganizationRequest(NavigateToNextEntityOrganizationRequestExecutor.RequestName);

            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterProcessId, workflow.Id);
            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterNewActiveStageId, nextStage.Id);

            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterCurrentEntityLogicalName, opp.LogicalName);
            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterCurrentEntityId, opp.Id);

            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterNextEntityLogicalName, contract.LogicalName);
            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterNextEntityId, contract.Id);

            request.Parameters.Add(NavigateToNextEntityOrganizationRequestExecutor.ParameterNewTraversedPath, string.Join(",", currentStage.Id, nextStage.Id));

            // Execute

            var response = service.Execute(request);
            var traversedPath = response.Results[NavigateToNextEntityOrganizationRequestExecutor.ParameterTraversedPath];

            var oppAfterSet = (from o in context.CreateQuery("opportunity")
                               where o.Id == opp.Id
                               select o).First();

            Assert.True(response != null);
            Assert.True(traversedPath.ToString() == (currentStage.Id + "," + nextStage.Id));
            Assert.True(traversedPath.ToString() == oppAfterSet["traversedpath"].ToString());
        }
    }
}