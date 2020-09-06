using System;

namespace Reinforced.Tecture.Testing.Checks
{
    /// <summary>
    /// Checks annotator helper (to be extended)
    /// </summary>
    public interface IAnnotator
    {
        /// <summary>
        /// Makes unit test generator to produce assertions function on specified command field
        /// </summary>
        /// <param name="commandField">Command field</param>
        /// <returns>Assertions generation mark</returns>
        Func<object,bool> Assertions(object commandField);
    }
}
