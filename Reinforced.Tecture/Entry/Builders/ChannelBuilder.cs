using System;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;

namespace Reinforced.Tecture.Entry.Builders
{
    /// <summary>
    /// Configuration for channel of specified type
    /// </summary>
    /// <typeparam name="TChannel">Type of channel</typeparam>
    public interface ChannelBinding<out TChannel> where TChannel : Channel
    {
        /// <summary>
        /// Gets channel type
        /// </summary>
        Type Channel { get; }
    }


    internal sealed class ChannelBindingImpl<TChannel> : MultiplexerRegistrationDecorator, ChannelBinding<TChannel> where TChannel : Channel
    {
        public ChannelBindingImpl(ChannelMultiplexer multiplexer) : base(multiplexer, typeof(TChannel))
        {
        }

        public Type Channel
        {
            get { return typeof(TChannel); }
        }
    }
}
