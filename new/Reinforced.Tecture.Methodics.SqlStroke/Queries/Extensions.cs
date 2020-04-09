using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.SqlStroke.Commands;

namespace Reinforced.Tecture.Methodics.SqlStroke.Queries
{
    public static partial class Extensions
    {
        private static RawQuery QueryCore(this ISqlSource s, LambdaExpression expr, params Type[] usedTypes)
        {
            var rt = s.GetStrokeRuntime(usedTypes);
            var p = rt.GetProcessor().RevealQuery(expr);
            var cmd = new Sql(p.CommandText, p.CommandParameters);
            return new RawQuery(cmd, rt);
        }
    }
}
