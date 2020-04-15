using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Methodics.Orm.Queries
{
    public static class Extensions
    {
        public static IQueryFor<T> Get<T>(this IOrmSource qr) where T : class
        {
            return new QueryBuilder<T>(qr.Orm);
        }
    }
}
