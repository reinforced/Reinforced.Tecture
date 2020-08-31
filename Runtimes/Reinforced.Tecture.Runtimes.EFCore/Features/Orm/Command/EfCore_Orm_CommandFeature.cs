using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class EfCore_Orm_CommandFeature : Tecture.Features.Orm.Command
    {
        private readonly ILazyDisposable<DbContext> _context;

        public EfCore_Orm_CommandFeature(ILazyDisposable<DbContext> context)
        {
            _context = context;
        }

        protected override bool IsSubject(Type t)
        {
            if (Aux.IsEvaluationNeeded)
            {
                return _context.Value.Model.FindEntityType(t) != null;
            }
            else
            {
                var tp = _context.ValueType;
                var allDbSets = tp.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

                var typeQuery = allDbSets
                    .SelectMany(x => x.PropertyType.GetGenericArguments())
                    .Any(x => x == t);
                return typeQuery;
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}
