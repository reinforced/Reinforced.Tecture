using System;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Testing.Assumptions;
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

        public static TestingEnvironment Assume(this TestingEnvironment env, Action<Assuming> assumptions)
        {
            if (assumptions != null) assumptions(env._assumptions);
            return env;
        }
    }
}
