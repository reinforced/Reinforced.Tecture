using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Features.SqlStroke.Query
{
    public abstract class DirectSql : StrokeFeatureBase, QueryFeature
    {
        public abstract IEnumerable<T> Query<T>(string command, object[] parameters);
        public abstract Task<IEnumerable<T>> QueryAsync<T>(string command, object[] parameters);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {

        }

        protected DirectSql(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
