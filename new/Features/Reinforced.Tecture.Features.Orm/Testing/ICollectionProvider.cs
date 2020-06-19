using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Methodics.Orm.Testing
{
    public interface ICollectionProvider
    {
        IList<T> GetCollection<T>();

        IList GetCollection(Type t);
    }
}
