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
        /// Gets specified query feature for channel's read end
        /// </summary>
        /// <typeparam name="T">Type of feature</typeparam>
        /// <param name="r">Channel's read end</param>
        /// <returns>Feature instance</returns>
        public static T Feature<T>(this Read<QueryChannel<T>> r) where T : QueryFeature
        {
            var mux = r as IQueryMultiplexer;
            
            return mux.GetFeature<T>();
        }

        /// <summary>
        /// Gets specified command feature for channel's write end
        /// </summary>
        /// <typeparam name="T">Type of feature</typeparam>
        /// <param name="w">Channel's write end</param>
        /// <returns>Feature instance</returns>
        public static T Feature<T>(this Write<CommandChannel<T>> w) where T : CommandFeature
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetFeature<T>();
        }

        /// <summary>
        /// Gets specified query feature for channel's read end
        /// </summary>
        /// <typeparam name="T">Type of feature</typeparam>
        /// <param name="r">Channel's read end</param>
        /// <returns>Feature instance</returns>
        public static T PleaseFeature<T>(this Read r) where T : QueryFeature
        {
            var mux = r as IQueryMultiplexer;

            return mux.GetFeature<T>();
        }

        /// <summary>
        /// Gets specified query feature for channel's write end
        /// </summary>
        /// <typeparam name="T">Type of feature</typeparam>
        /// <param name="w">Channel's write end</param>
        /// <returns>Feature instance</returns>
        public static T PleaseFeature<T>(this Write w) where T : CommandFeature
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetFeature<T>();
        }
    }
}
