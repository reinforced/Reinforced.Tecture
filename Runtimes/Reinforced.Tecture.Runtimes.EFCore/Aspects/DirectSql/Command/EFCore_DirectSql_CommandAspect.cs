using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Runtime;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Command
{
    class EFCore_DirectSql_CommandAspect : Tecture.Aspects.DirectSql.DirectSql.Command
    {
        internal ILazyDisposable<DbContext> DbContext { get; }

        public EFCore_DirectSql_CommandAspect(ILazyDisposable<DbContext> dbContext, Type channel, InterpolatorFactory fac) 
            : base(new EfCoreStokeRuntime(dbContext, channel, fac))
        {
            DbContext = dbContext;
            
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        protected override HashSet<Type> ServingTypes
        {
            get
            {
                if (!Context.ProvidesTestData)
                {
                    return new HashSet<Type>(DbContext.Value.Model.GetEntityTypes().Select(x => x.ClrType));
                }
                else
                {
                    var tp = DbContext.ValueType;
                    var allDbSets = tp.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

                    var typeQuery = allDbSets
                        .SelectMany(x => x.PropertyType.GetGenericArguments());
                    return new HashSet<Type>(typeQuery);
                }
            }
        }
        
        
        /// <inheritdoc />
        protected override void OnRegister()
        {
            _runner = new DirectSqlRunner(this, Context);
        }

        /// <inheritdoc />
        protected override void Save()
        {
            if (!Context.ProvidesTestData)
            {
                DbContext.Value.SaveChanges();
            }
        }

        /// <inheritdoc />
        protected override Task SaveAsync(CancellationToken token = default)
        {
            if (!Context.ProvidesTestData)
            {
                return DbContext.Value.SaveChangesAsync(token);
            }

            return Task.FromResult(0);
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            DbContext.Dispose();
            _runner.Dispose();
        }

        private DirectSqlRunner _runner;


        /// <inheritdoc />
        protected override CommandRunner<Sql> GetRunner1(Sql command)
        {
            return _runner;
        }
    }
}
