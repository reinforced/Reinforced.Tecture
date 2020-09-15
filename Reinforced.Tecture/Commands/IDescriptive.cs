namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Entity that can describe itself
    /// </summary>
    public interface IDescriptive
    {
        /// <summary>
        /// Produces entity description
        /// </summary>
        /// <returns>Entity description</returns>
        string Describe();
    }
}
