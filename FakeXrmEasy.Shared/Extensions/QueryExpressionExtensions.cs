using Microsoft.Xrm.Sdk.Query;
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

            if (linkedEntity != null)
            {
                return linkedEntity.LinkToEntityName;
            }

            //If the alias wasn't found, it means it  could be any of the EntityNames
            return sAlias;
        }

        /// <summary>
        /// Makes a deep clone of the Query Expression
        /// </summary>
        /// <param name="qe">Query Expression</param>
        /// <returns></returns>
        public static QueryExpression Clone(this QueryExpression qe)
        {
            return qe.Copy();
        }
    }
}