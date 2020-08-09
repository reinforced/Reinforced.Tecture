using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;

namespace Reinforced.Tecture.Testing.Builders
{
    public static partial class Extensions
    {
        /// <summary>
        /// Specifies testing query feature implementation for data channel
        /// </summary>
        /// <typeparam name="TFeature">Query feature type</typeparam>
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Feature implementation</param>
        public static void ForQuery<TFeature>(this TestingChannelConfiguration<QueryChannel<TFeature>> cf, TFeature feature) where TFeature : QueryFeature
        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterQueryFeature(typeof(TFeature), feature);
        }
    }
}
