using System;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Aspects.Orm.PrimaryKey
{
    /// <summary>
    /// Entity with primary key
    /// </summary>
    public interface IPrimaryKey { }

    /// <summary>
    /// Entity with singular primary key
    /// </summary>
    /// <typeparam name="T">Type of primary key</typeparam>
    public interface IPrimaryKey<T> : IPrimaryKey
    {
        /// <summary>
        /// Gets property expression that participates primary key
        /// </summary>
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
