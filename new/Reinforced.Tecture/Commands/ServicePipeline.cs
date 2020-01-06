using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Command pipeline context suitable for extensions
    /// </summary>
    public class ServicePipeline
    {
        private readonly Pipeline _pipeline;

        internal ServicePipeline(Pipeline pipeline)
        {
            _pipeline = pipeline;
        }

        private static readonly Type[] Empty = new Type[0];
        
        public virtual IEnumerable<Type> Subjects { get{return Empty;} }

        public virtual bool IsSubject(Type t)
        {
            return false;
        }

        public bool IsSubject<T>()
        {
            return IsSubject(typeof(T));
        }

        public void EnqueueCommand(CommandBase effect)
        {
            _pipeline.EnqueueCommand(effect);
        }

        public TCommand Enqueue<TCommand>(TCommand cmd) where TCommand : CommandBase
        {
            return _pipeline.Enqueue(cmd);
        }

        public CatchingCommands<T> Catch<T>(T sideEffectCatcher, string annotation = null) where T : ICommandCatcher
        {
            return _pipeline.Catch(sideEffectCatcher, annotation);
        }
    }

    public class ServicePipeline<T1> : ServicePipeline
    {
        public ServicePipeline(Pipeline pipeline) : base(pipeline)
        {
        }

        public override IEnumerable<Type> Subjects
        {
            get { yield return typeof(T1); }
        }

        public override bool IsSubject(Type t)
        {
            return t == typeof(T1);
        }
    }
}
