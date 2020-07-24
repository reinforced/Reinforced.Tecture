using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public abstract class Query : StrokeFeatureBase, QueryFeature
    {
        public abstract IEnumerable<T> DoQuery<T>(string command, object[] parameters);
        public abstract Task<IEnumerable<T>> DoQueryAsync<T>(string command, object[] parameters);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {

        }

        protected Query(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
