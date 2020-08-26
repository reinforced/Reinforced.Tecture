using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement
{
    public class ResourceSupplyItem : IPrimaryKey<int,int>
    {
        internal ResourceSupplyItem() { }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public int ResourceSupplyId { get; set; }

        public ResourceSupply ResourceSupply { get; set; }

        public double Quantity { get; set; }

        public (Expression<Func<int>>, Expression<Func<int>>) PrimaryKey
        {
            get { return (() => ResourceId, () => ResourceSupplyId); }
        }
    }
}
