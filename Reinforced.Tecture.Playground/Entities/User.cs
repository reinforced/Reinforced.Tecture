using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Playground.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
