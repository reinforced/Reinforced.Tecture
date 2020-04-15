using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Integrate;

namespace Reinforced.Tecture.Methodics.Orm.Queries
{
    public interface IOrmQueryRuntime : IQueryRuntime
    {
        IQueryable<T> Get<T>() where T : class;
        QueryStats Stats { get; }
    }
}
