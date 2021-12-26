using System;

namespace Reinforced.Tecture.Aspects.Orm
{
    /// <summary>
    /// Lazy + IDisposable interface
    /// </summary>
    /// <typeparam name="T">Containing value</typeparam>
    public interface ILazyDisposable<out T> : IDisposable
    {
        /// <summary>
        /// Lazy-disposed value
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Type of lazy-disposed value (use this instead of Value.GetType())
        /// </summary>
        Type ValueType { get; }
    }

    /// <summary>
    /// Lazy + IDisposable
    /// </summary>
    /// <typeparam name="T">Containing value</typeparam>
    public class LazyDisposable<T> : ILazyDisposable<T>
    {
        private readonly Func<T> _getter;

        private T _value;

        /// <summary>
        /// Constructs new instance of lazy-disposable
        /// </summary>
        /// <param name="getter">Value getter</param>
        public LazyDisposable(Func<T> getter)
        {
            _getter = getter;
        }

        /// <summary>
        /// Constructs default instance of Lazy Disposable
        /// </summary>
        /// <returns>Default instance</returns>
        public static LazyDisposable<T> Default()
        {
            return new LazyDisposable<T>(() => default(T));
        }

        private readonly object _locker = new object();

        /// <inheritdoc cref="ILazyDisposable{T}"/>
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

        /// <inheritdoc cref="ILazyDisposable{T}"/>
        public Type ValueType
        {
            get { return typeof(T); }
        }

        private bool _isObtained = false;
        private bool _isDisposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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
