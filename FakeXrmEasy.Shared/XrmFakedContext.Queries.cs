using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using FakeXrmEasy.Extensions;
using FakeXrmEasy.Extensions.FetchXml;
using FakeXrmEasy.Models;
using FakeXrmEasy.OrganizationFaults;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmContext
    {
        protected internal Type FindReflectedType(string logicalName)
        {
            var types =
                ProxyTypesAssemblies.Select(a => FindReflectedType(logicalName, a))
                                    .Where(t => t != null);

            if(types.Count() > 1) {
                var errorMsg = $"Type { logicalName } is defined in multiple assemblies: ";
                foreach(var type in types) {
                    errorMsg += type.Assembly
                                    .GetName()
                                    .Name + "; ";
                }
                var lastIndex = errorMsg.LastIndexOf("; ");
                errorMsg = errorMsg.Substring(0, lastIndex) + ".";
                throw new InvalidOperationException(errorMsg);
            }

            return types.SingleOrDefault();
        }

        /// <summary>
        /// Finds reflected type for given entity from given assembly.
        /// </summary>
        /// <param name="logicalName">
        /// Entity logical name which type is searched from given
        /// <paramref name="assembly"/>.
        /// </param>
        /// <param name="assembly">
        /// Assembly where early-bound type is searched for given
        /// <paramref name="logicalName"/>.
        /// </param>
        /// <returns>
        /// Early-bound type of <paramref name="logicalName"/> if it's found
        /// from <paramref name="assembly"/>. Otherwise null is returned.
        /// </returns>
        private static Type FindReflectedType(string logicalName,
                                              Assembly assembly)
        {
            try
            {
                if (assembly == null)
                {
                    throw new ArgumentNullException(nameof(assembly));
                }

                /* This wasn't building within the CI FAKE build script...
                var subClassType = assembly.GetTypes()
                        .Where(t => typeof(Entity).IsAssignableFrom(t))
                        .Where(t => t.GetCustomAttributes<EntityLogicalNameAttribute>(true).Any())
                        .FirstOrDefault(t => t.GetCustomAttributes<EntityLogicalNameAttribute>(true).First().LogicalName.Equals(logicalName, StringComparison.OrdinalIgnoreCase));

                */
                var subClassType = assembly.GetTypes()
                        .Where(t => typeof(Entity).IsAssignableFrom(t))
                        .Where(t => t.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true).Length > 0)
                        .Where(t => ((EntityLogicalNameAttribute)t.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true)[0]).LogicalName.Equals(logicalName.ToLower()))
                        .FirstOrDefault();

                return subClassType;
            }
            catch (ReflectionTypeLoadException exception)
            {
                // now look at ex.LoaderExceptions - this is an Exception[], so:
                var s = "";
                foreach (var innerException in exception.LoaderExceptions)
                {
                    // write details of "inner", in particular inner.Message
                    s += innerException.Message + " ";
                }

                throw new Exception("XrmFakedContext.FindReflectedType: " + s);
            }
        }

        protected internal Type FindAttributeTypeInInjectedMetadata(string sEntityName, string sAttributeName)
        {
            if (!EntityMetadata.ContainsKey(sEntityName))
                return null;

            if (EntityMetadata[sEntityName].Attributes == null)
                return null;

            var attribute = EntityMetadata[sEntityName].Attributes
                                .Where(a => a.LogicalName == sAttributeName)
                                .FirstOrDefault();

            if (attribute == null)
                return null;

            if (attribute.AttributeType == null)
                return null;

            switch (attribute.AttributeType.Value)
            {
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.BigInt:
                    return typeof(long);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Integer:
                    return typeof(int);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Boolean:
                    return typeof(bool);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.CalendarRules:
                    throw new Exception("CalendarRules: Type not yet supported");

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Lookup:
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Customer:
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Owner:
                    return typeof(EntityReference);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.DateTime:
                    return typeof(DateTime);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Decimal:
                    return typeof(decimal);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Double:
                    return typeof(double);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.EntityName:
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Memo:
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.String:
                    return typeof(string);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Money:
                    return typeof(Money);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.PartyList:
                    return typeof(EntityReferenceCollection);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Picklist:
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.State:
                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Status:
                    return typeof(OptionSetValue);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Uniqueidentifier:
                    return typeof(Guid);

                case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Virtual:
#if FAKE_XRM_EASY_9
                    if (attribute.AttributeTypeName.Value == "MultiSelectPicklistType")
                    {
                        return typeof(OptionSetValueCollection);
                    }
#endif
                    throw new Exception("Virtual: Type not yet supported");

                default:
                    return typeof(string);

            }

        }
        protected internal Type FindReflectedAttributeType(Type earlyBoundType, string sEntityName, string attributeName)
        {
            //Get that type properties
            var attributeInfo = GetEarlyBoundTypeAttribute(earlyBoundType, attributeName);
            if (attributeInfo == null && attributeName.EndsWith("name"))
            {
                // Special case for referencing the name of a EntityReference
                attributeName = attributeName.Substring(0, attributeName.Length - 4);
                attributeInfo = GetEarlyBoundTypeAttribute(earlyBoundType, attributeName);

                if (attributeInfo.PropertyType != typeof(EntityReference))
                {
                    // Don't mess up if other attributes follow this naming pattern
                    attributeInfo = null;
                }
            }

            if (attributeInfo == null || attributeInfo.PropertyType.FullName == null)
            {
                //Try with metadata
                var injectedType = FindAttributeTypeInInjectedMetadata(sEntityName, attributeName);

                if (injectedType == null)
                {
                    throw new Exception($"XrmFakedContext.FindReflectedAttributeType: Attribute {attributeName} not found for type {earlyBoundType}");
                }

                return injectedType;
            }

            if (attributeInfo.PropertyType.FullName.EndsWith("Enum") || attributeInfo.PropertyType.BaseType.FullName.EndsWith("Enum"))
            {
                return typeof(int);
            }

            if (!attributeInfo.PropertyType.FullName.StartsWith("System."))
            {
                try
                {
                    var instance = Activator.CreateInstance(attributeInfo.PropertyType);
                    if (instance is Entity)
                    {
                        return typeof(EntityReference);
                    }
                }
                catch
                {
                    // ignored
                }
            }
#if FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
            else if (attributeInfo.PropertyType.FullName.StartsWith("System.Nullable"))
            {
                return attributeInfo.PropertyType.GenericTypeArguments[0];
            }
#endif

            return attributeInfo.PropertyType;
        }

        private static PropertyInfo GetEarlyBoundTypeAttribute(Type earlyBoundType, string attributeName)
        {
            var attributeInfo = earlyBoundType.GetProperties()
                .Where(pi => pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true).Length > 0)
                .Where(pi => (pi.GetCustomAttributes(typeof(AttributeLogicalNameAttribute), true)[0] as AttributeLogicalNameAttribute).LogicalName.Equals(attributeName))
                .FirstOrDefault();

            return attributeInfo;
        }

        public IQueryable<Entity> CreateQuery(string entityLogicalName)
        {
            return this.CreateQuery<Entity>(entityLogicalName);
        }

        public IQueryable<T> CreateQuery<T>()
            where T : Entity
        {
            var typeParameter = typeof(T);

            if (ProxyTypesAssembly == null)
            {
                //Try to guess proxy types assembly
                var assembly = Assembly.GetAssembly(typeof(T));
                if (assembly != null)
                {
                    ProxyTypesAssembly = assembly;
                }
            }

            var logicalName = "";

            if (typeParameter.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true).Length > 0)
            {
                logicalName = (typeParameter.GetCustomAttributes(typeof(EntityLogicalNameAttribute), true)[0] as EntityLogicalNameAttribute).LogicalName;
            }

            return this.CreateQuery<T>(logicalName);
        }

        protected IQueryable<T> CreateQuery<T>(string entityLogicalName)
            where T : Entity
        {
            var subClassType = FindReflectedType(entityLogicalName);
            if (subClassType == null && !(typeof(T) == typeof(Entity)) || (typeof(T) == typeof(Entity) && string.IsNullOrWhiteSpace(entityLogicalName)))
            {
                throw new Exception($"The type {entityLogicalName} was not found");
            }

            var lst = new List<T>();
            if (!Data.ContainsKey(entityLogicalName))
            {
                return lst.AsQueryable(); //Empty list
            }

            foreach (var e in Data[entityLogicalName].Values)
            {
                if (subClassType != null)
                {
                    var cloned = e.Clone(subClassType);
                    lst.Add((T)cloned);
                }
                else
                    lst.Add((T)e.Clone());
            }

            return lst.AsQueryable();
        }

        public IQueryable<Entity> CreateQueryFromEntityName(string entityName)
        {
            return Data[entityName].Values.AsQueryable();
        }

        public static IQueryable<Entity> TranslateLinkedEntityToLinq(XrmFakedContext context, LinkEntity le, IQueryable<Entity> query, ColumnSet previousColumnSet, Dictionary<string, int> linkedEntities, string linkFromAlias = "", string linkFromEntity = "")
        {
            if (!string.IsNullOrEmpty(le.EntityAlias))
            {
                if (!Regex.IsMatch(le.EntityAlias, "^[A-Za-z_]\\w*$", RegexOptions.ECMAScript))
                {
                    throw new FaultException(new FaultReason($"Invalid character specified for alias: {le.EntityAlias}. Only characters within the ranges [A-Z], [a-z] or [0-9] or _ are allowed.  The first character may only be in the ranges [A-Z], [a-z] or _."));
                }
            }

            var leAlias = string.IsNullOrWhiteSpace(le.EntityAlias) ? le.LinkToEntityName : le.EntityAlias;
            context.EnsureEntityNameExistsInMetadata(le.LinkFromEntityName != linkFromAlias ? le.LinkFromEntityName : linkFromEntity);
            context.EnsureEntityNameExistsInMetadata(le.LinkToEntityName);

            if (!context.AttributeExistsInMetadata(le.LinkToEntityName, le.LinkToAttributeName))
            {
                OrganizationServiceFaultQueryBuilderNoAttributeException.Throw(le.LinkToAttributeName);
            }

            IQueryable<Entity> inner = null;
            if(le.JoinOperator == JoinOperator.LeftOuter)
            {
                //inner = context.CreateQuery<Entity>(le.LinkToEntityName);

                
                //filters are applied in the inner query and then ignored during filter evaluation
                var outerQueryExpression = new QueryExpression()
                {
                    EntityName = le.LinkToEntityName,
                    Criteria = le.LinkCriteria,
                    ColumnSet = new ColumnSet(true)
                };

                var outerQuery = TranslateQueryExpressionToLinq(context, outerQueryExpression);
                inner = outerQuery;
                
            }
            else
            {
                //Filters are applied after joins
                inner = context.CreateQuery<Entity>(le.LinkToEntityName);
            }
            
            //if (!le.Columns.AllColumns && le.Columns.Columns.Count == 0)
            //{
            //    le.Columns.AllColumns = true;   //Add all columns in the joined entity, otherwise we can't filter by related attributes, then the Select will actually choose which ones we need
            //}

            if (string.IsNullOrWhiteSpace(linkFromAlias))
            {
                linkFromAlias = le.LinkFromAttributeName;
            }
            else
            {
                linkFromAlias += "." + le.LinkFromAttributeName;
            }

            switch (le.JoinOperator)
            {
                case JoinOperator.Inner:
                case JoinOperator.Natural:
                    query = query.Join(inner,
                                    outerKey => outerKey.KeySelector(linkFromAlias, context),
                                    innerKey => innerKey.KeySelector(le.LinkToAttributeName, context),
                                    (outerEl, innerEl) => outerEl.Clone(outerEl.GetType()).JoinAttributes(innerEl, new ColumnSet(true), leAlias, context));

                    break;
                case JoinOperator.LeftOuter:
                    query = query.GroupJoin(inner,
                                    outerKey => outerKey.KeySelector(linkFromAlias, context),
                                    innerKey => innerKey.KeySelector(le.LinkToAttributeName, context),
                                    (outerEl, innerElemsCol) => new { outerEl, innerElemsCol })
                                                .SelectMany(x => x.innerElemsCol.DefaultIfEmpty()
                                                            , (x, y) => x.outerEl
                                                                            .JoinAttributes(y, new ColumnSet(true), leAlias, context));


                    break;
                default: //This shouldn't be reached as there are only 3 types of Join...
                    throw new PullRequestException(string.Format("The join operator {0} is currently not supported. Feel free to implement and send a PR.", le.JoinOperator));

            }

            // Process nested linked entities recursively
            foreach (var nestedLinkedEntity in le.LinkEntities)
            {
                if (string.IsNullOrWhiteSpace(le.EntityAlias))
                {
                    le.EntityAlias = le.LinkToEntityName;
                }

                if (string.IsNullOrWhiteSpace(nestedLinkedEntity.EntityAlias))
                {
                    nestedLinkedEntity.EntityAlias = EnsureUniqueLinkedEntityAlias(linkedEntities, nestedLinkedEntity.LinkToEntityName);
                }

                query = TranslateLinkedEntityToLinq(context, nestedLinkedEntity, query, le.Columns, linkedEntities, le.EntityAlias, le.LinkToEntityName);
            }

            return query;
        }

        private static string EnsureUniqueLinkedEntityAlias(IDictionary<string, int> linkedEntities, string entityName)
        {
            if (linkedEntities.ContainsKey(entityName))
            {
                linkedEntities[entityName]++;
            }
            else
            {
                linkedEntities[entityName] = 1;
            }

            return $"{entityName}{linkedEntities[entityName]}";
        }


        protected static XElement RetrieveFetchXmlNode(XDocument xlDoc, string sName)
        {
            return xlDoc.Descendants().Where(e => e.Name.LocalName.Equals(sName)).FirstOrDefault();
        }

        public static XDocument ParseFetchXml(string fetchXml)
        {
            try
            {
                return XDocument.Parse(fetchXml);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FetchXml must be a valid XML document: {0}", ex.ToString()));
            }
        }

        public static QueryExpression TranslateFetchXmlToQueryExpression(XrmFakedContext context, string fetchXml)
        {
            return TranslateFetchXmlDocumentToQueryExpression(context, ParseFetchXml(fetchXml));
        }

        public static QueryExpression TranslateFetchXmlDocumentToQueryExpression(XrmFakedContext context, XDocument xlDoc)
        {
            //Validate nodes
            if (!xlDoc.Descendants().All(el => el.IsFetchXmlNodeValid()))
                throw new Exception("At least some node is not valid");

            //Root node
            if (!xlDoc.Root.Name.LocalName.Equals("fetch"))
            {
                throw new Exception("Root node must be fetch");
            }

            var entityNode = RetrieveFetchXmlNode(xlDoc, "entity");
            var query = new QueryExpression(entityNode.GetAttribute("name").Value);

            query.ColumnSet = xlDoc.ToColumnSet();

            // Ordering is done after grouping/aggregation
            if (!xlDoc.IsAggregateFetchXml())
            {
                var orders = xlDoc.ToOrderExpressionList();
                foreach (var order in orders)
                {
                    query.AddOrder(order.AttributeName, order.OrderType);
                }
            }

            query.Criteria = xlDoc.ToCriteria(context);

            query.TopCount = xlDoc.ToTopCount();

            query.PageInfo.Count = xlDoc.ToCount() ?? 0;
            query.PageInfo.PageNumber = xlDoc.ToPageNumber() ?? 1;

            var linkedEntities = xlDoc.ToLinkEntities(context);
            foreach (var le in linkedEntities)
            {
                query.LinkEntities.Add(le);
            }

            return query;
        }

        public static IQueryable<Entity> TranslateQueryExpressionToLinq(XrmFakedContext context, QueryExpression qe)
        {
            if (qe == null) return null;

            //Start form the root entity and build a LINQ query to execute the query against the In-Memory context:
            context.EnsureEntityNameExistsInMetadata(qe.EntityName);

            IQueryable<Entity> query = null;

            query = context.CreateQuery<Entity>(qe.EntityName);

            var linkedEntities = new Dictionary<string, int>();

            // Add as many Joins as linked entities
            foreach (var le in qe.LinkEntities)
            {
                if (string.IsNullOrWhiteSpace(le.EntityAlias))
                {
                    le.EntityAlias = EnsureUniqueLinkedEntityAlias(linkedEntities, le.LinkToEntityName);
                }

                query = TranslateLinkedEntityToLinq(context, le, query, qe.ColumnSet, linkedEntities);
            }

            // Compose the expression tree that represents the parameter to the predicate.
            ParameterExpression entity = Expression.Parameter(typeof(Entity));
            var expTreeBody = TranslateQueryExpressionFiltersToExpression(context, qe, entity);
            Expression<Func<Entity, bool>> lambda = Expression.Lambda<Func<Entity, bool>>(expTreeBody, entity);
            query = query.Where(lambda);

            //Sort results
            if (qe.Orders != null)
            {
                if (qe.Orders.Count > 0)
                {
                    IOrderedQueryable<Entity> orderedQuery = null;

                    var order = qe.Orders[0];
                    if (order.OrderType == OrderType.Ascending)
                        orderedQuery = query.OrderBy(e => e.Attributes.ContainsKey(order.AttributeName) ? e[order.AttributeName] : null, new XrmOrderByAttributeComparer());
                    else
                        orderedQuery = query.OrderByDescending(e => e.Attributes.ContainsKey(order.AttributeName) ? e[order.AttributeName] : null, new XrmOrderByAttributeComparer());

                    //Subsequent orders should use ThenBy and ThenByDescending
                    for (var i = 1; i < qe.Orders.Count; i++)
                    {
                        var thenOrder = qe.Orders[i];
                        if (thenOrder.OrderType == OrderType.Ascending)
                            orderedQuery = orderedQuery.ThenBy(e => e.Attributes.ContainsKey(thenOrder.AttributeName) ? e[thenOrder.AttributeName] : null, new XrmOrderByAttributeComparer());
                        else
                            orderedQuery = orderedQuery.ThenByDescending(e => e[thenOrder.AttributeName], new XrmOrderByAttributeComparer());
                    }

                    query = orderedQuery;
                }
            }

            //Project the attributes in the root column set  (must be applied after the where and order clauses, not before!!)
            query = query.Select(x => x.Clone(x.GetType()).ProjectAttributes(qe, context));

            return query;
        }


        protected static Expression TranslateConditionExpression(QueryExpression qe, XrmFakedContext context, TypedConditionExpression c, ParameterExpression entity)
        {
            Expression attributesProperty = Expression.Property(
                entity,
                "Attributes"
                );


            //If the attribute comes from a joined entity, then we need to access the attribute from the join
            //But the entity name attribute only exists >= 2013
#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
            string attributeName = "";

            //Do not prepend the entity name if the EntityLogicalName is the same as the QueryExpression main logical name

            if (!string.IsNullOrWhiteSpace(c.CondExpression.EntityName) && !c.CondExpression.EntityName.Equals(qe.EntityName))
            {
                attributeName = c.CondExpression.EntityName + "." + c.CondExpression.AttributeName;
            }
            else
                attributeName = c.CondExpression.AttributeName;

            Expression containsAttributeExpression = Expression.Call(
                attributesProperty,
                typeof(AttributeCollection).GetMethod("ContainsKey", new Type[] { typeof(string) }),
                Expression.Constant(attributeName)
                );

            Expression getAttributeValueExpr = Expression.Property(
                            attributesProperty, "Item",
                            Expression.Constant(attributeName, typeof(string))
                            );
#else
            Expression containsAttributeExpression = Expression.Call(
                attributesProperty,
                typeof(AttributeCollection).GetMethod("ContainsKey", new Type[] { typeof(string) }),
                Expression.Constant(c.CondExpression.AttributeName)
                );

            Expression getAttributeValueExpr = Expression.Property(
                            attributesProperty, "Item",
                            Expression.Constant(c.CondExpression.AttributeName, typeof(string))
                            );
#endif



            Expression getNonBasicValueExpr = getAttributeValueExpr;

            Expression operatorExpression = null;

            switch (c.CondExpression.Operator)
            {
                case ConditionOperator.Equal:
                case ConditionOperator.Today:
                case ConditionOperator.Yesterday:
                case ConditionOperator.Tomorrow:
                case ConditionOperator.EqualUserId:
                    operatorExpression = TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotEqualUserId:
                    operatorExpression = Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.EqualBusinessId:
                    operatorExpression = TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotEqualBusinessId:
                    operatorExpression = Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.BeginsWith:
                case ConditionOperator.Like:
                    operatorExpression = TranslateConditionExpressionLike(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.EndsWith:
                    operatorExpression = TranslateConditionExpressionEndsWith(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.Contains:
                    operatorExpression = TranslateConditionExpressionContains(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotEqual:
                    operatorExpression = Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.DoesNotBeginWith:
                case ConditionOperator.DoesNotEndWith:
                case ConditionOperator.NotLike:
                case ConditionOperator.DoesNotContain:
                    operatorExpression = Expression.Not(TranslateConditionExpressionLike(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.Null:
                    operatorExpression = TranslateConditionExpressionNull(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotNull:
                    operatorExpression = Expression.Not(TranslateConditionExpressionNull(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.GreaterThan:
                    operatorExpression = TranslateConditionExpressionGreaterThan(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.GreaterEqual:
                    operatorExpression = TranslateConditionExpressionGreaterThanOrEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.LessThan:
                    operatorExpression = TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.LessEqual:
                    operatorExpression = TranslateConditionExpressionLessThanOrEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.In:
                    operatorExpression = TranslateConditionExpressionIn(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotIn:
                    operatorExpression = Expression.Not(TranslateConditionExpressionIn(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.On:
                    operatorExpression = TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotOn:
                    operatorExpression = Expression.Not(TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.OnOrAfter:
                    operatorExpression = Expression.Or(
                               TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression),
                               TranslateConditionExpressionGreaterThan(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;
                case ConditionOperator.Last7Days:
                    operatorExpression = TranslateConditionExpressionLast(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.OnOrBefore:
                    operatorExpression = Expression.Or(
                                TranslateConditionExpressionEqual(context, c, getNonBasicValueExpr, containsAttributeExpression),
                                TranslateConditionExpressionLessThan(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;

                case ConditionOperator.Between:
                    if (c.CondExpression.Values.Count != 2)
                    {
                        throw new Exception("Between operator requires exactly 2 values.");
                    }
                    operatorExpression = TranslateConditionExpressionBetween(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.NotBetween:
                    if (c.CondExpression.Values.Count != 2)
                    {
                        throw new Exception("Not-Between operator requires exactly 2 values.");
                    }
                    operatorExpression = Expression.Not(TranslateConditionExpressionBetween(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;
                case ConditionOperator.OlderThanXMonths:
                    var monthsToAdd = 0;
                    var parsedMonths = int.TryParse(c.CondExpression.Values[0].ToString(), out monthsToAdd);

                    if (parsedMonths == false)
                    {
                        throw new Exception("Older than X months requires an integer value in the ConditionExpression.");
                    }

                    if (monthsToAdd <= 0)
                    {
                        throw new Exception("Older than X months requires a value greater than 0.");
                    }

                    var olderThanDate = DateTime.Now.AddMonths(-monthsToAdd);

                    operatorExpression = TranslateConditionExpressionOlderThan(c, getNonBasicValueExpr, containsAttributeExpression, olderThanDate);
                    break;

                case ConditionOperator.NextXWeeks:
                    operatorExpression = TranslateConditionExpressionNext(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.ThisYear:
                case ConditionOperator.LastYear:
                case ConditionOperator.NextYear:
                case ConditionOperator.ThisMonth:
                    operatorExpression = TranslateConditionExpressionBetweenDates(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

#if FAKE_XRM_EASY_9
                case ConditionOperator.ContainValues:
                    operatorExpression = TranslateConditionExpressionContainValues(c, getNonBasicValueExpr, containsAttributeExpression);
                    break;

                case ConditionOperator.DoesNotContainValues:
                    operatorExpression = Expression.Not(TranslateConditionExpressionContainValues(c, getNonBasicValueExpr, containsAttributeExpression));
                    break;
#endif

                default:
                    throw new PullRequestException(string.Format("Operator {0} not yet implemented for condition expression", c.CondExpression.Operator.ToString()));


            }

            if (c.IsOuter)
            {
                //If outer join, filter is optional, only if there was a value
                return Expression.Constant(true);
            }
            else
                return operatorExpression;

        }

        private static void ValidateSupportedTypedExpression(TypedConditionExpression typedExpression)
        {
            Expression validateOperatorTypeExpression = Expression.Empty();
            ConditionOperator[] supportedOperators = (ConditionOperator[])Enum.GetValues(typeof(ConditionOperator));

#if FAKE_XRM_EASY_9
            if (typedExpression.AttributeType == typeof(OptionSetValueCollection))
            {
                supportedOperators = new[]
                {
                    ConditionOperator.ContainValues,
                    ConditionOperator.DoesNotContainValues,
                    ConditionOperator.Equal,
                    ConditionOperator.NotEqual,
                    ConditionOperator.NotNull,
                    ConditionOperator.Null,
                    ConditionOperator.In,
                    ConditionOperator.NotIn,
                };
            }
#endif

            if (!supportedOperators.Contains(typedExpression.CondExpression.Operator))
            {
                OrganizationServiceFaultOperatorIsNotValidException.Throw();
            }
        }
        
        protected static Expression GetAppropiateTypedValue(object value)
        {
            //Basic types conversions
            //Special case => datetime is sent as a string
            if (value is string)
            {
                DateTime dtDateTimeConversion;
                if (DateTime.TryParse(value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dtDateTimeConversion))
                {
                    return Expression.Constant(dtDateTimeConversion, typeof(DateTime));
                }
                else
                {
                    return GetCaseInsensitiveExpression(Expression.Constant(value, typeof(string)));
                }
            }
            else if (value is EntityReference)
            {
                var cast = (value as EntityReference).Id;
                return Expression.Constant(cast);
            }
            else if (value is OptionSetValue)
            {
                var cast = (value as OptionSetValue).Value;
                return Expression.Constant(cast);
            }
            else if (value is Money)
            {
                var cast = (value as Money).Value;
                return Expression.Constant(cast);
            }
            return Expression.Constant(value);
        }

        protected static Type GetAppropiateTypeForValue(object value)
        {
            //Basic types conversions
            //Special case => datetime is sent as a string
            if (value is string)
            {
                DateTime dtDateTimeConversion;
                if (DateTime.TryParse(value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dtDateTimeConversion))
                {
                    return typeof(DateTime);
                }
                else
                {
                    return typeof(string);
                }
            }
            else
                return value.GetType();
        }

        protected static Expression GetAppropiateTypedValueAndType(object value, Type attributeType)
        {
            if (attributeType == null)
                return GetAppropiateTypedValue(value);


            //Basic types conversions
            //Special case => datetime is sent as a string
            if (value is string)
            {
                int iValue;

                DateTime dtDateTimeConversion;
                Guid id;
                if (attributeType.IsDateTime()  //Only convert to DateTime if the attribute's type was DateTime
                    && DateTime.TryParse(value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dtDateTimeConversion))
                {
                    return Expression.Constant(dtDateTimeConversion, typeof(DateTime));
                }
                else if (attributeType.IsOptionSet() && int.TryParse(value.ToString(), out iValue))
                {
                    return Expression.Constant(iValue, typeof(int));
                }
                else if (attributeType == typeof(EntityReference) && Guid.TryParse((string)value, out id))
                {
                    return Expression.Constant(id);
                }
                else
                {
                    return GetCaseInsensitiveExpression(Expression.Constant(value, typeof(string)));
                }
            }
            else if (value is EntityReference)
            {
                var cast = (value as EntityReference).Id;
                return Expression.Constant(cast);
            }
            else if (value is OptionSetValue)
            {
                var cast = (value as OptionSetValue).Value;
                return Expression.Constant(cast);
            }
            else if (value is Money)
            {
                var cast = (value as Money).Value;
                return Expression.Constant(cast);
            }
            return Expression.Constant(value);
        }

        protected static Expression GetAppropiateCastExpressionBasedOnType(Type t, Expression input, object value)
        {
            var typedExpression = GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(input, value, t);

            //Now, any value (entity reference, string, int, etc,... could be wrapped in an AliasedValue object
            //So let's add this
            var getValueFromAliasedValueExp = Expression.Call(Expression.Convert(input, typeof(AliasedValue)),
                                            typeof(AliasedValue).GetMethod("get_Value"));

            var exp = Expression.Condition(Expression.TypeIs(input, typeof(AliasedValue)),
                    GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(getValueFromAliasedValueExp, value, t),
                    typedExpression //Not an aliased value
                );

            return exp;
        }

        //protected static Expression GetAppropiateCastExpressionBasedOnValue(XrmFakedContext context, Expression input, object value)
        //{
        //    var typedExpression = GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(context, input, value, sEntityName, sAttributeName);

        //    //Now, any value (entity reference, string, int, etc,... could be wrapped in an AliasedValue object
        //    //So let's add this
        //    var getValueFromAliasedValueExp = Expression.Call(Expression.Convert(input, typeof(AliasedValue)),
        //                                    typeof(AliasedValue).GetMethod("get_Value"));

        //    var  exp = Expression.Condition(Expression.TypeIs(input, typeof(AliasedValue)),
        //            GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(context, getValueFromAliasedValueExp, value, sEntityName, sAttributeName),
        //            typedExpression //Not an aliased value
        //        );

        //    return exp;
        //}

        protected static Expression GetAppropiateCastExpressionBasedOnValueInherentType(Expression input, object value)
        {
            if (value is Guid || value is EntityReference)
                return GetAppropiateCastExpressionBasedGuid(input); //Could be compared against an EntityReference
            if (value is int || value is OptionSetValue)
                return GetAppropiateCastExpressionBasedOnInt(input); //Could be compared against an OptionSet
            if (value is decimal || value is Money)
                return GetAppropiateCastExpressionBasedOnDecimal(input); //Could be compared against a Money
            if (value is bool)
                return GetAppropiateCastExpressionBasedOnBoolean(input); //Could be a BooleanManagedProperty
            if (value is string)
            {
                return GetAppropiateCastExpressionBasedOnString(input, value);
            }
            return GetAppropiateCastExpressionDefault(input, value); //any other type
        }

        protected static Expression GetAppropiateCastExpressionBasedOnAttributeTypeOrValue(Expression input, object value, Type attributeType)
        {
            if (attributeType != null)
            {

#if FAKE_XRM_EASY || FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015
                if (attributeType == typeof(Microsoft.Xrm.Client.CrmEntityReference))
                    return GetAppropiateCastExpressionBasedGuid(input);
#endif
                if (attributeType == typeof(Guid))
                    return GetAppropiateCastExpressionBasedGuid(input);
                if (attributeType == typeof(EntityReference))
                    return GetAppropiateCastExpressionBasedOnEntityReference(input, value);
                if (attributeType == typeof(int) || attributeType == typeof(Nullable<int>) || attributeType.IsOptionSet())
                    return GetAppropiateCastExpressionBasedOnInt(input);
                if (attributeType == typeof(decimal) || attributeType == typeof(Money))
                    return GetAppropiateCastExpressionBasedOnDecimal(input);
                if (attributeType == typeof(bool) || attributeType == typeof(BooleanManagedProperty))
                    return GetAppropiateCastExpressionBasedOnBoolean(input);
                if (attributeType == typeof(string))
                    return GetAppropiateCastExpressionBasedOnStringAndType(input, value, attributeType);
                if (attributeType.IsDateTime())
                    return GetAppropiateCastExpressionBasedOnDateTime(input, value);
#if FAKE_XRM_EASY_9
                if (attributeType.IsOptionSetValueCollection())
                    return GetAppropiateCastExpressionBasedOnOptionSetValueCollection(input);
#endif

                return GetAppropiateCastExpressionDefault(input, value); //any other type
            }

            return GetAppropiateCastExpressionBasedOnValueInherentType(input, value); //Dynamic entities
        }
        protected static Expression GetAppropiateCastExpressionBasedOnString(Expression input, object value)
        {
            var defaultStringExpression = GetCaseInsensitiveExpression(GetAppropiateCastExpressionDefault(input, value));

            DateTime dtDateTimeConversion;
            if (DateTime.TryParse(value.ToString(), out dtDateTimeConversion))
            {
                return Expression.Convert(input, typeof(DateTime));
            }

            int iValue;
            if (int.TryParse(value.ToString(), out iValue))
            {
                return Expression.Condition(Expression.TypeIs(input, typeof(OptionSetValue)),
                    GetToStringExpression<Int32>(GetAppropiateCastExpressionBasedOnInt(input)),
                    defaultStringExpression
                );
            }

            return defaultStringExpression;
        }

        protected static Expression GetAppropiateCastExpressionBasedOnStringAndType(Expression input, object value, Type attributeType)
        {
            var defaultStringExpression = GetCaseInsensitiveExpression(GetAppropiateCastExpressionDefault(input, value));

            int iValue;
            if (attributeType.IsOptionSet() && int.TryParse(value.ToString(), out iValue))
            {
                return Expression.Condition(Expression.TypeIs(input, typeof(OptionSetValue)),
                    GetToStringExpression<Int32>(GetAppropiateCastExpressionBasedOnInt(input)),
                    defaultStringExpression
                );
            }

            return defaultStringExpression;
        }

        protected static Expression GetAppropiateCastExpressionBasedOnDateTime(Expression input, object value)
        {
            // Convert to DateTime if string
            DateTime _;
            if (value is DateTime || value is string && DateTime.TryParse(value.ToString(), out _))
            {
                return Expression.Convert(input, typeof(DateTime));
            }

            return input; // return directly
        }

        protected static Expression GetAppropiateCastExpressionDefault(Expression input, object value)
        {
            return Expression.Convert(input, value.GetType());  //Default type conversion
        }
        protected static Expression GetAppropiateCastExpressionBasedGuid(Expression input)
        {
            var getIdFromEntityReferenceExpr = Expression.Call(Expression.TypeAs(input, typeof(EntityReference)),
                typeof(EntityReference).GetMethod("get_Id"));

            return Expression.Condition(
                Expression.TypeIs(input, typeof(EntityReference)),  //If input is an entity reference, compare the Guid against the Id property
                Expression.Convert(
                    getIdFromEntityReferenceExpr,
                    typeof(Guid)),
                Expression.Condition(Expression.TypeIs(input, typeof(Guid)),  //If any other case, then just compare it as a Guid directly
                    Expression.Convert(input, typeof(Guid)),
                    Expression.Constant(Guid.Empty, typeof(Guid))));
        }

        protected static Expression GetAppropiateCastExpressionBasedOnEntityReference(Expression input, object value)
        {
            Guid guid;
            if (value is string && !Guid.TryParse((string)value, out guid))
            {
                var getNameFromEntityReferenceExpr = Expression.Call(Expression.TypeAs(input, typeof(EntityReference)),
                    typeof(EntityReference).GetMethod("get_Name"));

                return GetCaseInsensitiveExpression(Expression.Condition(Expression.TypeIs(input, typeof(EntityReference)),
                    Expression.Convert(getNameFromEntityReferenceExpr, typeof(string)),
                    Expression.Constant(string.Empty, typeof(string))));
            }

            var getIdFromEntityReferenceExpr = Expression.Call(Expression.TypeAs(input, typeof(EntityReference)),
                typeof(EntityReference).GetMethod("get_Id"));

            return Expression.Condition(
                Expression.TypeIs(input, typeof(EntityReference)),  //If input is an entity reference, compare the Guid against the Id property
                Expression.Convert(
                    getIdFromEntityReferenceExpr,
                    typeof(Guid)),
                Expression.Condition(Expression.TypeIs(input, typeof(Guid)),  //If any other case, then just compare it as a Guid directly
                    Expression.Convert(input, typeof(Guid)),
                    Expression.Constant(Guid.Empty, typeof(Guid))));

        }

        protected static Expression GetAppropiateCastExpressionBasedOnDecimal(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(Money)),
                                Expression.Convert(
                                    Expression.Call(Expression.TypeAs(input, typeof(Money)),
                                            typeof(Money).GetMethod("get_Value")),
                                            typeof(decimal)),
                           Expression.Condition(Expression.TypeIs(input, typeof(decimal)),
                                        Expression.Convert(input, typeof(decimal)),
                                        Expression.Constant(0.0M)));

        }

        protected static Expression GetAppropiateCastExpressionBasedOnBoolean(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(BooleanManagedProperty)),
                                Expression.Convert(
                                    Expression.Call(Expression.TypeAs(input, typeof(BooleanManagedProperty)),
                                            typeof(BooleanManagedProperty).GetMethod("get_Value")),
                                            typeof(bool)),
                           Expression.Condition(Expression.TypeIs(input, typeof(bool)),
                                        Expression.Convert(input, typeof(bool)),
                                        Expression.Constant(false)));

        }

        protected static Expression GetAppropiateCastExpressionBasedOnInt(Expression input)
        {
            return Expression.Condition(
                        Expression.TypeIs(input, typeof(OptionSetValue)),
                                            Expression.Convert(
                                                Expression.Call(Expression.TypeAs(input, typeof(OptionSetValue)),
                                                        typeof(OptionSetValue).GetMethod("get_Value")),
                                                        typeof(int)),
                                                    Expression.Convert(input, typeof(int)));
        }

        protected static Expression GetAppropiateCastExpressionBasedOnOptionSetValueCollection(Expression input)
        {
            return Expression.Call(typeof(XrmFakedContext).GetMethod("ConvertToHashSetOfInt"), input, Expression.Constant(true));
        }

#if FAKE_XRM_EASY_9
        public static HashSet<int> ConvertToHashSetOfInt(object input, bool isOptionSetValueCollectionAccepted)
        {
            var set = new HashSet<int>();

            var faultReason = $"The formatter threw an exception while trying to deserialize the message: There was an error while trying to deserialize parameter" +
                $" http://schemas.microsoft.com/xrm/2011/Contracts/Services:query. The InnerException message was 'Error in line 1 position 8295. Element " +
                $"'http://schemas.microsoft.com/2003/10/Serialization/Arrays:anyType' contains data from a type that maps to the name " +
                $"'http://schemas.microsoft.com/xrm/2011/Contracts:{input?.GetType()}'. The deserializer has no knowledge of any type that maps to this name. " +
                $"Consider changing the implementation of the ResolveName method on your DataContractResolver to return a non-null value for name " +
                $"'{input?.GetType()}' and namespace 'http://schemas.microsoft.com/xrm/2011/Contracts'.'.  Please see InnerException for more details.";

            if (input is int)
            {
                set.Add((int)input);
            }
            else if (input is string)
            {
                set.Add(int.Parse(input as string));
            }
            else if (input is int[])
            {
                set.UnionWith(input as int[]);
            }
            else if (input is string[])
            {
                set.UnionWith((input as string[]).Select(s => int.Parse(s)));
            }
            else if (input is DataCollection<object>)
            {
                var collection = input as DataCollection<object>;

                if (collection.All(o => o is int))
                {
                    set.UnionWith(collection.Cast<int>());
                }
                else if (collection.All(o => o is string))
                {
                    set.UnionWith(collection.Select(o => int.Parse(o as string)));
                }
                else if (collection.Count == 1 && collection[0] is int[])
                {
                    set.UnionWith(collection[0] as int[]);
                }
                else if (collection.Count == 1 && collection[0] is string[])
                {
                    set.UnionWith((collection[0] as string[]).Select(s => int.Parse(s)));
                }
                else
                {
                    throw new FaultException(new FaultReason(faultReason));
                }
            }
            else if (isOptionSetValueCollectionAccepted && input is OptionSetValueCollection)
            {
                set.UnionWith((input as OptionSetValueCollection).Select(osv => osv.Value));
            }
            else
            {
                throw new FaultException(new FaultReason(faultReason));
            }

            return set;
        }
#endif

        protected static Expression TransformExpressionGetDateOnlyPart(Expression input)
        {
            return Expression.Call(input, typeof(DateTime).GetMethod("get_Date"));
        }

        protected static Expression TransformExpressionValueBasedOnOperator(ConditionOperator op, Expression input)
        {
            switch (op)
            {
                case ConditionOperator.Today:
                case ConditionOperator.Yesterday:
                case ConditionOperator.Tomorrow:
                case ConditionOperator.On:
                case ConditionOperator.OnOrAfter:
                case ConditionOperator.OnOrBefore:
                    return TransformExpressionGetDateOnlyPart(input);

                default:
                    return input; //No transformation
            }
        }

        protected static Expression TranslateConditionExpressionEqual(XrmFakedContext context, TypedConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            object unaryOperatorValue = null;

            switch (c.CondExpression.Operator)
            {
                case ConditionOperator.Today:
                    unaryOperatorValue = DateTime.Today;
                    break;
                case ConditionOperator.Yesterday:
                    unaryOperatorValue = DateTime.Today.AddDays(-1);
                    break;
                case ConditionOperator.Tomorrow:
                    unaryOperatorValue = DateTime.Today.AddDays(1);
                    break;
                case ConditionOperator.EqualUserId:
                case ConditionOperator.NotEqualUserId:
                    unaryOperatorValue = context.CallerId.Id;
                    break;

                case ConditionOperator.EqualBusinessId:
                case ConditionOperator.NotEqualBusinessId:
                    unaryOperatorValue = context.BusinessUnitId.Id;
                    break;
            }

            if (unaryOperatorValue != null)
            {
                //c.Values empty in this case
                var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(c.AttributeType, getAttributeValueExpr, unaryOperatorValue);
                var transformedExpression = TransformExpressionValueBasedOnOperator(c.CondExpression.Operator, leftHandSideExpression);

                expOrValues = Expression.Equal(transformedExpression,
                                GetAppropiateTypedValueAndType(unaryOperatorValue, c.AttributeType));
            }
#if FAKE_XRM_EASY_9
            else if (c.AttributeType == typeof(OptionSetValueCollection))
            {
                var conditionValue = GetSingleConditionValue(c);

                var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(c.AttributeType, getAttributeValueExpr, conditionValue);
                var rightHandSideExpression = Expression.Constant(ConvertToHashSetOfInt(conditionValue, isOptionSetValueCollectionAccepted: false));

                expOrValues = Expression.Equal(
                    Expression.Call(leftHandSideExpression, typeof(HashSet<int>).GetMethod("SetEquals"), rightHandSideExpression),
                    Expression.Constant(true));
            }
#endif
            else
            {
                foreach (object value in c.CondExpression.Values)
                {
                    var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(c.AttributeType, getAttributeValueExpr, value);
                    var transformedExpression = TransformExpressionValueBasedOnOperator(c.CondExpression.Operator, leftHandSideExpression);

                    expOrValues = Expression.Or(expOrValues, Expression.Equal(
                                transformedExpression,
                                TransformExpressionValueBasedOnOperator(c.CondExpression.Operator, GetAppropiateTypedValueAndType(value, c.AttributeType))));


                }
            }

            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        private static object GetSingleConditionValue(TypedConditionExpression c)
        {
            if (c.CondExpression.Values.Count != 1)
            {
                OrganizationServiceFaultInvalidArgument.Throw($"The {c.CondExpression.Operator} requires 1 value/s, not {c.CondExpression.Values.Count}.Parameter name: {c.CondExpression.AttributeName}");
            }

            var conditionValue = c.CondExpression.Values.Single();

            if (!(conditionValue is string) && conditionValue is IEnumerable)
            {
                var conditionValueEnumerable = conditionValue as IEnumerable;
                var count = 0;

                foreach (var obj in conditionValueEnumerable)
                {
                    count++;
                    conditionValue = obj;
                }

                if (count != 1)
                {
                    OrganizationServiceFaultInvalidArgument.Throw($"The {c.CondExpression.Operator} requires 1 value/s, not {count}.Parameter name: {c.CondExpression.AttributeName}");
                }
            }

            return conditionValue;
        }

        protected static Expression TranslateConditionExpressionIn(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));

#if FAKE_XRM_EASY_9
            if (tc.AttributeType == typeof(OptionSetValueCollection))
            {
                var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, null);
                var rightHandSideExpression = Expression.Constant(ConvertToHashSetOfInt(c.Values, isOptionSetValueCollectionAccepted: false));

                expOrValues = Expression.Equal(
                    Expression.Call(leftHandSideExpression, typeof(HashSet<int>).GetMethod("SetEquals"), rightHandSideExpression),
                    Expression.Constant(true));
            }
            else
#endif
            {
                foreach (object value in c.Values)
                {
                    if (value is Array)
                    {
                        foreach (var a in ((Array)value))
                        {
                            expOrValues = Expression.Or(expOrValues, Expression.Equal(
                                GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, a),
                                GetAppropiateTypedValueAndType(a, tc.AttributeType)));
                        }
                    }
                    else
                    {
                        expOrValues = Expression.Or(expOrValues, Expression.Equal(
                                    GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value),
                                    GetAppropiateTypedValueAndType(value, tc.AttributeType)));
                    }
                }
            }

            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        //protected static Expression TranslateConditionExpressionOn(ConditionExpression c, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        //{
        //    BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
        //    foreach (object value in c.Values)
        //    {

        //        expOrValues = Expression.Or(expOrValues, Expression.Equal(
        //                    GetAppropiateCastExpressionBasedOnValue(getAttributeValueExpr, value),
        //                    GetAppropiateTypedValue(value)));


        //    }
        //    return Expression.AndAlso(
        //                    containsAttributeExpr,
        //                    Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
        //                        expOrValues));
        //}

        protected static Expression TranslateConditionExpressionGreaterThanOrEqual(XrmFakedContext context, TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            //var c = tc.CondExpression;

            return Expression.Or(
                                TranslateConditionExpressionEqual(context, tc, getAttributeValueExpr, containsAttributeExpr),
                                TranslateConditionExpressionGreaterThan(tc, getAttributeValueExpr, containsAttributeExpr));

        }
        protected static Expression TranslateConditionExpressionGreaterThan(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            if (c.Values.Count(v => v != null) != 1)
            {
                throw new FaultException(new FaultReason($"The ConditonOperator.{c.Operator} requires 1 value/s, not {c.Values.Count(v => v != null)}. Parameter Name: {c.AttributeName}"));
            }

            if (tc.AttributeType == typeof(string))
            {
                return TranslateConditionExpressionGreaterThanString(tc, getAttributeValueExpr, containsAttributeExpr);
            }
            else if (GetAppropiateTypeForValue(c.Values[0]) == typeof(string))
            {
                return TranslateConditionExpressionGreaterThanString(tc, getAttributeValueExpr, containsAttributeExpr);
            }
            else
            {
                BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
                foreach (object value in c.Values)
                {
                    var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value);
                    var transformedExpression = TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, leftHandSideExpression);

                    expOrValues = Expression.Or(expOrValues,
                            Expression.GreaterThan(
                                transformedExpression,
                                TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, GetAppropiateTypedValueAndType(value, tc.AttributeType))));
                }
                return Expression.AndAlso(
                                containsAttributeExpr,
                                Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                    expOrValues));
            }

        }

        protected static Expression TranslateConditionExpressionGreaterThanString(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value);
                var transformedExpression = TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, leftHandSideExpression);

                var left = transformedExpression;
                var right = TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, GetAppropiateTypedValueAndType(value, tc.AttributeType));

                var methodCallExpr = GetCompareToExpression<string>(left, right);

                expOrValues = Expression.Or(expOrValues,
                        Expression.GreaterThan(
                            methodCallExpr,
                            Expression.Constant(0)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionLessThanOrEqual(XrmFakedContext context, TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            //var c = tc.CondExpression;

            return Expression.Or(
                                TranslateConditionExpressionEqual(context, tc, getAttributeValueExpr, containsAttributeExpr),
                                TranslateConditionExpressionLessThan(tc, getAttributeValueExpr, containsAttributeExpr));

        }

        protected static Expression GetCompareToExpression<T>(Expression left, Expression right)
        {
            return Expression.Call(left, typeof(T).GetMethod("CompareTo", new Type[] { typeof(string) }), new[] { right });
        }

        protected static Expression TranslateConditionExpressionLessThanString(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            foreach (object value in c.Values)
            {
                var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value);
                var transformedLeftHandSideExpression = TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, leftHandSideExpression);

                var rightHandSideExpression = TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, GetAppropiateTypedValueAndType(value, tc.AttributeType));

                //var compareToMethodCall = Expression.Call(transformedLeftHandSideExpression, typeof(string).GetMethod("CompareTo", new Type[] { typeof(string) }), new[] { rightHandSideExpression });
                var compareToMethodCall = GetCompareToExpression<string>(transformedLeftHandSideExpression, rightHandSideExpression);

                expOrValues = Expression.Or(expOrValues,
                        Expression.LessThan(compareToMethodCall, Expression.Constant(0)));
            }
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                expOrValues));
        }

        protected static Expression TranslateConditionExpressionLessThan(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            if (c.Values.Count(v => v != null) != 1)
            {
                throw new FaultException(new FaultReason($"The ConditonOperator.{c.Operator} requires 1 value/s, not {c.Values.Count(v => v != null)}. Parameter Name: {c.AttributeName}"));
            }

            if (tc.AttributeType == typeof(string))
            {
                return TranslateConditionExpressionLessThanString(tc, getAttributeValueExpr, containsAttributeExpr);
            }
            else if (GetAppropiateTypeForValue(c.Values[0]) == typeof(string))
            {
                return TranslateConditionExpressionLessThanString(tc, getAttributeValueExpr, containsAttributeExpr);
            }
            else
            {
                BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
                foreach (object value in c.Values)
                {
                    var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value);
                    var transformedExpression = TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, leftHandSideExpression);

                    expOrValues = Expression.Or(expOrValues,
                            Expression.LessThan(
                                transformedExpression,
                                TransformExpressionValueBasedOnOperator(tc.CondExpression.Operator, GetAppropiateTypedValueAndType(value, tc.AttributeType))));
                }
                return Expression.AndAlso(
                                containsAttributeExpr,
                                Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                    expOrValues));
            }

        }

        protected static Expression TranslateConditionExpressionLast(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            var beforeDateTime = default(DateTime);
            var currentDateTime = DateTime.UtcNow;

            switch (c.Operator)
            {
                case ConditionOperator.Last7Days:
                    beforeDateTime = currentDateTime.AddDays(-7);
                    break;
            }

            c.Values.Add(beforeDateTime);
            c.Values.Add(currentDateTime);

            return TranslateConditionExpressionBetween(tc, getAttributeValueExpr, containsAttributeExpr);
        }

        protected static Expression TranslateConditionExpressionBetweenDates(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            DateTime? startDate = null;
            DateTime? endDate = null;

            var today = DateTime.Today;
            var thisYear = today.Year;
            var thisMonth = today.Month;


            switch (c.Operator)
            {
                case ConditionOperator.ThisYear:
                    // From first day of this year to last day of this year
                    startDate = new DateTime(thisYear, 1, 1);
                    endDate = new DateTime(thisYear, 12, 31);
                    break;                
                case ConditionOperator.LastYear:
                    // From first day of last year to last day of last year
                    startDate = new DateTime(thisYear - 1, 1, 1);
                    endDate = new DateTime(thisYear - 1, 12, 31);
                    break;
                case ConditionOperator.NextYear:
                    // From first day of next year to last day of next year
                    startDate = new DateTime(thisYear + 1, 1, 1);
                    endDate = new DateTime(thisYear + 1, 12, 31);
                    break;
                case ConditionOperator.ThisMonth:
                    // From first day of this month to last day of this month
                    startDate = new DateTime(thisYear, thisMonth, 1);
                    // Add one month to the first of this month, and then remove one day
                    endDate = new DateTime(thisYear, thisMonth, 1).AddMonths(1).AddDays(-1);
                    break;
            }

            c.Values.Add(startDate);
            c.Values.Add(endDate);

            return TranslateConditionExpressionBetween(tc, getAttributeValueExpr, containsAttributeExpr);
        }

        protected static Expression TranslateConditionExpressionBetween(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            object value1, value2;
            value1 = c.Values[0];
            value2 = c.Values[1];

            //Between the range... 
            var exp = Expression.And(
                Expression.GreaterThanOrEqual(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value1),
                            GetAppropiateTypedValueAndType(value1, tc.AttributeType)),

                Expression.LessThanOrEqual(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, value2),
                            GetAppropiateTypedValueAndType(value2, tc.AttributeType)));


            //and... attribute exists too
            return Expression.AndAlso(
                            containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                exp));
        }

        protected static Expression TranslateConditionExpressionNull(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            return Expression.Or(Expression.AndAlso(
                                    containsAttributeExpr,
                                    Expression.Equal(
                                    getAttributeValueExpr,
                                    Expression.Constant(null))),   //Attribute is null
                                 Expression.AndAlso(
                                    Expression.Not(containsAttributeExpr),
                                    Expression.Constant(true)));   //Or attribute is not defined (null)
        }

        protected static Expression TranslateConditionExpressionOlderThan(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr, DateTime olderThanDate)
        {
            var lessThanExpression = Expression.LessThan(
                            GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, olderThanDate),
                            GetAppropiateTypedValueAndType(olderThanDate, tc.AttributeType));

            return Expression.AndAlso(containsAttributeExpr,
                            Expression.AndAlso(Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                                lessThanExpression));
        }

        protected static Expression TranslateConditionExpressionEndsWith(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString()).ToList());
            var typedComputedCondition = new TypedConditionExpression(computedCondition);
            typedComputedCondition.AttributeType = tc.AttributeType;

            return TranslateConditionExpressionLike(typedComputedCondition, getAttributeValueExpr, containsAttributeExpr);
        }

        protected static Expression GetToStringExpression<T>(Expression e)
        {
            return Expression.Call(e, typeof(T).GetMethod("ToString", new Type[] { }));
        }
        protected static Expression GetCaseInsensitiveExpression(Expression e)
        {
            return Expression.Call(e,
                                typeof(string).GetMethod("ToLowerInvariant", new Type[] { }));
        }

        protected static Expression TranslateConditionExpressionLike(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            BinaryExpression expOrValues = Expression.Or(Expression.Constant(false), Expression.Constant(false));
            Expression convertedValueToStr = Expression.Convert(GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, c.Values[0]), typeof(string));

            Expression convertedValueToStrAndToLower = GetCaseInsensitiveExpression(convertedValueToStr);

            string sLikeOperator = "%";
            foreach (object value in c.Values)
            {
                var strValue = value.ToString();
                string sMethod = "";

                if (strValue.EndsWith(sLikeOperator) && strValue.StartsWith(sLikeOperator))
                    sMethod = "Contains";

                else if (strValue.StartsWith(sLikeOperator))
                    sMethod = "EndsWith";

                else
                    sMethod = "StartsWith";

                expOrValues = Expression.Or(expOrValues, Expression.Call(
                    convertedValueToStrAndToLower,
                    typeof(string).GetMethod(sMethod, new Type[] { typeof(string) }),
                    Expression.Constant(value.ToString().ToLowerInvariant().Replace("%", "")) //Linq2CRM adds the percentage value to be executed as a LIKE operator, here we are replacing it to just use the appropiate method
                ));
            }

            return Expression.AndAlso(
                            containsAttributeExpr,
                            expOrValues);
        }

        protected static Expression TranslateConditionExpressionContains(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString() + "%").ToList());
            var computedTypedCondition = new TypedConditionExpression(computedCondition);
            computedTypedCondition.AttributeType = tc.AttributeType;

            return TranslateConditionExpressionLike(computedTypedCondition, getAttributeValueExpr, containsAttributeExpr);

        }

        protected static BinaryExpression TranslateMultipleConditionExpressions(QueryExpression qe, XrmFakedContext context, string sEntityName, List<ConditionExpression> conditions, LogicalOperator op, ParameterExpression entity, bool bIsOuter)
        {
            BinaryExpression binaryExpression = null;  //Default initialisation depending on logical operator
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var c in conditions)
            {
                //Create a new typed expression 
                var typedExpression = new TypedConditionExpression(c);
                typedExpression.IsOuter = bIsOuter;

                string sAttributeName = c.AttributeName;

                //Find the attribute type if using early bound entities
                if (context.ProxyTypesAssembly != null)
                {

#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
                    if (c.EntityName != null)
                        sEntityName = qe.GetEntityNameFromAlias(c.EntityName);
                    else
                    {
                        if (c.AttributeName.IndexOf(".") >= 0)
                        {
                            var alias = c.AttributeName.Split('.')[0];
                            sEntityName = qe.GetEntityNameFromAlias(alias);
                            sAttributeName = c.AttributeName.Split('.')[1];
                        }
                        else
                        {
                            sEntityName = qe.EntityName; //Attributes from the root entity
                        }
                    }

#else
                    //CRM 2011
                    if (c.AttributeName.IndexOf(".") >= 0) {
                        var alias = c.AttributeName.Split('.')[0];
                        sEntityName = qe.GetEntityNameFromAlias(alias);
                        sAttributeName = c.AttributeName.Split('.')[1];
                    }
#endif

                    var earlyBoundType = context.FindReflectedType(sEntityName);
                    if (earlyBoundType != null)
                    {
                        typedExpression.AttributeType = context.FindReflectedAttributeType(earlyBoundType, sEntityName, sAttributeName);

                        // Special case when filtering on the name of a Lookup
                        if (typedExpression.AttributeType == typeof(EntityReference) && sAttributeName.EndsWith("name"))
                        {
                            var realAttributeName = c.AttributeName.Substring(0, c.AttributeName.Length - 4);

                            if (GetEarlyBoundTypeAttribute(earlyBoundType, sAttributeName) == null && GetEarlyBoundTypeAttribute(earlyBoundType, realAttributeName) != null && GetEarlyBoundTypeAttribute(earlyBoundType, realAttributeName).PropertyType == typeof(EntityReference))
                            {
                                // Need to make Lookups work against the real attribute, not the "name" suffixed attribute that doesn't exist
                                c.AttributeName = realAttributeName;
                            }
                        }
                    }
                }

                ValidateSupportedTypedExpression(typedExpression);

                //Build a binary expression  
                if (op == LogicalOperator.And)
                {
                    binaryExpression = Expression.And(binaryExpression, TranslateConditionExpression(qe, context, typedExpression, entity));
                }
                else
                    binaryExpression = Expression.Or(binaryExpression, TranslateConditionExpression(qe, context, typedExpression, entity));
            }

            return binaryExpression;
        }

        protected static BinaryExpression TranslateMultipleFilterExpressions(QueryExpression qe, XrmFakedContext context, string sEntityName, List<FilterExpression> filters, LogicalOperator op, ParameterExpression entity, bool bIsOuter)
        {
            BinaryExpression binaryExpression = null;
            if (op == LogicalOperator.And)
                binaryExpression = Expression.And(Expression.Constant(true), Expression.Constant(true));
            else
                binaryExpression = Expression.Or(Expression.Constant(false), Expression.Constant(false));

            foreach (var f in filters)
            {
                var thisFilterLambda = TranslateFilterExpressionToExpression(qe, context, sEntityName, f, entity, bIsOuter);

                //Build a binary expression  
                if (op == LogicalOperator.And)
                {
                    binaryExpression = Expression.And(binaryExpression, thisFilterLambda);
                }
                else
                    binaryExpression = Expression.Or(binaryExpression, thisFilterLambda);
            }

            return binaryExpression;
        }

        protected static List<Expression> TranslateLinkedEntityFilterExpressionToExpression(QueryExpression qe, XrmFakedContext context, LinkEntity le, ParameterExpression entity)
        {
            //In CRM 2011, condition expressions are at the LinkEntity level without an entity name
            //From CRM 2013, condition expressions were moved to outside the LinkEntity object at the QueryExpression level,
            //with an EntityName alias attribute

            //If we reach this point, it means we are translating filters at the Link Entity level (2011),
            //Therefore we need to prepend the alias attribute because the code to generate attributes for Joins (JoinAttribute extension) is common across versions
            var linkedEntitiesQueryExpressions = new List<Expression>();

            if (le.LinkCriteria != null)
            {
                var earlyBoundType = context.FindReflectedType(le.LinkToEntityName);
                var attributeMetadata = context.AttributeMetadataNames.ContainsKey(le.LinkToEntityName) ? context.AttributeMetadataNames[le.LinkToEntityName] : null;

                foreach (var ce in le.LinkCriteria.Conditions)
                {
                    if (earlyBoundType != null)
                    {
                        var attributeInfo = GetEarlyBoundTypeAttribute(earlyBoundType, ce.AttributeName);
                        if (attributeInfo == null && ce.AttributeName.EndsWith("name"))
                        {
                            // Special case for referencing the name of a EntityReference
                            var sAttributeName = ce.AttributeName.Substring(0, ce.AttributeName.Length - 4);
                            attributeInfo = GetEarlyBoundTypeAttribute(earlyBoundType, sAttributeName);

                            if (attributeInfo.PropertyType == typeof(EntityReference))
                            {
                                // Don't mess up if other attributes follow this naming pattern
                                ce.AttributeName = sAttributeName;
                            }
                        }
                    }
                    else if (attributeMetadata != null && !attributeMetadata.ContainsKey(ce.AttributeName) && ce.AttributeName.EndsWith("name"))
                    {
                        // Special case for referencing the name of a EntityReference
                        var sAttributeName = ce.AttributeName.Substring(0, ce.AttributeName.Length - 4);
                        if (attributeMetadata.ContainsKey(sAttributeName))
                        {
                            ce.AttributeName = sAttributeName;
                        }
                    }

                    var entityAlias = !string.IsNullOrEmpty(le.EntityAlias) ? le.EntityAlias : le.LinkToEntityName;
                    ce.AttributeName = entityAlias + "." + ce.AttributeName;
                }

                foreach (var fe in le.LinkCriteria.Filters)
                {
                    foreach (var ce in fe.Conditions)
                    {
                        var entityAlias = !string.IsNullOrEmpty(le.EntityAlias) ? le.EntityAlias : le.LinkToEntityName;
                        ce.AttributeName = entityAlias + "." + ce.AttributeName;
                    }
                }
            }

            //Translate this specific Link Criteria
            linkedEntitiesQueryExpressions.Add(TranslateFilterExpressionToExpression(qe, context, le.LinkToEntityName, le.LinkCriteria, entity, le.JoinOperator == JoinOperator.LeftOuter));

            //Processed nested linked entities
            foreach (var nestedLinkedEntity in le.LinkEntities)
            {
                var listOfExpressions = TranslateLinkedEntityFilterExpressionToExpression(qe, context, nestedLinkedEntity, entity);
                linkedEntitiesQueryExpressions.AddRange(listOfExpressions);
            }

            return linkedEntitiesQueryExpressions;
        }

        protected static Expression TranslateQueryExpressionFiltersToExpression(XrmFakedContext context, QueryExpression qe, ParameterExpression entity)
        {
            var linkedEntitiesQueryExpressions = new List<Expression>();
            foreach (var le in qe.LinkEntities)
            {
                var listOfExpressions = TranslateLinkedEntityFilterExpressionToExpression(qe, context, le, entity);
                linkedEntitiesQueryExpressions.AddRange(listOfExpressions);
            }

            if (linkedEntitiesQueryExpressions.Count > 0 && qe.Criteria != null)
            {
                //Return the and of the two
                Expression andExpression = Expression.Constant(true);
                foreach (var e in linkedEntitiesQueryExpressions)
                {
                    andExpression = Expression.And(e, andExpression);

                }
                var feExpression = TranslateFilterExpressionToExpression(qe, context, qe.EntityName, qe.Criteria, entity, false);
                return Expression.And(andExpression, feExpression);
            }
            else if (linkedEntitiesQueryExpressions.Count > 0)
            {
                //Linked entity expressions only
                Expression andExpression = Expression.Constant(true);
                foreach (var e in linkedEntitiesQueryExpressions)
                {
                    andExpression = Expression.And(e, andExpression);

                }
                return andExpression;
            }
            else
            {
                //Criteria only
                return TranslateFilterExpressionToExpression(qe, context, qe.EntityName, qe.Criteria, entity, false);
            }
        }
        protected static Expression TranslateFilterExpressionToExpression(QueryExpression qe, XrmFakedContext context, string sEntityName, FilterExpression fe, ParameterExpression entity, bool bIsOuter)
        {
            if (fe == null) return Expression.Constant(true);

            BinaryExpression conditionsLambda = null;
            BinaryExpression filtersLambda = null;
            if (fe.Conditions != null && fe.Conditions.Count > 0)
            {
                conditionsLambda = TranslateMultipleConditionExpressions(qe, context, sEntityName, fe.Conditions.ToList(), fe.FilterOperator, entity, bIsOuter);
            }

            //Process nested filters recursively
            if (fe.Filters != null && fe.Filters.Count > 0)
            {
                filtersLambda = TranslateMultipleFilterExpressions(qe, context, sEntityName, fe.Filters.ToList(), fe.FilterOperator, entity, bIsOuter);
            }

            if (conditionsLambda != null && filtersLambda != null)
            {
                //Satisfy both
                if (fe.FilterOperator == LogicalOperator.And)
                {
                    return Expression.And(conditionsLambda, filtersLambda);
                }
                else
                {
                    return Expression.Or(conditionsLambda, filtersLambda);
                }
            }
            else if (conditionsLambda != null)
                return conditionsLambda;
            else if (filtersLambda != null)
                return filtersLambda;

            return Expression.Constant(true); //Satisfy filter if there are no conditions nor filters
        }
        protected static Expression TranslateConditionExpressionNext(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            var nextDateTime = default(DateTime);
            var currentDateTime = DateTime.UtcNow;
            var numberOfWeeks = (int)c.Values[0];

            switch (c.Operator)
            {
                case ConditionOperator.NextXWeeks:
                    nextDateTime = currentDateTime.AddDays(7 * numberOfWeeks);
                    break;
            }

            c.Values[0] = (currentDateTime);
            c.Values.Add(nextDateTime);
            // Jordi don't think this is needed - tests work without it and not used in the TranslateConditionExpressionBetween method
            // c.Values.Add(numberOfWeeks);

            return TranslateConditionExpressionBetween(tc, getAttributeValueExpr, containsAttributeExpr);
        }

#if FAKE_XRM_EASY_9
        protected static Expression TranslateConditionExpressionContainValues(TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var leftHandSideExpression = GetAppropiateCastExpressionBasedOnType(tc.AttributeType, getAttributeValueExpr, null);
            var rightHandSideExpression = Expression.Constant(ConvertToHashSetOfInt(tc.CondExpression.Values, isOptionSetValueCollectionAccepted: false));

            return Expression.AndAlso(
                       containsAttributeExpr,
                       Expression.AndAlso(
                           Expression.NotEqual(getAttributeValueExpr, Expression.Constant(null)),
                           Expression.Equal(
                               Expression.Call(leftHandSideExpression, typeof(HashSet<int>).GetMethod("Overlaps"), rightHandSideExpression),
                               Expression.Constant(true))));
        }
#endif
    }
}