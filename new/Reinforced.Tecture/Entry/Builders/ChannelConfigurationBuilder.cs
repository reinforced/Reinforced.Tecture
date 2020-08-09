using System;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;

namespace Reinforced.Tecture.Entry.Builders
{
    public interface ChannelConfiguration<out TChannel> where TChannel : Channel
    {
        Type Channel { get; }
    }


    internal sealed class ChannelConfigurationImpl<TChannel> : MultiplexerRegistrationDecorator, ChannelConfiguration<TChannel> where TChannel : Channel
    {
        public ChannelConfigurationImpl(ChannelMultiplexer multiplexer) : base(multiplexer, typeof(TChannel))
        {
        }

        public Type Channel
        {
            get { return typeof(TChannel); }
        }
    }
}
