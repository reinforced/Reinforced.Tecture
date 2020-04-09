using System;
using System.Collections.Generic;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Command pipeline context suitable for extensions
    /// </summary>
    public class ServicePipeline : IRuntimeLocator
    {
        internal readonly Pipeline CorePipeline;

        internal ServicePipeline(Pipeline corePipeline)
        {
            CorePipeline = corePipeline;
        }
        
        /// <summary>
        /// Await point to split actions before/after savechanges call
        /// </summary>
        public ActionsQueueTask Save
        {
            get { return new ActionsQueueTask(CorePipeline.PostSaveActions); }
        }

        /// <summary>
        /// Await point to split actions that must happen after everything
        /// </summary>
        public ActionsQueueTask Final
        {
            get { return new ActionsQueueTask(CorePipeline.FinallyActions); }
        }

        private static readonly Type[] Empty = new Type[0];

        public virtual IEnumerable<Type> Subjects { get { return Empty; } }

        public virtual bool IsSubject(Type t)
        {
            return false;
        }

        public bool IsSubject<T>()
        {
            return IsSubject(typeof(T));
        }

        public TCommand Enqueue<TCommand>(TCommand cmd) where TCommand : CommandBase
        {
            return CorePipeline.Enqueue(cmd);
        }

        public CatchingCommands<T> Catch<T>(T sideEffectCatcher, string annotation = null) where T : ICommandCatcher
        {
            return CorePipeline.Catch(sideEffectCatcher, annotation);
        }

        public IEnumerable<TRuntime> GetRuntimes<TRuntime>(Func<TRuntime, bool> predicate = null)
            where TRuntime : ITectureRuntime
        {
            return CorePipeline._locator.GetRuntimes(predicate);
        }
    }

    public class ServicePipeline<T1> : ServicePipeline
    {
        public ServicePipeline(Pipeline corePipeline) : base(corePipeline)
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
