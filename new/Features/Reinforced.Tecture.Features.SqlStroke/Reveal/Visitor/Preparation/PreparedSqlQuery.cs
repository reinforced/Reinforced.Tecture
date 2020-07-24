namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation
{
    class PreparedSqlQuery
    {
        public string QueryStructure { get; private set; }

        public PositionedSqlExpression[] Arguments { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PreparedSqlQuery(string queryStructure, PositionedSqlExpression[] arguments)
        {
            QueryStructure = queryStructure;
            Arguments = arguments;
        }
    }
}