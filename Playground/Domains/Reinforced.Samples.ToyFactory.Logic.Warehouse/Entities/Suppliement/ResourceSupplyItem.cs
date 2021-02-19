using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement
{
    /// <summary>
    /// Joining table for resource definition and resource in warehouse 
    /// </summary>
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
