using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Entry
{
    /// <summary>
    /// Builder that configures and assembles Tecture instance
    /// </summary>
    public class TectureBuilder
    {
        internal readonly RuntimeMultiplexer _mx = new RuntimeMultiplexer();
        internal ITransactionManager _transactionManager;
        internal Action<Exception> _excHandler = null;

        /// <summary>
        /// Produces Tecture instance
        /// </summary>
        /// <returns></returns>
        public ITecture Build()
        {
            return new Tecture(
                _mx,
                new CommandsDispatcher(_mx),
                false, 
                _transactionManager, 
                exceptionHandler: _excHandler);
        }
    }
}
