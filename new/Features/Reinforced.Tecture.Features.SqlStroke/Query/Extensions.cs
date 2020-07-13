using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.SqlStroke.Command;

namespace Reinforced.Tecture.Features.SqlStroke.Query
{
    public static partial class Extensions
    {
        private static RawQuery QueryCore(this Read<QueryChannel<DirectSql>> s, LambdaExpression expr, params Type[] usedTypes)
        {
            var rt = s.Feature();
            var p = rt.GetProcessor(usedTypes).RevealQuery(expr);
            var cmd = new Sql(p.CommandText, p.CommandParameters);
            return new RawQuery(cmd, rt);
        }
    }
}
