using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reinforced.Tecture.Methodics.Orm.Queries
{
    interface ISetAccess
    {
        IQueryable<T> Set<T>();
    }
}
