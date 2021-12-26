using System;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;
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
            var tdh = new TestDataProvider();
            Aux = new TestingContextContainer(tdh, _transactionManager);
            _mx = new ChannelMultiplexer(Aux);
        }

        internal readonly TestingContextContainer Aux;
        internal readonly ChannelMultiplexer _mx;
        internal readonly TransactionManager _transactionManager = new TransactionManager();
        internal Func<Exception, bool> _excHandler = null;
        internal Func<Type, object> _resolver = null;

        /// <summary>
        /// Produces Tecture instance
        /// </summary>
        /// <returns></returns>
        public ITecture Build()
        {
            _mx.Validate();
            return new Tecture(
                _mx,
                Aux,
                false,
                _transactionManager,
                exceptionHandler: _excHandler,
                _resolver);
        }
    }
}
