using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Methodics.Orm.Queries
{
    public interface IOrmSource : ISource 
    {
        IOrmQueryRuntime Orm { get; }
    }
}
