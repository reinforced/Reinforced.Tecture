using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Playground.Entities
{
    public class Order : EntityBase
    {
        
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Column("O_U_I")]
        public int OldUserId { get; set; }

        public virtual User OldUser { get; set; }

        public bool IsActive { get; set; }
        public ICollection<Item> Items { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Order()
        {
            Items = new HashSet<Item>();
        }

       
        [MaxLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the physical bin number. The number is assigned by user or system automatically.
        /// </summary>
        [MaxLength(15)]
        public string Number { get; set; }

        /// <summary>
        /// Dedicated - Specifies that quantities in the bin are protected from being picked for other demands. 
        /// Quantities in dedicated bins can still be reserved.
        /// </summary>
        public bool Dedicated { get; set; }

        public BinContentsMovementType ContentsMovementType { get; set; }
    }

    public enum BinContentsMovementType
    {
        [Display(Name = "No Restrictions")]
        NoRestrictions = 1,

        [Display(Name = "Inbound Block")]
        InboundBlock = 2,

        [Display(Name = "Outbound Block")]
        OutboundBlock = 3,

        [Display(Name = "Inbound & Outbound Block")]
        InboundOutboundBlock = 4
    }
}
