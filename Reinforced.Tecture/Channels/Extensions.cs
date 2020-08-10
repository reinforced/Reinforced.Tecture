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
        /// Retrieves specified query feature for 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <param name="a">Auxilary tools for queries</param>
        /// <returns></returns>
        public static T Feature<T>(this Read<QueryChannel<T>> r, out Auxilary a) where T : QueryFeature
        {
            var mux = r as IQueryMultiplexer;
            
            return mux.GetFeature<T>(out a);
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
        /// <param name="a">Auxilary tools for queries</param>
        /// <returns></returns>
        public static T PleaseFeature<T>(this Read r, out Auxilary a) where T : QueryFeature
        {
            var mux = r as IQueryMultiplexer;

            return mux.GetFeature<T>(out a);
        }

        
        public static T PleaseFeature<T>(this Write w) where T : CommandFeature
        {
            var mux = w as ICommandMultiplexer;

            return mux.GetFeature<T>();
        }
    }
}
