using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.TraceWrapping
{
    internal interface ITracedQueryable<out T>
    {
        Orm.Query Aspect { get; }
        IQueryable<T> Original { get; }
        DescriptionHolder Description { get; }

        IQueryable<T> CreateNewOriginal(Expression cleanExpression = null);
    }
}