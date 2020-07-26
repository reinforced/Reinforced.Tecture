using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation
{
    internal static class Convert
    {
        public static PreparedSqlQuery Query(PreparedQuery q, Func<Type, bool> isEntity)
        {
            List<SqlQueryExpression> positioned = new List<SqlQueryExpression>();
            var visitor = new StrokeVisitor(q.InitialTableReferences, isEntity);

            foreach (var pe in q.Arguments)
            {
                visitor.Visit(pe.Expression);
                var sql = visitor.Retrieve();
                sql.IsTop = true;
                sql.Position = pe.Position;
                sql.Index = pe.Index;
                positioned.Add(sql);
            }

            return new PreparedSqlQuery(q.QueryStructure, positioned.ToArray());
        }
    }
}
