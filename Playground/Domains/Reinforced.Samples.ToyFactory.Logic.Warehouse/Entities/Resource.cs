using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities
{
    public class Resource : IPrimaryKey<int>, IEntity
    {
        internal Resource() { }

        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public double StockQuantity { get; internal set; }

        public int MeasurementUnitId { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }
    }
}
