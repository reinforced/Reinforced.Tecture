using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Entities
{
    public class BlueprintResources : IPrimaryKey<int,int>
    {
        public Resource Resource { get; internal set; }

        public Blueprint Blueprint { get; internal set; }

        public int ResourceId { get; internal set; }

        public int BlueprintId { get; internal set; }

        public double Quantity { get; internal set; }

        public (Expression<Func<int>>, Expression<Func<int>>) PrimaryKey
        {
            get { return (() => ResourceId, () => BlueprintId); }
        }
    }
}
