using System;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Reveal;

namespace Reinforced.Tecture.Features.SqlStroke.Commands
{
    public static partial class Extensions
    {
        
        internal static StrokeProcessor GetProcessor(this Write s, Type[] usedTypes)
        {
            return s.PleaseFeature<DirectSql>().GetProcessor(usedTypes);
        }

        private static Sql After(this Write s, LambdaExpression expr, params Type[] usedTypes)
        {
            var p = s.GetProcessor(usedTypes).RevealQuery(expr);
            var cmd = new Sql(p.CommandText, p.CommandParameters);
            s.Save.Enqueue(() => s.Put(cmd));
            return cmd;
        }

        private static Sql Before(this Write s, LambdaExpression expr, params Type[] usedTypes)
        {
            var p = s.GetProcessor(usedTypes).RevealQuery(expr);
            var cmd = new Sql(p.CommandText, p.CommandParameters);
            s.Put(cmd);
            return cmd;
        }
    }
}
