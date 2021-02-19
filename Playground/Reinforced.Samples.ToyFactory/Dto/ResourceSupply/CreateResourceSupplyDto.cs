using System.Collections.Generic;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto;

namespace Reinforced.Samples.ToyFactory.Dto.ResourceSupply
{
    public class CreateResourceSupplyDto
    {
        public string Name { get; set; }
        public List<ResourceItemDto> Items { get; set; }
    }
}