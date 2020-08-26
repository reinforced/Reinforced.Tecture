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
    /// <typeparam name="T">Entity that is going to be added</typeparam>
    public interface IAddition<out T> { }

    /// <summary>
    /// Primary key-related operation on entity type and command type
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    /// <typeparam name="T">EntityType</typeparam>
    public interface IPrimaryKeyOperation<TCommand, out T> { }
}
