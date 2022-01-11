using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Commands
{
    class Pipeline
    {
        private readonly object QueueLocker = new object();

        private readonly Queue<CommandBase> _commandQueue = new Queue<CommandBase>();


        internal TraceCollector TraceCollector = null;

        /// <summary>
        /// Gets count of commands in queue
        /// </summary>
        public int CommandsCount => _commandQueue.Count;

        public void EnqueueCommand(CommandBase cmd)
        {
            lock (QueueLocker)
            {
                cmd.Order = _commandQueue.Count + 1;
                if (_debugMode)
                {
                    //DebugInfo dbg = null;
                    //var st = new StackTrace();
                    //foreach (var stf in st.GetFrames())
                    //{
                    //    var m = stf.GetMethod();
                    //    if (m != null && m.DeclaringType != null && typeof(TectureService).IsAssignableFrom(m.DeclaringType))
                    //    {
                    //        //if (m.GetCustomAttribute<UnexplainableAttribute>() != null) continue;
                    //        dbg = new DebugInfo();
                    //        dbg.SourceService = m.DeclaringType;
                    //        dbg.SourceMethod = m;
                    //        dbg.LineNumber = stf.GetFileLineNumber();
                    //        dbg.FileName = stf.GetFileName();
                    //        break;
                    //    }
                    //}

                    //cmd.Debug = dbg;
                }

                if (TraceCollector?.LightMode == true)
                {
                    cmd._lightMode = true;
                    TraceCollector?.Command(cmd);
                }
                else
                {
                    TraceCollector?.Command(cmd.TraceClone());
                }

                if (!(cmd is ITracingOnly)) _commandQueue.Enqueue(cmd);
            }
        }

        public TCommand Enqueue<TCommand>(TCommand cmd) where TCommand : CommandBase
        {
            EnqueueCommand(cmd);
            return cmd;
        }

        internal ActionsQueue PostSaveActions { get; private set; }
        internal ActionsQueue FinallyActions { get; private set; }

        private readonly bool _debugMode;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal Pipeline(bool debugMode, ActionsQueue postSaveActions, ActionsQueue finallyActions)
        {
            _debugMode = debugMode;
            PostSaveActions = postSaveActions;
            FinallyActions = finallyActions;
        }

        internal IEnumerable<CommandBase> GetEffects()
        {
            var nq = new Queue<CommandBase>(_commandQueue);
            lock (QueueLocker)
            {
                _commandQueue.Clear();
            }

            while (nq.Count > 0)
            {
                yield return nq.Dequeue();
            }
        }

        internal bool HasEffects => _commandQueue.Count > 0;
    }
}