using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.SqlStroke.Queries
{
    public static partial class Extensions
    {
        private static RawQuery QueryCore(this Read<QueryChannel<Query>> s, LambdaExpression expr, params Type[] usedTypes)
        {
            var rt = s.Feature();
            rt.Tooling.ThrowCheckTypes(usedTypes);
            var cmd = new Sql(expr);
            return new RawQuery(cmd, rt);
        }

        /// <summary>
        /// Describes SQL query for testing purposes
        /// </summary>
        /// <param name="q">Raw query</param>
        /// <param name="description">Description test</param>
        /// <returns>Fluent</returns>
        public static RawQuery Describe(this RawQuery q, string description)
        {
            q._description = description;
            return q;
        }
    }
}
