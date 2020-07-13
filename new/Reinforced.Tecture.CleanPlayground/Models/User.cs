using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.CleanPlayground.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
