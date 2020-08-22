using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Reinforced.Tecture.Features.Orm.PrimaryKey
{
    public interface IPrimaryKey
    {
        
    }

    public interface IPrimaryKey<T> : IPrimaryKey
    {
        Expression<Func<T>> PrimaryKey { get; }
    }

    /// <summary>
    /// Minimal interface describing addition of entity with ability to retrieve primary key later
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAddition<out T> { }
}
