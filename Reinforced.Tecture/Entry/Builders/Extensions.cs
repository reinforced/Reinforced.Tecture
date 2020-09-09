using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;

namespace Reinforced.Tecture.Entry.Builders
{
    /// <summary>
    /// Channel binding extensions
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Specifies query aspect implementation for data channel
        /// </summary>
        /// <typeparam name="TAspect">Query aspect type</typeparam>
        /// <param name="cf">Channel configuration</param>
        /// <param name="aspect">Aspect implementation</param>
        public static void ForQuery<TAspect>(this ChannelBinding<QueryChannel<TAspect>> cf, TAspect aspect) where TAspect : QueryAspect
        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterQueryAspect(typeof(TAspect), aspect);
        }
    }
}