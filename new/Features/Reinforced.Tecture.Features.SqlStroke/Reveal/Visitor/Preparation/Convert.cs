using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation
{
    internal static class Convert
    {
        public static PreparedSqlQuery Query(PreparedQuery q, Func<Type, bool> isEntity)
        {
            List<PositionedSqlExpression> positioned = new List<PositionedSqlExpression>();
            var visitor = new StrokeVisitor(q.InitialTableReferences, isEntity);

            foreach (var pe in q.Arguments)
            {
                visitor.Visit(pe.Expression);
                var sql = visitor.Retrieve();
                sql.IsTop = true;
                positioned.Add(new PositionedSqlExpression(pe.Index, pe.Position, sql));
            }

            return new PreparedSqlQuery(q.QueryStructure, positioned.ToArray());
        }
    }
}
