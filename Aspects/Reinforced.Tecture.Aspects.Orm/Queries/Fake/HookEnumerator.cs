using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Fake
{
    class HookEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _original;
        private readonly List<T> _data = new List<T>();
        private long _currentIndex = -1;
        private long _indexBeforeReset = -1;


        public HookEnumerator(string hash, IEnumerator<T> original, Auxilary aux, DescriptionHolder description)
        {
            _original = original;
            aux.QueryManuallyClone(hash, (IEnumerable<T>)_data, description.Description);
        }

        public bool MoveNext()
        {
            var result = _original.MoveNext();
            if (!result) return false;

            _currentIndex++;
            if (_currentIndex > _indexBeforeReset)
            {
                _current = _original.Current;
                _data.Add(_current.DeepClone());
            }
            return result;
        }

        public void Reset()
        {
            _currentIndex = -1;
            _indexBeforeReset = _currentIndex;
            _original.Reset();
        }

        private T _current;
        public T Current { get { return _current; } }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            _original.Dispose();
        }
    }


    class HookEnumerator : IEnumerator
    {
        private readonly IEnumerator _original;
        private readonly List<object> _data = new List<object>();
        private long _currentIndex = -1;
        private long _indexBeforeReset = -1;

        public HookEnumerator(string hash, IEnumerator original, Auxilary aux, DescriptionHolder description)
        {
            _original = original;
            aux.QueryManuallyClone(hash, (IEnumerable<object>)_data, description.Description);
        }

        public bool MoveNext()
        {
            var result = _original.MoveNext();
            if (!result) return false;

            _currentIndex++;
            if (_currentIndex > _indexBeforeReset)
            {
                _data.Add(_original.Current.DeepClone());
            }
            return result;
        }

        public void Reset()
        {
            _currentIndex = -1;
            _indexBeforeReset = _currentIndex;
            _original.Reset();
        }


        object IEnumerator.Current { get { return _original.Current; } }
    }
}
