using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Tracing.Promises;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Wrapped.Enumerators
{
    class WrappedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _original;
        private readonly ChannelTransaction _tran;
        private IDemander<T> _demander;

        public WrappedEnumerator(IEnumerator<T> original, ChannelTransaction tran)
        {
            _tran = tran;
            _original = original;
            _demander = EmptyDemander<T>.Instance;
        }

        public void Demands(Demanding<IEnumerable<T>> d, string hash, DescriptionHolder description)
        {
            _demander = new Demander<T>(d, hash, description);
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
            _demander.Reset();
            _original.Reset();
        }

        public T Current { get { return _original.Current; } }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            _tran?.Commit();
            _tran?.Dispose();
            _original.Dispose();
            _demander.Dispose();
        }
    }


    class WrappedEnumerator : IEnumerator
    {
        private readonly IEnumerator _original;
        private readonly ChannelTransaction _tran;
        private IDemander<object> _demander;

        public WrappedEnumerator(IEnumerator original, ChannelTransaction tran)
        {
            _tran = tran;
            _original = original;
            _demander = EmptyDemander<object>.Instance;
        }
        public void Demands(Demanding<IEnumerable<object>> d, string hash, DescriptionHolder description)
        {
            _demander = new Demander<object>(d, hash, description);
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
