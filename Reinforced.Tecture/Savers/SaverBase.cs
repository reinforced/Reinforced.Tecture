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

        internal AuxilaryContainer _Aux;

        /// <summary>
        /// Reference to test data & tracing capabilities
        /// </summary>
        protected Auxilary Aux(Type channel)
        {
            return _Aux.ForChannel(channel);
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Save();

        protected abstract Task SaveAsync();

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();
    }
}
