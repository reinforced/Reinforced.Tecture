using System.Linq.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation
{
    struct PositionedExpression
    {
        public int Index { get; set; }
        public int Position { get; set; }
        public Expression Expression { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PositionedExpression(int index, int position, Expression expression)
        {
            Index = index;
            Position = position;
            Expression = expression;
        }
    }

    struct PositionedSqlExpression
    {
        public int Index { get; set; }
        public int Position { get; set; }
        public SqlQueryExpression Expression { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PositionedSqlExpression(int index, int position, SqlQueryExpression expression)
        {
            Index = index;
            Position = position;
            Expression = expression;
        }
    }
}