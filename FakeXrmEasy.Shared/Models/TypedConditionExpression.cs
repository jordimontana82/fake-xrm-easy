using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.Models
{
    /// <summary>
    /// A condition expression with a decorated type
    /// </summary>
    public class TypedConditionExpression
    {
        public ConditionExpression CondExpression { get; set; }
        public Type AttributeType { get; set; }

        public TypedConditionExpression(ConditionExpression c)
        {
            CondExpression = c;
        }
    }
}
