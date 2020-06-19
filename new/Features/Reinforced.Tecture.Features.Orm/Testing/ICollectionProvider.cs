using System;
using System.Collections;
using System.Collections.Generic;

namespace Reinforced.Tecture.Features.Orm.Testing
{
    public interface ICollectionProvider
    {
        IList<T> GetCollection<T>();

        IList GetCollection(Type t);
    }
}
