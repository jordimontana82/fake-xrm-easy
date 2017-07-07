using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;

namespace FakeXrmEasy
{
    public class XrmFakedWorkflowContext : IWorkflowContext
    {
        public Guid BusinessUnitId { get; set; }

        public Guid CorrelationId { get; set; }

        public int Depth { get; set; }

        public Guid InitiatingUserId { get; set; }

        public ParameterCollection InputParameters { get; set; }

        public bool IsExecutingOffline { get; set; }

        public bool IsInTransaction { get; set; }

        public bool IsOfflinePlayback { get; set; }

        public int IsolationMode { get; set; }

        public string MessageName { get; set; }

        public int Mode { get; set; }

        public DateTime OperationCreatedOn { get; set; }

        public Guid OperationId { get; set; }

        public Guid OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public ParameterCollection OutputParameters { get; set; }

        public EntityReference OwningExtension { get; set; }

        public IWorkflowContext ParentContext { get; set; }

        public EntityImageCollection PostEntityImages { get; set; }

        public EntityImageCollection PreEntityImages { get; set; }

        public Guid PrimaryEntityId { get; set; }

        public string PrimaryEntityName { get; set; }

        public Guid? RequestId { get; set; }

        public string SecondaryEntityName { get; set; }

        public ParameterCollection SharedVariables { get; set; }

        public string StageName { get; set; }

        public Guid UserId { get; set; }

        public int WorkflowCategory { get; set; }

#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365
        public int WorkflowMode { get; set; }
#endif
    }
}