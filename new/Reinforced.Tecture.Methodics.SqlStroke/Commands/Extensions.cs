using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.SqlStroke.Reveal;

namespace Reinforced.Tecture.Methodics.SqlStroke.Commands
{
    public static partial class Extensions
    {
        internal static StrokeProcessor GetProcessor(this ServicePipeline s, Type[] usedTypes)
        {
            var rt = s
                .GetRuntimes<SqlStrokeRuntimeBase>()
                .FirstOrDefault(x => usedTypes.All(t => x.Types.Contains(t)));

            if (rt == null)
                throw new SqlStrokeException($"Can not use SQL strokes. Please ensure that corresponding runtime is registered and serving following types: {string.Join(", ", usedTypes.Select(v => v.Name))}");

            return rt.GetProcessor();
        }

        private static Sql After(this ServicePipeline s, LambdaExpression expr, params Type[] usedTypes)
        {
            var p = s.GetProcessor(usedTypes).RevealQuery(expr);
            var cmd = new Sql(p.CommandText, p.CommandParameters);
            s.Save.ContinueWith(() => s.Enqueue(cmd));
            return cmd;
        }

        private static Sql Before(this ServicePipeline s, LambdaExpression expr, params Type[] usedTypes)
        {
            var p = s.GetProcessor(usedTypes).RevealQuery(expr);
            var cmd = new Sql(p.CommandText, p.CommandParameters);
            s.Enqueue(cmd);
            return cmd;
        }
    }
}
