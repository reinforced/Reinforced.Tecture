using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public abstract class Query : QueryFeature
    {
        public abstract IEnumerable<T> DoQuery<T>(string command, object[] parameters) where T : class;
        public abstract Task<IEnumerable<T>> DoQueryAsync<T>(string command, object[] parameters) where T : class;

        private StrokeToolingWrapper _tooling;
        internal StrokeToolingWrapper Tooling
        {
            get
            {
                if (_tooling == null)
                {
                    _tooling = new StrokeToolingWrapper(_runtime, Aux, ServingTypes);
                }

                return _tooling;
            }
        }

        private readonly IStrokeRuntime _runtime;
        internal new Auxilary Aux
        {
            get { return base.Aux; }
        }
        protected Query(IStrokeRuntime runtime)
        {
            _runtime = runtime;
        }
        
        protected abstract HashSet<Type> ServingTypes { get; }
    }
}
