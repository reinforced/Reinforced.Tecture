using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyFactory.Logic.Entities
{
    /// <summary>
    /// Blueprint Id holder with FK to ToyType
    /// </summary>
    public class Blueprint : IPrimaryKey<int>, IEntity
    {
        public int Id { get; internal set; }

        public ToyType ToyType { get; internal set; }

        public int ToyTypeId { get; internal set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }
    }
}
