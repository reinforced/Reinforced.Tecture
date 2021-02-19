using System.Collections.Generic;
using Reinforced.Samples.ToyFactory.Logic.Dto;

namespace Reinforced.Samples.ToyFactory.Dto.Blueprint
{
    public class AddResourceDto
    {
        public int BlueprintId { get; set; }
        public List<ResourceWithQuantity> Resources { get; set; }
    }
}