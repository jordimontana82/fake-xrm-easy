using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.Serialization;

namespace FakeXrmEasy
{
    /// <summary>
    /// Holds custom properties of a IPluginExecutionContext
    /// Extracted from https://msdn.microsoft.com/es-es/library/microsoft.xrm.sdk.ipluginexecutioncontext_properties.aspx
    /// </summary>
    [DataContract(Name = "PluginExecutionContext", Namespace = "")]
    public class XrmFakedPluginExecutionContext : IPluginExecutionContext
    {
        [DataMember(Order = 1)]
        public Guid BusinessUnitId { get; set; }

        [DataMember(Order = 2)]
        public Guid CorrelationId { get; set; }

        [DataMember(Order = 3)]
        public int Depth { get; set; }

        [DataMember(Order = 4)]
        public Guid InitiatingUserId { get; set; }

        [DataMember(Order = 5)]
        public ParameterCollection InputParameters { get; set; }

        [DataMember(Order = 6)]
        public bool IsExecutingOffline { get; set; }

        [DataMember(Order = 7)]
        public bool IsInTransaction
        {
            get
            {
                return Stage == (int)ProcessingStepStage.Preoperation || Stage == (int)ProcessingStepStage.Postoperation && Mode == (int)ProcessingStepMode.Synchronous;
            }
            set {  /* This property is writable only to correctly support serialization/deserialization */ }
        }

        [DataMember(Order = 8)]
        public bool IsOfflinePlayback { get; set; }

        [DataMember(Order = 9)]
        public int IsolationMode { get; set; }

        [DataMember(Order = 10)]
        public string MessageName { get; set; }

        [DataMember(Order = 11)]
        public int Mode { get; set; }

        [DataMember(Order = 12)]
        public DateTime OperationCreatedOn { get; set; }

        [DataMember(Order = 13)]
        public Guid OperationId { get; set; }

        [DataMember(Order = 14)]
        public Guid OrganizationId { get; set; }

        [DataMember(Order = 15)]
        public string OrganizationName { get; set; }

        [DataMember(Order = 16)]
        public ParameterCollection OutputParameters { get; set; }

        [DataMember(Order = 17)]
        public EntityReference OwningExtension { get; set; }

        [DataMember(Order = 18)]
        public EntityImageCollection PostEntityImages { get; set; }

        [DataMember(Order = 19)]
        public EntityImageCollection PreEntityImages { get; set; }

        [DataMember(Order = 20)]
        public Guid PrimaryEntityId { get; set; }

        [DataMember(Order = 21)]
        public string PrimaryEntityName { get; set; }

        [DataMember(Order = 22)]
        public Guid? RequestId { get; set; }

        [DataMember(Order = 23)]
        public string SecondaryEntityName { get; set; }

        [DataMember(Order = 24)]
        public ParameterCollection SharedVariables { get; set; }

        [DataMember(Order = 25)]
        public Guid UserId { get; set; }

        [DataMember(Order = 26)]
        public IPluginExecutionContext ParentContext { get; set; }

        [DataMember(Order = 27)]
        public int Stage { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public XrmFakedPluginExecutionContext()
        {
            Depth = 1;
            IsExecutingOffline = false;
            MessageName = "Create"; //Default value,
            IsolationMode = 1;
        }
    }
}