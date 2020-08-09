using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Testing.Stories;

namespace Reinforced.Storage.Testing.SideEffects
{
    public class AsyncBulkSideEffectRunner : AssumptionEffectRunner<AsyncBulkSideEffect>
    {
        public AsyncBulkSideEffectRunner(TestingEnvironment env) : base(env)
        {
        }

        public StorageStory ToStory(AsyncBulkSideEffect bse, Func<DirectSqlSideEffect, int> assumption)
        {
            var q = new Queue<SideEffectBase>();
            var t = bse.Run("<% TMP TABLE %>", d =>
            {
                q.Enqueue(d);
                return Task.FromResult(assumption(d));
            });
            t.Wait();
            return new StorageStory(q, _env);
        }

        public StorageStory ToStory(AsyncBulkSideEffect bse)
        {
            return ToStory(bse, x => 0);
        }

        public AsyncBulkSideEffectRunner Assume<T>(BulkSimulation<T> simulation)
        {
            this.Assume(d => d.ElementType == typeof(T), (e, col) =>
            {
                simulation.Data = e.Data.Cast<T>();
                simulation.Collections = col;
                simulation.Sql = _env.Sqls;
                var story = ToStory(e, simulation.HandleCommand);
                simulation.ValidateSqlStory(story);
            });
            return this;
        }

        public AsyncBulkSideEffectRunner Assume<T>(string annotation, BulkSimulation<T> simulation)
        {
            this.Assume(d => d.ElementType == typeof(T) && d.Annotation == annotation, (e, col) =>
            {
                simulation.Data = e.Data.Cast<T>();
                simulation.Collections = col;
                simulation.Sql = _env.Sqls;
                var story = ToStory(e, simulation.HandleCommand);
                simulation.ValidateSqlStory(story);
            });
            return this;
        }
    }
}
