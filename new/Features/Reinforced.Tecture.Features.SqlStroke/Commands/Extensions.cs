using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.SqlStroke.Reveal;

namespace Reinforced.Tecture.Features.SqlStroke.Commands
{
    public static partial class Extensions
    {
        private static Sql After(this Write s, LambdaExpression expr, params Type[] usedTypes)
        {
            s.PleaseFeature<Command>().ThrowCheckTypes(usedTypes);
            var cmd = new Sql(expr);
            s.Save.Enqueue(() => s.Put(cmd));
            return cmd;
        }

        private static Sql Before(this Write s, LambdaExpression expr, params Type[] usedTypes)
        {
            s.PleaseFeature<Command>().ThrowCheckTypes(usedTypes);
            var cmd = new Sql(expr);
            s.Put(cmd);
            return cmd;
        }
    }
}
