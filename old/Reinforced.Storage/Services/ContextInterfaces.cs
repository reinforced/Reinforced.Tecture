using System;

namespace Reinforced.Storage.Services
{
    /// <summary>
    /// Defines storage service without context
    /// </summary>
    public interface INoContext { }

    /// <summary>
    /// Defines storage interface with context
    /// </summary>
    public interface IWithContext { }

    public class LetBuilder
    {
        internal virtual Type HoistType { get; }
        internal LetBuilder() { }
    }

    /// <summary>
    /// Generic service context builder
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public class LetBuilder<TService> : LetBuilder where TService : StorageService, IWithContext
    {
        internal override Type HoistType { get { return typeof(TService); } }

        private readonly ServiceManager _serviceManager;

        internal LetBuilder(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        internal TService Init(Type[] paramTypes, object[] ctx)
        {
            return _serviceManager.CreateWithContext<TService>(paramTypes, ctx);
        }
    }
}
