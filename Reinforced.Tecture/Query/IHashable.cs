namespace Reinforced.Tecture.Query
{
    /// <summary>
    /// Hashable entity 
    /// </summary>
    public interface IHashable
    {
        /// <summary>
        /// Writes entity sensitive data into hashbox
        /// </summary>
        /// <param name="hb">Hashbox instance to write sensetive data to</param>
        void WriteData(Hashbox hb);
    }
}
