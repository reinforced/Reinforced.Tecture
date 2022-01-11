using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Tracing
{
    internal class TraceCollector
    {
        private readonly Queue<CommandBase> _traceCommands = new Queue<CommandBase>();

        internal readonly bool LightMode;

        internal readonly bool Profiling;

        public TraceCollector(bool lightMode, bool profiling)
        {
            LightMode = lightMode;
            Profiling = profiling;
            if (Profiling)
            {
                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
        }

        private readonly Stopwatch _stopwatch;

        internal void Command(CommandBase command)
        {
            command._lightMode = LightMode;
            _traceCommands.Enqueue(command);
        }

        internal void Save(TimeSpan timeTaken, Exception exception)
        {
            Command(new Save() { TimeTaken = timeTaken, Exception = exception, _lightMode = LightMode});
        }

        internal Trace Finish()
        {
            if (Profiling) _stopwatch.Stop();
            Command(new End() { TimeTaken = Profiling ? _stopwatch.Elapsed : TimeSpan.Zero, _lightMode = LightMode });
            return new Trace(_traceCommands, LightMode);
        }

        public PromisedQuery<T> PromiseQuery<T>(Type channel)
        {
            var cmd = new QueryRecord(channel, false)
            {
                _lightMode = LightMode
            };
            Command(cmd);
            return new PromisedQuery<T>(cmd);
        }

        public PromisedQuery<T> PromiseTestQuery<T>(Type channel)
        {
            var cmd = new QueryRecord(channel, true)
            {
                _lightMode = LightMode
            };
            Command(cmd);
            return new PromisedQuery<T>(cmd);
        }
    }
}