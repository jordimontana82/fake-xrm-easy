using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace FakeXrmEasy
{
    /// <summary>
    /// Holds custom properties of a IPluginExecutionContext
    /// Extracted from https://msdn.microsoft.com/es-es/library/microsoft.xrm.sdk.ipluginexecutioncontext_properties.aspx
    /// </summary>
    [DataContract(Name= "PluginExecutionContext", Namespace = "")]
    public class XrmFakedPluginExecutionContext: IPluginExecutionContext
    {

        [DataMember]
        public Guid BusinessUnitId { get; set; }

        [DataMember]
        public Guid CorrelationId { get; set; }

        [DataMember]
        public int Depth { get; set; }

        [DataMember]
        public Guid InitiatingUserId { get; set; }

        [DataMember]
        public ParameterCollection InputParameters { get; set; }

        [DataMember]
        public bool IsExecutingOffline { get; set; }

        [DataMember]
        public bool IsInTransaction { get; set; }

        [DataMember]
        public bool IsOfflinePlayback { get; set; }

        [DataMember]
        public int IsolationMode { get; set; }

        [DataMember]
        public string MessageName { get; set; }

        [DataMember]
        public int Mode { get; set; }

        [DataMember]
        public DateTime OperationCreatedOn { get; set; }

        [DataMember]
        public Guid OperationId { get; set; }

        [DataMember]
        public Guid OrganizationId { get; set; }

        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public ParameterCollection OutputParameters { get; set; }

        [DataMember]
        public EntityReference OwningExtension { get; set; }

        [DataMember]
        public IPluginExecutionContext ParentContext { get; set; }

        [DataMember]
        public EntityImageCollection PostEntityImages { get; set; }

        [DataMember]
        public EntityImageCollection PreEntityImages { get; set; }

        [DataMember]
        public Guid PrimaryEntityId { get; set; }

        [DataMember]
        public string PrimaryEntityName { get; set; }

        [DataMember]
        public Guid? RequestId { get; set; }

        [DataMember]
        public string SecondaryEntityName { get; set; }

        [DataMember]
        public ParameterCollection SharedVariables { get; set; }

        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public XrmFakedPluginExecutionContext()
        {
            Depth = 1;
            IsExecutingOffline = false;
            MessageName = "Create"; //Default value
        }
    }
}
