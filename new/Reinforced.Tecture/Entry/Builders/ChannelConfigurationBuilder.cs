using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;

namespace Reinforced.Tecture.Entry.Builders
{
    public interface ChannelConfiguration<out TChannel> where TChannel : Channel
    {

    }


    internal sealed class ChannelConfigurationImpl<TChannel> : MultiplexerRegistrationDecorator, ChannelConfiguration<TChannel> where TChannel : Channel
    {
        public ChannelConfigurationImpl(ChannelMultiplexer multiplexer) : base(multiplexer, typeof(TChannel))
        {
        }
    }
}
