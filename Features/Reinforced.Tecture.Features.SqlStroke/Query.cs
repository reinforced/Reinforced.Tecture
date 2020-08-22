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

        internal readonly StrokeToolingWrapper Tooling;

        internal new Auxilary Aux
        {
            get { return base.Aux; }
        }
        protected Query(IStrokeRuntime runtime)
        {
            Tooling = new StrokeToolingWrapper(runtime);
        }
    }
}
