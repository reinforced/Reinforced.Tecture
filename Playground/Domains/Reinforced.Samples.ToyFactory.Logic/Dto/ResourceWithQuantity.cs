using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Samples.ToyFactory.Logic.Dto
{
    /// <summary>
    /// now it is used for adding some resources to specified blueprint
    /// </summary>
    public class ResourceWithQuantity
    {
        public int ResourceId { get; set; }

        public double Quantity { get; set; }
    }
}
