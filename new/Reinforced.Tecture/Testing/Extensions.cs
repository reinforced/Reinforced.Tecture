using System;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Testing.Builders;

namespace Reinforced.Tecture.Testing
{
    public static class Extensions
    {
        public static TestingEnvironment WithChannel<TChannel>(this TestingEnvironment tb, Action<TestingChannelConfiguration<TChannel>> cfg) where TChannel : Channel
        {
            var cb = new TestingChannelConfigurationImpl<TChannel>(tb._mx);
            cfg(cb);
            return tb;
        }
    }
}
