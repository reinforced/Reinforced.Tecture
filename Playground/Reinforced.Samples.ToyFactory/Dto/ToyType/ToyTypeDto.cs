namespace Reinforced.Samples.ToyFactory.Dto.ToyType
{ 
    /// <summary>
    /// since it's not possibe to serialize ToyType entity to json, here is DTO for it
    /// </summary>
    public class ToyTypeDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}