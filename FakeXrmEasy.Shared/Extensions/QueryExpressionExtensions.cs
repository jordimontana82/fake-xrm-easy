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
                            .Where(le => le.EntityAlias != null && le.EntityAlias.Equals(sAlias))
                            .FirstOrDefault();

            if(linkedEntity != null)
            {
                return linkedEntity.LinkToEntityName;
            }

            //If the alias wasn't found, it means it  could be any of the EntityNames
            return sAlias;
        } 
    }
}
