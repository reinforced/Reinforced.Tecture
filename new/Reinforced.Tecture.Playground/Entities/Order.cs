using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Playground.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public virtual User User { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
