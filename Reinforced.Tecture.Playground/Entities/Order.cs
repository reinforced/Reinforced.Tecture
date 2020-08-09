using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Playground.Entities
{
    public class Order : IPrimaryKey<int>
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
