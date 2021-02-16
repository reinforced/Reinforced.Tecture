using System;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture
{
    /// <summary>
    /// Base class for all aspects
    /// </summary>
    public abstract class Aspect : IDisposable
    {
        internal Auxiliary _aux;
        internal Type _channel;

        /// <summary>
        /// Access to test data/query tooling
        /// </summary>
        protected Auxiliary Aux
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
        protected virtual void OnRegister()
        {

        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();
    }

    /// <summary>
    /// Query aspect type
    /// </summary>
    public abstract class QueryAspect : Aspect { }

    /// <summary>
    /// Command aspect type
    /// </summary>
    public abstract class CommandAspect : Aspect { }
}
