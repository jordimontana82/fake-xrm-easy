using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FakeXrmEasy.Extensions
{
    public static class QueryExpressionExtensions
    {
        public static string GetEntityNameFromAlias(this QueryExpression qe, string sAlias)
        {
            if (sAlias == null)
                return qe.EntityName;

            var linkedEntity = qe.LinkEntities
                            .Where(le => le.EntityAlias.Equals(sAlias))
                            .FirstOrDefault();

            if(linkedEntity != null)
            {
                return linkedEntity.LinkToEntityName;
            }

            return null;
        } 
    }
}
