using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate
{
    /// <summary>
    /// Interpolates all accesses to the tables/columns within the query
    /// </summary>
    public class SchemaInterpolator
    {
        protected readonly IMapper Mapper;

        public SchemaInterpolator(IMapper mapper)
        {
            Mapper = mapper;
        }

        internal SchemaInterpolatedQuery Proceed(LanguageInterpolatedQuery query)
        {
            List<string> formatString = new List<string>();
            List<object> actualParameters = new List<object>();
            foreach (var p in query.Parameters)
            {
                if (p is SqlTableReference tableRef)
                {
                    formatString.Add(VisitTableReference(tableRef));
                    continue;
                }

                if (p is SqlColumnReference colRef)
                {
                    formatString.Add(VisitColumnReference(colRef));
                    continue;
                }

                formatString.Add($"{{{actualParameters.Count}}}");
                actualParameters.Add(p);
            }

            var queryString = string.Format(query.Query, formatString.ToArray());
            return new SchemaInterpolatedQuery(queryString, actualParameters.ToArray(), query.UsedTypes);
        }

        protected virtual string VisitTableReference(SqlTableReference expr)
        {
            return VisitTableReference(expr.Table, expr.ChildrenJoinedAs, expr.AsAlias);
        }

        protected virtual string VisitTableReference(TableReference tref, Join joinType, bool notExpand)
        {
            if (notExpand) return TableAlias(tref);

            StringBuilder result = new StringBuilder();
            result.Append(TableMakeAlias(tref));
            VisitChildrenReferences(tref, joinType, result);
            return result.ToString();
        }

        private void VisitChildrenReferences(TableReference tref, Join joinType, StringBuilder result)
        {
            foreach (var ntr in tref.Children)
            {
                result.AppendLine();
                result.AppendFormat(" {0} JOIN {1} ON ", JoinToString(joinType), TableMakeAlias(ntr));
                var fields = Mapper.GetJoinKeys(tref.EntityType, ntr.JoinColumn);
                var first = true;
                foreach (var assocField in fields)
                {
                    if (!first) result.Append(" AND ");
                    first = false;

                    result.Append(VisitColumnReference(ntr, assocField.From));
                    result.Append(" = ");
                    result.Append(VisitColumnReference(ntr.Parent, assocField.To));
                }

                if (ntr.Children.Count > 0) VisitChildrenReferences(ntr, joinType, result);
            }

            result.AppendLine();
        }

        protected virtual string TableMakeAlias(TableReference tr)
        {
            if (string.IsNullOrEmpty(tr.Alias)) return $"[{Mapper.GetTableName(tr.EntityType)}]";
            return $"[{Mapper.GetTableName(tr.EntityType)}] [{tr.Alias}]";
        }
        protected virtual string TableAlias(TableReference tr)
        {
            if (string.IsNullOrEmpty(tr.Alias)) return $"[{Mapper.GetTableName(tr.EntityType)}]";
            return $"[{tr.Alias}]";
        }

        protected virtual string VisitColumnReference(SqlColumnReference expr)
        {
            return VisitColumnReference(expr.Table, Mapper.GetColumnName(expr.Table.EntityType, expr.Column));
        }

        protected virtual string VisitColumnReference(TableReference tr, string colName)
        {
            return $"{TableAlias(tr)}.[{colName}]";
        }

        protected virtual string JoinToString(Join join)
        {
            switch (join)
            {
                case Join.Cross: return "CROSS";
                case Join.Inner: return "INNER";
                case Join.Left: return "LEFT";
                case Join.Right: return "RIGHT";
                case Join.Outer: return "OUTER";
                case Join.Default: return string.Empty;
            }

            throw new Exception("Unknown join type: " + join);
        }

    }
}
