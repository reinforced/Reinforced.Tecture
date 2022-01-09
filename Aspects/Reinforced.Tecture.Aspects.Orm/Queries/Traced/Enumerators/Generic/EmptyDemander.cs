namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Generic
{
    class EmptyDemander<T> : IDemander<T>
    {
        private EmptyDemander() { }
        public void MoveNext(T current) { }
        public void Reset() { }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() { }

        public static readonly EmptyDemander<T> Instance = new EmptyDemander<T>();
    }
}