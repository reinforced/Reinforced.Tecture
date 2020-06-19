using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Entry.Builders
{
    public static partial class Extensions
    {
        /// <summary>
        /// Specifies query feature implementation for data channel
        /// </summary>
        /// <typeparam name="TFeature">Query feature type</typeparam>
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Feature implementation</param>
        public static void ForQuery<TFeature>(this ChannelConfiguration<QueryChannel<TFeature>> cf, TFeature feature) where TFeature : QueryFeature
        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterQueryFeature(typeof(TFeature), feature);
        }
    }
}