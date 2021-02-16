using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Tracing
{

    internal class TraceCollector
    {
        private readonly Queue<CommandBase> _traceCommands = new Queue<CommandBase>();

        internal void Command(CommandBase command)
        {
            _traceCommands.Enqueue(command);
        }

        internal void Save()
        {
            Command(new Save());
        }

        internal Trace Finish()
        {
            Command(new End());
            return new Trace(_traceCommands);
        }

        public PromisedQuery<T> PromiseQuery<T>(Type channel)
        {
            var cmd = new QueryRecord(channel, false);
            Command(cmd);
            return new PromisedQuery<T>(cmd);
        }

        public PromisedQuery<T> PromiseTestQuery<T>(Type channel)
        {
            var cmd = new QueryRecord(channel, true);
            Command(cmd);
            return new PromisedQuery<T>(cmd);
        }

    }
}
