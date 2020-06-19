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

        public void RegisterQueryFeature(Type queryFeatureType, QueryFeature feature)
        {
            _multiplexer.RegisterQueryFeature(_channelType, queryFeatureType, feature);
        }

        public void RegisterCommandFeature(Type commandFeatureType, CommandFeature feature)
        {
            _multiplexer.RegisterCommandFeature(_channelType, commandFeatureType, feature);
        }

        public void RegisterSaver(SaverBase sb)
        {
            _multiplexer.RegisterSaver(_channelType, sb);
        }
    }
}
