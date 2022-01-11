using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Tracing.Commands.Cycles;

namespace Reinforced.Tecture.Tracing
{
    /// <summary>
    /// Story of applied side effects.
    /// Story can be shown as text or formally validated.
    /// You cannot construct story directly
    /// </summary>
    public class Trace
    {
        /// <summary>
        /// Gets whether trace is light, so does not contain enough data to generate test data or validation
        /// </summary>
        public bool IsLightTrace { get; private set; }
        
        private readonly CommandBase[] _commands;

        /// <summary>
        /// Effects that story consists of (order matters)
        /// </summary>
        public IEnumerable<CommandBase> Commands
        {
            get { return _commands.Where(x => !(x is ITracingOnly)).Cast<CommandBase>(); }
        }

        /// <summary>
        /// Effects that story consists of (order matters)
        /// </summary>
        public IEnumerable<CommandBase> All
        {
            get { return _commands; }
        }

        /// <summary>
        /// Recorded queries
        /// </summary>
        public IEnumerable<QueryRecord> Queries
        {
            get { return _commands.Where(x => x is QueryRecord).Cast<QueryRecord>(); }
        }

        /// <summary>
        /// Extracts trace of particular data channel
        /// </summary>
        /// <typeparam name="T">Type of channel</typeparam>
        /// <returns>Trace containing only entries related to specified channel</returns>
        public Trace OfChannel<T>() where T : Channel
        {
            var fn = typeof(T).FullName;
            var cmds = _commands.Where(x => x.ChannelId == fn || x is Save || x is End);
            return new Trace(new Queue<CommandBase>(cmds),IsLightTrace);
        }

        internal Trace(Queue<CommandBase> effects, bool isLightTrace)
        {
            IsLightTrace = isLightTrace;
            var nq = new Queue<CommandBase>(effects);
            CommandBase[] effectsArray = new CommandBase[effects.Count];
            int i = 0;
            while (nq.Count > 0)
            {
                effectsArray[i++] = nq.Dequeue();
            }

            _commands = effectsArray;
        }

        /// <summary>
        /// Begins story validation
        /// </summary>
        /// <returns>Story validator</returns>
        public TraceValidator Begins()
        {
            if (IsLightTrace)
                throw new TectureException("Cannot validate light trace");
            return new TraceValidator(this);
        }

        /// <summary>
        /// Turns trace into human-readeable text
        /// </summary>
        /// <returns>String containing trace explanation</returns>
        public string Explain()
        {
            var explainer = new TraceExplainer();
            explainer.ExplainTrace(this);
            return explainer.ToString();
        }
        
        /// <summary>
        /// Turns trace into human-readeable text using specified trace explainer
        /// </summary>
        /// <param name="explainer">Trace explainer</param>
        /// <returns></returns>
        public void Explain(TraceExplainer explainer)
        {
            explainer.ExplainTrace(this);
        }
    }
}