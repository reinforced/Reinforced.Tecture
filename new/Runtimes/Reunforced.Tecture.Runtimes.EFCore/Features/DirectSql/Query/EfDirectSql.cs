using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Features.SqlStroke;

namespace Reunforced.Tecture.Runtimes.EFCore.Features.Query
{
    class EfDirectSql : DirectSql
    {
        public override IEnumerable<T> Query<T>(string command, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<T>> QueryAsync<T>(string command, object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public EfDirectSql(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
