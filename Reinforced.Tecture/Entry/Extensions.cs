using System;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Entry
{
    /// <summary>
    /// Tecture entry point extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Configures channel
        /// </summary>
        /// <typeparam name="TChannel">Type of channel</typeparam>
        /// <param name="tb">Builder</param>
        /// <param name="cfg">Configuration action with channel configurator</param>
        /// <returns>Fluent</returns>
        public static TectureBuilder WithChannel<TChannel>(this TectureBuilder tb, Action<ChannelBinding<TChannel>> cfg) where TChannel : Channel
        {
            var cb = new ChannelBindingImpl<TChannel>(tb._mx, tb._transactionManager);
            cfg(cb);
            return tb;
        }

        public static TectureBuilder WithIoc(this TectureBuilder tb, Func<Type, object> iocResolver)
        {
            tb._resolver = iocResolver;
            return tb;
        }

        /// <summary>
        /// Adds exception handling method to be used by Tecture
        /// </summary>
        /// <param name="tb">Tecture builder</param>
        /// <param name="handler">Exception handler. Returns true if exception is handled and does not need further throw</param>
        /// <returns>Fluent</returns>
        public static TectureBuilder WithExceptionHandler(this TectureBuilder tb, Func<Exception,bool> handler)
        {
            tb._excHandler = handler;
            return tb;
        }

        /// <summary>
        /// Uses specified test data source for queries
        /// </summary>
        /// <param name="tb">Tecture builder</param>
        /// <param name="qs">Test data source instance</param>
        /// <returns>Fluent</returns>
        public static TectureBuilder WithTestData(this TectureBuilder tb, ITestDataSource qs)
        {
            tb.Aux._testDataProvider.Instance = qs;
            return tb;
        }

    }
}
