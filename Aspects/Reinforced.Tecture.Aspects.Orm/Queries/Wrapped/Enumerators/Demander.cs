using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Wrapped.Enumerators
{
    interface IDemander<T> : IDisposable
    {
        void MoveNext(T current);
        void Reset();
    }

    class EmptyDemander<T> : IDemander<T>
    {
        private EmptyDemander() { }
        public void MoveNext(T current) { }
        public void Reset() { }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() { }

        public static readonly EmptyDemander<T> Instance = new EmptyDemander<T>();
    }

    class Demander<T> : IDemander<T>
    {
        private readonly Demanding<IEnumerable<T>> _demanding;
        private readonly string _hash;
        private readonly DescriptionHolder _description;
        private readonly List<T> _data;
        private long _currentIndex = -1;
        private long _indexBeforeReset = -1;

        public Demander(Demanding<IEnumerable<T>> demanding, string hash, DescriptionHolder description)
        {
            _demanding = demanding;
            _hash = hash;
            _description = description;
            _data = new List<T>();
        }

        public void MoveNext(T current)
        {
            _currentIndex++;
            if (_currentIndex > _indexBeforeReset)
            {
                _data.Add(DeepCloner.DeepClone(current));
            }
        }

        public void Reset()
        {
            _currentIndex = -1;
            _indexBeforeReset = _currentIndex;
        }

        public void Dispose()
        {
            _demanding.Fullfill(_data, _data, _hash, _description.Description);
        }
    }
}
