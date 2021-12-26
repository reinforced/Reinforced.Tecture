using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Time.Queries
{
    /// <summary>
    /// Extensions for Date/Time channel
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Helps to obtain current date in DateTime format
        /// </summary>
        /// <param name="tc">Time channel</param>
        /// <returns>DateTime accessor</returns>
        public static DateTimeWrapper Date(this Read<QueryChannel<Query>> tc) => tc.Aspect().DateTimeWrapper;
        
        /// <summary>
        /// Helps to obtain current date in Offset format
        /// </summary>
        /// <param name="tc">Time channel</param>
        /// <returns>DateTimeOffset accessor</returns>
        public static DateTimeOffsetWrapper Offset(this Read<QueryChannel<Query>> tc) => tc.Aspect().DateTimeOffsetWrapper;
    }
}