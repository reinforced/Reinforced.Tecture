using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Entities
{
    enum ResourceSupplyStatus
    {
        Draft,
        Open,
    }
    public class ResourceSupply : IPrimaryKey<int>, IEntity
    {
        public string Name { get; internal set; }

        public DateTime CreationDate { get; set; }
        
        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }

        public int Id { get; }
    }
}
