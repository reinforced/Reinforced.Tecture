using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;

namespace Reinforced.Tecture.Aspects.Time
{
    public static class Extensions
    {
        /// <summary>
        /// Use Direct SQL query aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseDateTime(this ChannelBinding<QueryChannel<Query>> conf)
        {
            conf.ForQuery(new Query());
        }
    }
}