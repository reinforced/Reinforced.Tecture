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

    public interface IAddition<out T> { }

    public class Expected<T>
    {
        private readonly IAddition<IPrimaryKey<T>> _addition;
        internal Expected(IAddition<IPrimaryKey<T>> addition)
        {
            _addition = addition;
        }
        public T Key
        {
            get { return _addition.Key(); }
        }
    }
}
