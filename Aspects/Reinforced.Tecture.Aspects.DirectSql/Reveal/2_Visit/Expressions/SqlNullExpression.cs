// ReSharper disable CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// Expression denoting NULL
    /// </summary>
    public class SqlNullExpression : SqlQueryExpression
    {
        internal SqlNullExpression() { }
    }

    /// <summary>
    /// Expression denoting bool
    /// </summary>
    public class SqlBooleanExpression : SqlQueryExpression
    {
        internal SqlBooleanExpression(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets value of expression
        /// </summary>
        public bool Value { get; internal set; }
    }
}
