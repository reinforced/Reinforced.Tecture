using System;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Generic
{
    interface IDemander<T> : IDisposable
    {
        void MoveNext(T current);
        void Reset();
    }
}