using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Commands
{

    public class CatchingCommands<TCather> : IDisposable 
        where TCather : ICommandCatcher
    {
        private readonly Pipeline _queue;
        private readonly TCather _catcher;
        internal readonly string _annotation;
        /// <summary>
        /// Active side-effect catcher
        /// </summary>
        public TCather Catcher => _catcher;

        internal CatchingCommands(TCather catcher, Pipeline queue, string annotation)
        {
            _catcher = catcher;
            _queue = queue;
            _annotation = annotation;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => _queue.FinishCatch(this);
    }

    /// <summary>
    /// Thing that allows to catch various commands and aggregate it to another command
    /// </summary>
    public interface ICommandCatcher
    {
        /// <summary>
        /// Catches command
        /// </summary>
        /// <param name="effect">Command that must be caught</param>
        void Catch(CommandBase effect);

        /// <summary>
        /// Produces resulting command of caught ones
        /// </summary>
        /// <returns>Resulting command</returns>
        CommandBase Produce();
    }
}
