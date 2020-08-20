using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Samples.ToyStory.Logic.Entities
{
    public class Blueprint : IPrimaryKey<int>
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
