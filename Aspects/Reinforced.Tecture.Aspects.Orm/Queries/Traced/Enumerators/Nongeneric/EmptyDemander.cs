namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Nongeneric
{
    class EmptyDemander : IDemander
    {
        private EmptyDemander() { }
        public void MoveNext(object current) { }
        public void Reset() { }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() { }

        public static readonly EmptyDemander Instance = new EmptyDemander();
    }
}