using System;
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

        private readonly Dictionary<string, ChannelTransaction> _globalTransactions = new Dictionary<string, ChannelTransaction>();

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
                        _globalTransactions[channel] = _transactionManagers[channel].GetGlobalTransaction();
                    }
                }
            }
        }

        public Dictionary<string,ChannelTransaction> GetSaveTransactions(IEnumerable<string> channels, bool async)
        {
            List<ChannelTransaction> ctr = new List<ChannelTransaction>();
            foreach (var channel in channels)
            {
                if (_transactionManagers.ContainsKey(channel)) ctr.Add(_transactionManagers[channel].GetSaveTransaction(async));
                else ctr.Add(ChannelTransaction.Default);
            }
            return ctr.ToDictionary(x=>x.Channel.FullName);
        }
        
        public ChannelTransaction GetQueryTransaction(Type channel)
        {
            var result =
                _transactionManagers.ContainsKey(channel.FullName)
                    ? _transactionManagers[channel.FullName].GetQueryTransaction()
                    : ChannelTransaction.Default;
            EnsureGlobal(channel.FullName);
            return result;
        }

        public ChannelTransaction GetCommandTransaction(string channelFullName, CommandBase command, bool async)
        {
            var result =
                _transactionManagers.ContainsKey(channelFullName)
                    ? _transactionManagers[channelFullName].GetCommandTransaction(command, async)
                    : ChannelTransaction.Default;
            EnsureGlobal(channelFullName);
            return result;
        }

        private readonly object _locker = new object();

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
