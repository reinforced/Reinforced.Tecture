﻿using System;
using Reinforced.Tecture.Aspects;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Channels.Multiplexer
{
    internal class ChannelConfigurator
    {
        internal ChannelConfigurator(ChannelMultiplexer multiplexer, Type channelType, TransactionManager transactionManager)
        {
            _multiplexer = multiplexer;
            _channelType = channelType;
            TransactionManager = transactionManager;
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

        public TransactionManager TransactionManager { get; }
    }
}
