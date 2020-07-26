using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
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

        public bool Value { get; set; }
    }
}
