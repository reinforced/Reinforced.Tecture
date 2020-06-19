using System;
using System.Collections.Generic;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Assumptions
{
    public class Assuming
    {
        private readonly ChannelMultiplexer _mx;
        internal readonly Dictionary<Type, List<IAssumption>> _data = new Dictionary<Type, List<IAssumption>>();

        internal Assuming(ChannelMultiplexer mx)
        {
            _mx = mx;
        }

        public Assuming Assume<TCommand>(AssumptionBase<TCommand> assumption) where TCommand : CommandBase
        {
            if (!_data.ContainsKey(typeof(TCommand)))
            {
                _data[typeof(TCommand)] = new List<IAssumption>();
            }

            _data[typeof(TCommand)].Add(assumption);
            return this;
        }
    }
}