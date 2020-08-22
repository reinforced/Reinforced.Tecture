using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Entities
{
    public class ToyType : IPrimaryKey<int>, IEntity, IDescriptive
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }

        public string Descibe()
        {
            return $"toy type {Name}";
        }
    }
}
