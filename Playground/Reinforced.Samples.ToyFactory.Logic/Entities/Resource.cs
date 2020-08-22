using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Entities
{
    public class Resource : IPrimaryKey<int>, IEntity
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }
    }
}
