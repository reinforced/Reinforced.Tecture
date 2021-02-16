using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Transactions
{
    /// <summary>
    /// Provides transactions capabilities
    /// </summary>
    public class ChannelTransactionsManager
    {
        public ChannelTransactionsManager(Type channel)
        {
            Channel = channel;
        }

        /// <summary>
        /// Channel
        /// </summary>
        public Type Channel { get; }

        /// <summary>
        /// Gets transaction that begins before Saving of particular channel and gets disposed immediately after
        /// </summary>
        /// <returns></returns>
        public virtual ChannelTransaction GetSaveTransaction(bool async)
        {
            return ChannelTransaction.Default;
        }

        /// <summary>
        /// Gets transaction that begins before command execution of particular channel and gets disposed immediately after
        /// </summary>
        /// <param name="command">Command that is going to be executed</param>
        /// <param name="async">True when command is going to be executed asynchronously</param>
        /// <returns>Transaction instance</returns>
        public virtual ChannelTransaction GetCommandTransaction(CommandBase command, bool async)
        {
            return ChannelTransaction.Default;
        }

        /// <summary>
        /// Gets transaction that takes place within query and gets disposed immediately after
        /// </summary>
        /// <returns></returns>
        public virtual ChannelTransaction GetQueryTransaction()
        {
            return ChannelTransaction.Default;
        }

        /// <summary>
        /// Gets transaction that takes place before the first query and gets disposed after the last saving
        /// </summary>
        /// <returns></returns>
        public virtual ChannelTransaction GetGlobalTransaction()
        {
            return ChannelTransaction.Default;
        }
    }
}
