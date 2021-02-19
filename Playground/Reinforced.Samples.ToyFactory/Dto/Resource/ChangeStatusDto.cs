using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;

namespace Reinforced.Samples.ToyFactory.Dto.Resource
{
    public class ChangeStatusDto
    {
        public int Id { get; set; }
        public ResourceSupplyStatus NewStatus { get; set; }
    }
}