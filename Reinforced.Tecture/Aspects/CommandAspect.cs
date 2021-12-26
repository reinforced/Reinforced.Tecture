using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Aspects
{
    /// <summary>
    /// Command aspect type
    /// </summary>
    public abstract class CommandAspect : AspectBase
    {
        internal CommandAspect(){}
        internal abstract IEnumerable<Type> ServingCommandTypes { get; }

        internal void SaveInternal() => Save();

        internal Task SaveInternalAsync() => SaveAsync();

        /// <summary>
        /// Actually performs saving
        /// </summary>
        protected abstract void Save();

        /// <summary>
        /// Actually performs saving (async)
        /// </summary>
        protected abstract Task SaveAsync();
        
        internal abstract CommandRunner GetRunner(CommandBase cb);
    }
}