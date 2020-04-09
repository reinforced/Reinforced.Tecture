using System;
using System.Collections.Generic;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;

namespace Reinforced.Tecture.Testing.Assumptions
{
    public class Assuming
    {
        private readonly RuntimeMultiplexer _mx;
        internal readonly Dictionary<Type, List<AssumptionBase>> _data = new Dictionary<Type, List<AssumptionBase>>();

        internal Assuming(RuntimeMultiplexer mx)
        {
            _mx = mx;
        }

        public Assuming Assume<TCommand>(AssumptionBase<TCommand> assumption) where TCommand : CommandBase
        {
            if (!_data.ContainsKey(typeof(TCommand)))
            {
                _data[typeof(TCommand)] = new List<AssumptionBase>();
            }

            assumption.Locate = _mx;
            _data[typeof(TCommand)].Add(assumption);
            return this;
        }
    }
}