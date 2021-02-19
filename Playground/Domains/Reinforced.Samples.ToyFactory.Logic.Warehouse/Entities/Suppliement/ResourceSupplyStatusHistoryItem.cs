using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement
{
    /// <summary>
    /// Change tracker for resources in warehouse
    /// </summary>
    public class ResourceSupplyStatusHistoryItem : IPrimaryKey<int>, IEntity
    {
        internal ResourceSupplyStatusHistoryItem() { }

        public ResourceSupplyStatus? Previous { get; set; }

        public ResourceSupplyStatus Next { get; set; }

        public string Comment { get; set; }
        public DateTime CreatedDate { get; internal set; }

        public int ResourceSupplyId { get; internal set; }

        public ResourceSupply ResourceSupply { get; internal set; }

        public Expression<Func<int>> PrimaryKey { get { return () => Id; } }
        public int Id { get; internal set; }
    }
}
