using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;


namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    partial class EfCore_Orm_CommandAspect : Reinforced.Tecture.Aspects.Orm.Orm.Command
    {
    private readonly ILazyDisposable<DbContext> _context;

    public EfCore_Orm_CommandAspect(ILazyDisposable<DbContext> context)
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
                .Where(x => x.PropertyType.IsGenericType &&
                            x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            var typeQuery = allDbSets
                .SelectMany(x => x.PropertyType.GetGenericArguments())
                .Any(x => x == t);
            return typeQuery;
        }
    }
    }
}
