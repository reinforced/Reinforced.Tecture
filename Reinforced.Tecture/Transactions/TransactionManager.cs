using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Transactions
{
    class TransactionManager : IDisposable
    {
        private readonly Dictionary<string, ChannelTransactionsManager> _transactionManagers =
            new Dictionary<string, ChannelTransactionsManager>();

        private readonly ConcurrentDictionary<string, ChannelTransaction> _globalTransactions = new ConcurrentDictionary<string, ChannelTransaction>();
        private readonly object _locker = new object();
        
        public void RegisterManager(Type channel, ChannelTransactionsManager ctm)
        {
            _transactionManagers[channel.FullName] = ctm;
        }

        private void EnsureGlobal(string channel)
        {
            if (!_globalTransactions.ContainsKey(channel))
            {
                lock (_locker)
                {
                    if (!_globalTransactions.ContainsKey(channel))
                    {
                        _globalTransactions[channel] =
                            _transactionManagers.ContainsKey(channel)
                                ? _transactionManagers[channel].GetGlobalTransaction()
                                : ChannelTransaction.Default;
                    }
                }
            }
        }

        public Dictionary<string, ChannelTransaction> GetSaveTransactions(IEnumerable<string> channels, bool async)
        {
            Dictionary<string, ChannelTransaction> result = new Dictionary<string, ChannelTransaction>();
            foreach (var channel in channels)
            {
                result[channel] = _transactionManagers.ContainsKey(channel)
                    ? _transactionManagers[channel].GetSaveTransaction(async)
                        : ChannelTransaction.Default;
            }
            return result;
        }

        public ChannelTransaction GetQueryTransaction(Type channel)
        {
            var result =
                _transactionManagers.ContainsKey(channel.FullName)
                    ? _transactionManagers[channel.FullName].GetQueryTransaction()
                    : ChannelTransaction.Default;
            if (result!=ChannelTransaction.Default) EnsureGlobal(channel.FullName);
            return result;
        }

        public ChannelTransaction GetCommandTransaction(string channelFullName, CommandBase command, bool async)
        {
            var result =
                _transactionManagers.ContainsKey(channelFullName)
                    ? _transactionManagers[channelFullName].GetCommandTransaction(command, async)
                    : ChannelTransaction.Default;
            
            if (result!=ChannelTransaction.Default) EnsureGlobal(channelFullName);
            return result;
        }

        public void FinishAllGlobalTransactions()
        {
            lock (_locker)
            {
                foreach (var channelTransaction in _globalTransactions)
                {
                    channelTransaction.Value.Dispose();
                }

                _globalTransactions.Clear();
            }

        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            FinishAllGlobalTransactions();
        }
    }
}
