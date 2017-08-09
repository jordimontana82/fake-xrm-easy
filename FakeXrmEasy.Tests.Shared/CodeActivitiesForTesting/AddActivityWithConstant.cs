using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace FakeXrmEasy.Tests.CodeActivitiesForTesting
{
    /// <summary>
    /// A simple code activity that adds 2 parameters and a constant
    /// This is just to test ExecuteCodeActivity methods with specific instances
    /// </summary>
    public sealed partial class AddActivityWithConstant : CodeActivity
    {
        public int Constant { get; set; }

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
            this.result.Set(executionContext,
                this.firstSummand.Get(executionContext) +
                this.secondSummand.Get(executionContext) + Constant);
        }

        // Define Input/Output Arguments
        [Input("First summand")]
        public InArgument<int> firstSummand { get; set; }

        [Input("Second summand")]
        public InArgument<int> secondSummand { get; set; }

        [Output("Result")]
        public OutArgument<int> result { get; set; }
    }
}