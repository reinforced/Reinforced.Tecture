using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities
{
    public class MeasurementUnit : IPrimaryKey<int>, IEntity
    {
        public int Id { get; internal set; }

        public string ShortName { get; internal set; }

        public string Name { get; internal set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }
    }
}
