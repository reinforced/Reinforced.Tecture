using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Aspects.Orm.Queries.Fake;

namespace Reinforced.Tecture.Aspects.Orm.Queries
{
    interface IWrappedQueryable<out T>
    {
        IQueryable<T> Original { get; }
     
        Query Aspect { get; }

        DescriptionHolder Description { get; }
    }
}
