using System.Linq;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Wrapped.Queryables
{
    internal interface IWrappedQueryable<out T>
    {
        Orm.Query Aspect { get; }
        IQueryable<T> Original { get; }
        DescriptionHolder Description { get; }
    }
}