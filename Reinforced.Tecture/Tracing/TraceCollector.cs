using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

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

        public void Query<T>(Type channel, Type dataType, string hash, T result, string description)
        {
            Command(new QueryRecord(channel, dataType, hash, result, false).Annotate(description));
        }

        public void TestQuery<T>(Type channel, Type dataType, string hash, T result, string description)
        {
            Command(new QueryRecord(channel,dataType, hash, result, true).Annotate(description));
        }

    }
}
