using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Channels
{
    /// <summary>
    /// Set of infrastructure channel extensions
    /// </summary>
    public static class InfrastructureChannelExtensions
    {
        /// <summary>
        /// Gets specified query aspect for channel's read end
        /// </summary>
        /// <typeparam name="T">Type of aspect</typeparam>
        /// <param name="r">Channel's read end</param>
        /// <returns>Aspect instance</returns>
        public static T Aspect<T>(this Read<QueryChannel<T>> r) where T : QueryAspect
        {
            var mux = r as IQueryMultiplexer;
            
            return mux.GetAspect<T>();
        }

        /// <summary>
        /// Gets specified command aspect for channel's write end
        /// </summary>
        /// <typeparam name="T">Type of aspect</typeparam>
        /// <param name="w">Channel's write end</param>
        /// <returns>Aspect instance</returns>
        public static T Aspect<T>(this Write<CommandChannel<T>> w) where T : CommandAspect
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetAspect<T>();
        }

        /// <summary>
        /// Gets specified query aspect for channel's read end
        /// </summary>
        /// <typeparam name="T">Type of aspect</typeparam>
        /// <param name="r">Channel's read end</param>
        /// <returns>Aspect instance</returns>
        public static T PleaseAspect<T>(this Read r) where T : QueryAspect
        {
            var mux = r as IQueryMultiplexer;

            return mux.GetAspect<T>();
        }

        /// <summary>
        /// Gets specified query aspect for channel's write end
        /// </summary>
        /// <typeparam name="T">Type of aspect</typeparam>
        /// <param name="w">Channel's write end</param>
        /// <returns>Aspect instance</returns>
        public static T PleaseAspect<T>(this Write w) where T : CommandAspect
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetAspect<T>();
        }
    }
}
