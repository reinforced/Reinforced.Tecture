using System.Collections;
using Reinforced.Tecture.Tracing.Promises;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Nongeneric
{
    class TracedEnumerator : IEnumerator
    {
        private readonly IEnumerator _original;
        private readonly ChannelTransaction _tran;
        private IDemander _demander;

        public TracedEnumerator(IEnumerator original, ChannelTransaction tran)
        {
            _tran = tran;
            _original = original;
            _demander = EmptyDemander.Instance;
        }
        public void Demands(Demanding<IEnumerable> d, string hash, DescriptionHolder description)
        {
            _demander = new Demander(d, hash, description);
        }
        public bool MoveNext()
        {
            var result = _original.MoveNext();
            if (!result) return false;
            _demander.MoveNext(_original.Current);
            return result;
        }

        public void Reset()
        {
            _original.Reset();
            _demander.Reset();
        }

        public void Dispose()
        {
            _tran?.Commit();
            _tran?.Dispose();
            _demander.Dispose();
        }


        object IEnumerator.Current { get { return _original.Current; } }
    }
}
