using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Playground.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public int Status { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
