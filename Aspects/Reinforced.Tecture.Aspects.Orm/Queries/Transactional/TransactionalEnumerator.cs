using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Transactional
{
    class TransactionalEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _original;
        private readonly Auxilary _auxilary;
        private ChannelTransaction _tran;
        public TransactionalEnumerator(IEnumerator<T> original, Auxilary auxilary)
        {
            _original = original;
            _auxilary = auxilary;
        }
        private readonly object locker = new object();
        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public bool MoveNext()
        {
            if (_tran != null)
            {
                lock (locker)
                {
                    if (_tran != null)
                        _tran = _auxilary.GetQueryTransaction();
                }
            }
            return _original.MoveNext();
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public void Reset()
        {
            _original.Reset();
        }

        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        public T Current
        {
            get { return _original.Current; }
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
            _tran?.Commit();
            _tran?.Dispose();
            _original.Dispose();
        }
    }


    class TransactionalEnumerator : IEnumerator
    {
        private readonly IEnumerator _original;
        private readonly Auxilary _auxilary;
        private ChannelTransaction _tran;
        public TransactionalEnumerator(IEnumerator original, Auxilary auxilary)
        {
            _original = original;
            _auxilary = auxilary;
        }
        private readonly object locker = new object();
        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public bool MoveNext()
        {
            if (_tran != null)
            {
                lock (locker)
                {
                    if (_tran != null)
                        _tran = _auxilary.GetQueryTransaction();
                }
            }
            return _original.MoveNext();
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public void Reset()
        {
            _original.Reset();
        }

        
        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        object IEnumerator.Current
        {
            get { return _original.Current; }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _tran.Commit();
            _tran.Dispose();
        }
    }
}
