using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;

namespace Reinforced.Tecture.Testing.Builders
{
    public interface TestingChannelConfiguration<out TChannel> where TChannel : Channel
    {

    }


    internal sealed class TestingChannelConfigurationImpl<TChannel> : MultiplexerRegistrationDecorator, TestingChannelConfiguration<TChannel> where TChannel : Channel
    {
        public TestingChannelConfigurationImpl(ChannelMultiplexer multiplexer) : base(multiplexer, typeof(TChannel))
        {
        }
    }
}
