using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture
{
    /// <summary>
    /// Lazy + IDisposable
    /// </summary>
    /// <typeparam name="T">Containing value</typeparam>
    public class LazyDisposable<T> : IDisposable
    {
        private readonly Func<T> _getter;

        private T _value;

        public LazyDisposable(Func<T> getter)
        {
            _getter = getter;
        }

        private readonly object _locker = new object();

        public T Value
        {
            get
            {
                if (!_isObtained)
                {
                    lock (_locker)
                    {
                        if (!_isObtained)
                        {
                            _value = _getter();
                            _isObtained = true;
                        }
                    }
                }

                return _value;
            }
        }

        private bool _isObtained = false;
        private bool _isDisposed = false;
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            
            if (_isObtained)
            {
                lock (_locker)
                {
                    if (!_isDisposed)
                    {
                        if (_value is IDisposable d)
                        {
                            d.Dispose();
                        }

                        _isDisposed = true;
                    }
                }
            }
        }
    }
}
