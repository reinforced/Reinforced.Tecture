using System;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Entry
{
    /// <summary>
    /// Builder that configures and assembles Tecture instance
    /// </summary>
    public class TectureBuilder
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TectureBuilder()
        {
            var tdh = new TestDataHolder();
            Aux = new AuxilaryContainer(tdh);
            _mx = new ChannelMultiplexer(Aux);
        }

        internal readonly AuxilaryContainer Aux;
        internal readonly ChannelMultiplexer _mx;
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
                Aux,
                false, 
                _transactionManager, 
                exceptionHandler: _excHandler);
        }
    }
}
