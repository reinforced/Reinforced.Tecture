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
            return new Trace(new Queue<CommandBase>(cmds));
        }

        internal Trace(Queue<CommandBase> effects)
        {
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
            return new TraceValidator(this);
        }

        /// <summary>
        /// Turns story into human-readable text
        /// </summary>
        /// <param name="tw">Result writer</param>
        /// <param name="codes">Sets whether it is needed to output side-effect codes</param>
        public void Explain(TextWriter tw, Action<CommandBase,TextWriter> commandTemplate = null)
        {
            int i = 1;
            bool didCycleBegin = false;
            bool inCycle = false;
            foreach (var cmd in _commands)
            {
                if (cmd is EndCycle)
                {
                    didCycleBegin = false;
                    inCycle = false;
                }

                if (cmd is Cycle)
                {
                    didCycleBegin = true;
                }


                if (inCycle) continue;

                if (commandTemplate == null)
                {
                    if (!cmd.IsExecutable) tw.Write("   ");
                    else tw.Write(cmd.IsExecuted?"[v]":"[x]");
                    tw.Write($" {i}. ");

                    if (!string.IsNullOrEmpty(cmd.ChannelName)) tw.Write($"{cmd.ChannelName} ");
                    else tw.Write(" ");

                    var ca = cmd.GetType().GetTypeInfo().GetCustomAttribute<CommandCodeAttribute>();
                    tw.Write(ca != null ? $"{ca.Code}" : cmd.GetType().Name);

                    tw.Write(" ");
                    //tw.Write("\t");
    
                    cmd.Describe(tw);
                    tw.WriteLine();

                    if (cmd.Exception != null)
                    {
                        tw.WriteLine($"[ERROR] {cmd.Exception.Message}");   
                    }
                }
                else commandTemplate(cmd, tw);

                i++;

                if (cmd is Iteration && didCycleBegin)
                {
                    inCycle = true;
                }
            }
        }

        /// <summary>
        /// Turns story into human-readable text
        /// </summary>
        /// <param name="codes">Sets whether it is needed to output side-effect codes</param>
        /// <param name="commandTemplate">Template for command serialization</param>
        /// <returns>Story textual representation</returns>
        public string Explain(Action<CommandBase,TextWriter> commandTemplate = null)
        {
            StringBuilder sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                Explain(tw,commandTemplate);
                tw.Flush();
            }

            return sb.ToString();
        }
    }
}
