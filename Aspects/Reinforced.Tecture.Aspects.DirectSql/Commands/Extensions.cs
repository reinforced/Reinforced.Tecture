using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using static Reinforced.Tecture.Aspects.DirectSql.DirectSql;

namespace Reinforced.Tecture.Aspects.DirectSql.Commands
{
    public static partial class Extensions
    {
        private static Sql After(this Write s, LambdaExpression expr, params Type[] usedTypes)
        {
            s.PleaseAspect<Command>().Tooling.ThrowCheckTypes(usedTypes);
            var cmd = new Sql(expr);
            s.Save.Enqueue(() => s.Put(cmd));
            return cmd;
        }

        private static Sql Before(this Write s, LambdaExpression expr, params Type[] usedTypes)
        {
            s.PleaseAspect<Command>().Tooling.ThrowCheckTypes(usedTypes);
            var cmd = new Sql(expr);
            s.Put(cmd);
            return cmd;
        }
    }
}
