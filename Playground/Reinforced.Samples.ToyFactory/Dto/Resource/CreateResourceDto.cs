using Reinforced.Samples.ToyFactory.Dto.ToyType;

namespace Reinforced.Samples.ToyFactory.Dto.Resource
{
    public class CreateResourceDto :CreateToyTypeDto
    {
        public string MeasurementUnit { get; set; }
    }
}