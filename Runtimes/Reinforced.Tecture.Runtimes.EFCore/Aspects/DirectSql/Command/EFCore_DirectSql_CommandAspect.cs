using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Runtime;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Command
{
    class EFCore_DirectSql_CommandAspect : Tecture.Aspects.DirectSql.DirectSql.Command
    {
        internal ILazyDisposable<DbContext> Context { get; }

        public EFCore_DirectSql_CommandAspect(ILazyDisposable<DbContext> context, Type channel, InterpolatorFactory fac) 
            : base(new EfCoreStokeRuntime(context, channel, fac))
        {
            Context = context;
            
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        protected override HashSet<Type> ServingTypes
        {
            get
            {
                if (Aux.IsCommandRunNeeded || Aux.IsEvaluationNeeded)
                {
                    return new HashSet<Type>(Context.Value.Model.GetEntityTypes().Select(x => x.ClrType));
                }
                else
                {
                    var tp = Context.ValueType;
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
            _runner = new DirectSqlRunner(this, Aux);
        }

        /// <inheritdoc />
        protected override void Save()
        {
            if (Aux.IsSavingNeeded)
            {
                Context.Value.SaveChanges();
            }
        }

        /// <inheritdoc />
        protected override Task SaveAsync()
        {
            if (Aux.IsSavingNeeded)
            {
                return Context.Value.SaveChangesAsync();
            }

            return Task.FromResult(0);
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            Context.Dispose();
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
