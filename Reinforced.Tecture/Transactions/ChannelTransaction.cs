using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Transactions
{
    /// <summary>
    /// Transaction 
    /// </summary>
    public class ChannelTransaction : IDisposable
    {
        public ChannelTransaction(Type channel)
        {
            Channel = channel;
        }

        /// <summary>
        /// Type of channel, transaction is being performed in
        /// </summary>
        public Type Channel { get; }

        public virtual void Commit() { }
        
        public virtual void Dispose() { }

        /// <summary>
        /// Default transaction value
        /// </summary>
        internal static ChannelTransaction Default = new ChannelTransaction(null);
    }
}
