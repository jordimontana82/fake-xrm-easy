using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class AccountNumberPlugin : IPlugin
    {
        /// <summary>
        /// A plug-in that auto generates an account number when an
        /// account is created.
        /// </summary>
        /// <remarks>Register this plug-in on the Create message, account entity,
        /// and pre-operation stage.
        /// </remarks>
        //<snippetAccountNumberPlugin2>
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context from the service provider.
            Microsoft.Xrm.Sdk.IPluginExecutionContext context = (Microsoft.Xrm.Sdk.IPluginExecutionContext)
                serviceProvider.GetService(typeof(Microsoft.Xrm.Sdk.IPluginExecutionContext));

            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                tracing.Trace("Contains target");

                // Obtain the target entity from the input parameters.
                Entity entity = (Entity)context.InputParameters["Target"];
                //</snippetAccountNumberPlugin2>

                // Verify that the target entity represents an account.
                // If not, this plug-in was not registered correctly.
                if (entity.LogicalName == "account")
                {
                    tracing.Trace("Is Account");

                    // An accountnumber attribute should not already exist because
                    // it is system generated.
                    if (entity.Attributes.Contains("accountnumber") == false)
                    {
                        // Create a new accountnumber attribute, set its value, and add
                        // the attribute to the entity's attribute collection.
                        Random rndgen = new Random();
                        entity.Attributes.Add("accountnumber", rndgen.Next().ToString());
                    }
                    else
                    {
                        // Throw an error, because account numbers must be system generated.
                        // Throwing an InvalidPluginExecutionException will cause the error message
                        // to be displayed in a dialog of the Web application.
                        throw new InvalidPluginExecutionException("The account number can only be set by the system.");
                    }
                }
            }
        }
    }
}