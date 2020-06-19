using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Savers
{
    /// <summary>
    /// Base for all savers
    /// </summary>
    public abstract class SaverBase
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

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Save();

        protected abstract Task SaveAsync();

    }
}
