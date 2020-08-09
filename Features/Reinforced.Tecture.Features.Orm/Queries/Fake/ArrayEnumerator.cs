using System.Collections;
using System.Collections.Generic;

namespace Reinforced.Tecture.Features.Orm.Queries.Fake
{
    class ArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _array;
        private long _currentIndex = -1;

        public ArrayEnumerator(T[] array)
        {
            _array = array;
        }

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public bool MoveNext()
        {
            _currentIndex++;
            if (_currentIndex >= _array.Length)
            {
                _currentIndex--;
                return false;
            }

            return true;
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public void Reset()
        {
            _currentIndex = 0;
        }

        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        public T Current
        {
            get { return _array[_currentIndex]; }
        }

        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            
        }
    }
}
