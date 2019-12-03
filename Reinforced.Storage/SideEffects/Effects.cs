using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Reinforced.Storage.Services;
using Reinforced.Storage.Testing.Stories;

namespace Reinforced.Storage.SideEffects
{
    public class CatchingEffects<TCather> : IDisposable where TCather : ISideEffectsCatcher
    {
        private readonly Effects _queue;
        private readonly TCather _catcher;
        internal readonly string _annotation;
        /// <summary>
        /// Active side-effect catcher
        /// </summary>
        public TCather Catcher => _catcher;

        internal CatchingEffects(TCather catcher, Effects queue, string annotation)
        {
            _catcher = catcher;
            _queue = queue;
            _annotation = annotation;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => _queue.FinishCatch(this);
    }
    public class Effects
    {

        private readonly Queue<SideEffectBase> _sideEffectQueue = new Queue<SideEffectBase>();

        public void EnqueueEffect(SideEffectBase effect)
        {
            if (_debugMode)
            {
                DebugInfo dbg = null;
                var st = new StackTrace();
                foreach (var stf in st.GetFrames())
                {
                    var m = stf.GetMethod();
                    if (m != null && m.DeclaringType != null && typeof(StorageService).IsAssignableFrom(m.DeclaringType))
                    {
                        if (m.GetCustomAttribute<UnexplainableAttribute>() != null) continue;
                        dbg = new DebugInfo();
                        dbg.SourceService = m.DeclaringType;
                        dbg.SourceMethod = m;
                        dbg.LineNumber = stf.GetFileLineNumber();
                        dbg.FileName = stf.GetFileName();
                        break;
                    }
                }

                effect.Debug = dbg;
            }
            _sideEffectQueue.Enqueue(effect);
        }

        public TEffect Enqueue<TEffect>(TEffect effect) where TEffect : SideEffectBase
        {
            EnqueueEffect(effect);
            return effect;
        }


        private readonly bool _debugMode;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal Effects(bool debugMode)
        {
            _debugMode = debugMode;
        }

        private readonly Stack<ISideEffectsCatcher> _catchersStack = new Stack<ISideEffectsCatcher>();

        public CatchingEffects<T> Catch<T>(T sideEffectCatcher, string annotation = null) where T : ISideEffectsCatcher
        {
            _catchersStack.Push(sideEffectCatcher);
            return new CatchingEffects<T>(sideEffectCatcher, this, annotation);
        }

        internal void FinishCatch<T>(CatchingEffects<T> catching) where T : ISideEffectsCatcher
        {
            var c = _catchersStack.Pop();
            if (c != (ISideEffectsCatcher)catching.Catcher)
                throw new Exception("Side-effects catchers stack imbalance");

            if (_catchersStack.Count == 0) EnqueueEffect(c.Produce().Annotate(catching._annotation));
            else _catchersStack.Peek().Catch(c.Produce().Annotate(catching._annotation));
        }
        internal IEnumerable<SideEffectBase> GetEffects()
        {
            var nq = new Queue<SideEffectBase>(_sideEffectQueue);
            _sideEffectQueue.Clear();
            while (nq.Count > 0)
            {
                yield return nq.Dequeue();
            }
        }

        internal bool HasEffects
        {
            get { return _sideEffectQueue.Count > 0; }
        }

        
    }
}
