using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Parse
{
    struct PositionedExpression
    {
        public int Index { get; private set; }
        public int Position { get; private set; }
        public Expression Expression { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        internal PositionedExpression(int index, int position, Expression expression)
        {
            Index = index;
            Position = position;
            Expression = expression;
        }
    }
}