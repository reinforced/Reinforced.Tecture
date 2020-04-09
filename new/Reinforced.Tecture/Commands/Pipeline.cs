using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Reinforced.Storage.Services;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Commands
{
    
    public class Pipeline
    {
        internal RuntimeMultiplexer _locator;
        private readonly Queue<CommandBase> _commandQueue = new Queue<CommandBase>();
        
        public void EnqueueCommand(CommandBase effect)
        {
            if (_debugMode)
            {
                DebugInfo dbg = null;
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

                effect.Debug = dbg;
            }
            _commandQueue.Enqueue(effect);
        }

        public TCommand Enqueue<TCommand>(TCommand cmd) where TCommand : CommandBase
        {
            EnqueueCommand(cmd);
            return cmd;
        }


        private readonly bool _debugMode;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal Pipeline(bool debugMode, RuntimeMultiplexer locator)
        {
            _debugMode = debugMode;
            _locator = locator;
        }

        private readonly Stack<ICommandCatcher> _catchersStack = new Stack<ICommandCatcher>();

        public CatchingCommands<T> Catch<T>(T sideEffectCatcher, string annotation = null) where T : ICommandCatcher
        {
            _catchersStack.Push(sideEffectCatcher);
            return new CatchingCommands<T>(sideEffectCatcher, this, annotation);
        }

        internal void FinishCatch<T>(CatchingCommands<T> catching) where T : ICommandCatcher
        {
            var c = _catchersStack.Pop();
            if (c != (ICommandCatcher)catching.Catcher)
                throw new Exception("Command catchers stack imbalance");

            if (_catchersStack.Count == 0) EnqueueCommand(c.Produce().Annotate(catching._annotation));
            else _catchersStack.Peek().Catch(c.Produce().Annotate(catching._annotation));
        }
        internal IEnumerable<CommandBase> GetEffects()
        {
            var nq = new Queue<CommandBase>(_commandQueue);
            _commandQueue.Clear();
            while (nq.Count > 0)
            {
                yield return nq.Dequeue();
            }
        }

        internal bool HasEffects => _commandQueue.Count > 0;
    }
}
