using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Query
{
    class EfCore_Orm_QueryAspect : Tecture.Aspects.Orm.Query
    {
        private readonly ILazyDisposable<DbContext> _context;

        public EfCore_Orm_QueryAspect(ILazyDisposable<DbContext> context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves queryable set
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>Queryable set of entities</returns>
        protected override IQueryable<T> Set<T>()
        {
            return _context.Value.Set<T>();
        }

        /// <summary>
        /// Gets asynchronous query executor instance
        /// </summary>
        protected override IAsyncQueryExecutor AsyncExecutor {
            get
            {
                return EfCoreAsyncExecutor.Instance;
            }
        }

        /// <summary>
        /// Returns key of just added entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addCommand">Addition command</param>
        /// <param name="keyProperties">Key property</param>
        /// <returns></returns>
        protected override IEnumerable<object> GetKey(Add addCommand, IEnumerable<PropertyInfo> keyProperties)
        {
            var e = addCommand.Entity;

            foreach (var propertyInfo in keyProperties)
            {
                yield return propertyInfo.GetValue(e);
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}
