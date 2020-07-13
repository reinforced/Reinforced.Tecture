using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Channels
{
    /// <summary>
    /// Set of infrastructure channel extensions
    /// </summary>
    public static class InfrastructureChannelExtensions
    {

        /// <summary>
        /// Retrieves specified query feature for 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static T Feature<T>(this Read<QueryChannel<T>> r) where T : QueryFeature
        {
            var mux = r as IQueryMultiplexer;
            
            return mux.GetFeature<T>();
        }

        public static T Feature<T>(this Write<CommandChannel<T>> w) where T : CommandFeature
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetFeature<T>();
        }

        /// <summary>
        /// Retrieves specified query feature for 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static T PleaseFeature<T>(this Read r) where T : QueryFeature
        {
            var mux = r as IQueryMultiplexer;

            return mux.GetFeature<T>();
        }

        /// <summary>
        /// Retrieves specified query feature for 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static IQueryStore QueryStore(this Read r)
        {
            var mux = r as IQueryMultiplexer;

            return mux.GetStore();
        }

        public static T PleaseFeature<T>(this Write w) where T : CommandFeature
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetFeature<T>();
        }
    }
}
