using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Channels.Multiplexer
{
    internal class MultiplexerRegistrationDecorator
    {
        internal MultiplexerRegistrationDecorator(ChannelMultiplexer multiplexer, Type channelType)
        {
            _multiplexer = multiplexer;
            _channelType = channelType;
        }

        private readonly Type _channelType;
        private readonly ChannelMultiplexer _multiplexer;

        public void RegisterQueryAspect(Type queryAspectType, QueryAspect aspect)
        {
            _multiplexer.RegisterQueryAspect(_channelType, queryAspectType, aspect);
        }

        public void RegisterCommandAspect(Type commandAspectType, CommandAspect aspect)
        {
            _multiplexer.RegisterCommandAspect(_channelType, commandAspectType, aspect);
        }

        public void RegisterSaver(SaverBase sb)
        {
            _multiplexer.RegisterSaver(_channelType, sb);
        }
    }
}
