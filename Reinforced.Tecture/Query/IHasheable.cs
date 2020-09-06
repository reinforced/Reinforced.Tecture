namespace Reinforced.Tecture.Query
{
    /// <summary>
    /// Hasheable entity 
    /// </summary>
    public interface IHasheable
    {
        /// <summary>
        /// Writes entity sensetive data into hashbox
        /// </summary>
        /// <param name="hb">Hashbox instance to write sensetive data to</param>
        void WriteData(Hashbox hb);
    }
}
