using Microsoft.Xrm.Sdk.Query;
using System;

namespace FakeXrmEasy.Models
{
    /// <summary>
    /// A condition expression with a decorated type
    /// </summary>
    public class TypedConditionExpression
    {
        public ConditionExpression CondExpression { get; set; }
        public Type AttributeType { get; set; }

        /// <summary>
        /// True if the condition came from a left outer join, in which case should be applied only if not null
        /// </summary>
        public bool IsOuter { get; set; }

        public TypedConditionExpression(ConditionExpression c)
        {
            IsOuter = false;
            CondExpression = c;
        }
    }
}