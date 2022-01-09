using System;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Nongeneric
{
    interface IDemander : IDisposable
    {
        void MoveNext(object current);
        void Reset();
    }
}