using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace FakeXrmEasy.Tests.CodeActivitiesForTesting
{
    public sealed partial class CheckContextPropertyActivity : CodeActivity
    {
        /// <summary>
        /// Performs the addition of two summands
        /// </summary>
        protected override void Execute(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory =
                executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service =
                serviceFactory.CreateOrganizationService(context.UserId);

            // Retrieve the summands and perform addition
            this.MessageName.Set(executionContext, context.MessageName);
        }

        [Output("Result")]
        public OutArgument<string> MessageName { get; set; }
    }
}