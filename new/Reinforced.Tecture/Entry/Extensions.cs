using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Entry
{
    public static class Extensions
    {
        /// <summary>
        /// Adds runtime to be used by Tecture
        /// </summary>
        /// <param name="tb">Tecture builder</param>
        /// <param name="runtime">Tecture runtime</param>
        /// <returns>Fluent</returns>
        public static TectureBuilder WithRuntime(this TectureBuilder tb, ITectureRuntime runtime)
        {
            tb._mx.AddRuntime(runtime);
            return tb;
        }

        /// <summary>
        /// Points transaction manager to use
        /// </summary>
        /// <param name="tb">Tecture builder</param>
        /// <param name="manager">Transaction manager instance</param>
        /// <returns>Fluent</returns>
        public static TectureBuilder WithTransactions(this TectureBuilder tb, ITransactionManager manager)
        {
            tb._transactionManager = manager;
            return tb;
        }

        /// <summary>
        /// Adds exception handling method to be used by Tecture
        /// </summary>
        /// <param name="tb">Tecture builder</param>
        /// <param name="handler">Exception handler</param>
        /// <returns>Fluent</returns>
        public static TectureBuilder WithExceptionHandler(this TectureBuilder tb, Action<Exception> handler)
        {
            tb._excHandler = handler;
            return tb;
        }

    }
}
