// ReSharper disable CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// Object parameter reference
    /// </summary>
    public class SqlObjectParameter : SqlQueryExpression
    {
        internal SqlObjectParameter() { }

        /// <summary>
        /// Gets parameter value
        /// </summary>
        public object Parameter { get; set; }
        
    }
}