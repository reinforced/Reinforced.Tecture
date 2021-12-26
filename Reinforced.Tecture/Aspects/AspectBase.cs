using System;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects
{
    /// <summary>
    /// Base class for all aspects
    /// </summary>
    public abstract class AspectBase : IDisposable
    {
        internal TestingContext _aux;
        internal Type _channel;

        /// <summary>
        /// Access to test data/query tooling
        /// </summary>
        protected TestingContext Aux
        {
            get { return _aux; }
        }

        /// <summary>
        /// Type of channel this aspect is bound to
        /// </summary>
        protected Type Channel
        {
            get { return _channel; }
        }

        internal void CallOnRegister()
        {
            OnRegister();
        }

        /// <summary>
        /// Being invoked right after aspect registration in channel multiplexer
        /// </summary>
        protected virtual void OnRegister() { }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();
    }

}
