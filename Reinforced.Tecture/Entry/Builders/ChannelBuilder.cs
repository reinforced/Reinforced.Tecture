using System;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Entry.Builders
{
    /// <summary>
    /// Channel binding
    /// </summary>
    public interface ChannelBinding
    {
        /// <summary>
        /// Gets channel type
        /// </summary>
        Type Channel { get; }
    }

    /// <summary>
    /// Configuration for channel of specified type
    /// </summary>
    /// <typeparam name="TChannel">Type of channel</typeparam>
    public interface ChannelBinding<out TChannel> : ChannelBinding where TChannel : Channel
    {
       
    }


    internal sealed class ChannelBindingImpl<TChannel> : ChannelConfigurator, ChannelBinding<TChannel> where TChannel : Channel
    {
        public ChannelBindingImpl(ChannelMultiplexer multiplexer, TransactionManager transactionManager) : base(multiplexer, typeof(TChannel), transactionManager)
        {
            
        }

        public Type Channel => typeof(TChannel);
    }
}
