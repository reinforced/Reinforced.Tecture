using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Integrate
{
    /// <summary>
    /// Tecture declarations container
    /// </summary>
    public class Registry
    {
        private readonly IResolve _resolve;

        internal Registry(IResolve resolve)
        {
            _resolve = resolve;
        }

        private object EnsureResolve(Type t)
        {
            object result = null;
            try
            {
                result = _resolve.GetInstance(t);
            }
            catch (Exception e)
            {
                throw new TectureException($"Exception when trying to resolve {t.FullName}",e);
            }

            if (result == null)
            {
                throw new TectureException($"Type {t.FullName} resolved to null instance. Please check that {t.FullName} is registered in your resolver");
            }

            return result;
        }

        private readonly HashSet<Type> _saverTypes = new HashSet<Type>();
        private readonly HashSet<Type> _sourceTypes = new HashSet<Type>();

        private readonly Dictionary<Type, Type> _commandRunners
            = new Dictionary<Type, Type>();

        public void DeclareSaver<TSaver>() where TSaver : ISaver
        {
            _saverTypes.Add(typeof(TSaver));
        }

        public void DeclareRunner<TCommand, TCommandRunner>()
            where TCommand : CommandBase
            where TCommandRunner : ICommandRunner<TCommand>
        {
            _commandRunners[typeof(TCommand)] = typeof(TCommandRunner);
        }

        public void DeclareSource<TSource>() where TSource : ISource
        {
            _sourceTypes.Add(typeof(TSource));
        }

        private ISaver[] _saversInst = null;
        public ISaver[] GetSavers()
        {
            return _saversInst 
                   ?? (_saversInst = _saverTypes
                       .Select(EnsureResolve)
                       .Cast<ISaver>()
                       .ToArray());
        }

        private readonly Dictionary<Type,ICommandRunner> _runnersInst
            = new Dictionary<Type, ICommandRunner>();

        public ICommandRunner GetRunner(Type commandType)
        {
            
            if (!_commandRunners.ContainsKey(commandType))
            {
                throw new TectureException($"Runner for command {commandType.Name} has not been declared");
            }

            if (!_runnersInst.ContainsKey(commandType))
            {
                _runnersInst[commandType] = 
                    (ICommandRunner) EnsureResolve(_commandRunners[commandType]);
            }

            return _runnersInst[commandType];
        }

        public ICommandRunner<TCommand> GetRunner<TCommand>() where TCommand : CommandBase
        {
            return (ICommandRunner<TCommand>)GetRunner(typeof(TCommand));
        }

        private readonly Dictionary<Type, ISource> _sourceInst = new Dictionary<Type, ISource>();

        public TSource GetSource<TSource>() where TSource : ISource
        {
            if (!_sourceTypes.Contains(typeof(TSource)))
            {
                throw new TectureException($"Data source of type {typeof(TSource).FullName} has not been declared");
            }

            if (!_sourceInst.ContainsKey(typeof(TSource)))
            {
                _sourceInst[typeof(TSource)] = (ISource) EnsureResolve(typeof(TSource));
            }

            return (TSource) _sourceInst[typeof(TSource)];
        }

        public void Clear()
        {
            _sourceInst.Clear();
            _runnersInst.Clear();
            _saversInst = null;
        }
    }
}
