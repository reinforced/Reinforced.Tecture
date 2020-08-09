using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Playground.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public DateTime? SoldDate { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }


        public string LanguageCode { get; set; }

        public int VendorId { get; set; }

        public virtual LanguageUser LanguageUser { get; set; }

        public int OrderDocId { get; set; }
        public virtual OrderDoc OrderDoc { get; set; }
    }
}
