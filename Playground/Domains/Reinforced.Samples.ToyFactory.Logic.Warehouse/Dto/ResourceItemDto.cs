using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto
{
    /// <summary>
    /// Using to add list of resources to warehouse
    /// </summary>
    public class ResourceItemDto
    {
        public string Name { get; set; }

        public int? Id { get; set; }

        public double Quantity { get; set; }
    }
}
