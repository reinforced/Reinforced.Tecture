using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement
{
    public enum ResourceSupplyStatus
    {
        Draft,
        Open,
        BeingDelivered,
        Sorting,
        Closed
    }
    public class ResourceSupply : IPrimaryKey<int>, IEntity
    {
        internal ResourceSupply() { }

        public string Name { get; internal set; }

        public DateTime CreationDate { get; internal set; }

        public ResourceSupplyStatus Status { get; internal set; }

        public int ItemsCount { get; internal set; }
        
        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }

        public int Id { get; internal set; }
    }
}
