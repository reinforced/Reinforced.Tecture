using System;

namespace Reinforced.Tecture.CleanPlayground.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CloseDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}