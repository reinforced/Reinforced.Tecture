using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Savers
{
    /// <summary>
    /// Base for all savers
    /// </summary>
    public abstract class SaverBase : IDisposable
    {
        internal void SaveInternal()
        {
            Save();
        }

        internal Task SaveInternalAsync()
        {
            return SaveAsync();
        }

        internal abstract IEnumerable<Type> ServingCommandTypes { get; }

        internal abstract CommandRunner GetRunner(CommandBase cb);

        internal Auxilary _Aux;

        /// <summary>
        /// Reference to test data and tracing capabilities
        /// </summary>
        protected Auxilary Aux
        {
            get { return _Aux; }
        }

        internal void CallOnRegister()
        {
            OnRegister();
        }

        /// <summary>
        /// Being invoked when saver has been just registered in channel muliplexer
        /// </summary>
        protected abstract void OnRegister();

        /// <summary>
        /// Actually performs saving
        /// </summary>
        protected abstract void Save();

        /// <summary>
        /// Actually performs saving (async)
        /// </summary>
        protected abstract Task SaveAsync();

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();
    }
}
