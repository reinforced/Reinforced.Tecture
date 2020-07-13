using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.CleanPlayground
{
    class CatchingEnumerator<T> : IEnumerator<T>
    {
        private List<T> _catching = new List<T>();

        private readonly IEnumerator<T> _existing;

        public CatchingEnumerator(IEnumerator<T> existing)
        {
            _existing = existing;
        }

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>
        /// <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public bool MoveNext()
        {
            var r = _existing.MoveNext();
            if (r)
            {
                _catching.Add(Current);
            }

            return r;
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public void Reset()
        {
            _existing.Reset();
        }

        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        public T Current
        {
            get { return _existing.Current; }
        }

        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        object IEnumerator.Current
        {
            get { return ((IEnumerator) _existing).Current; }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _existing.Dispose();
        }
    }
}
