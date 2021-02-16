using System;

namespace Reinforced.Tecture.Testing.Checks.ParameterDescription
{
    /// <summary>
    /// Check parameter that will be converted to the code of constant extracted with value extracted from particular command
    /// </summary>
    public class CommandExtractCheckParameter : ICheckParameter
    {
        /// <summary>
        /// Constant type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Delegate consuming command instance and returning object to be inlined into test code as constant
        /// </summary>
        public Delegate Extractor { get; set; }
    }
}
