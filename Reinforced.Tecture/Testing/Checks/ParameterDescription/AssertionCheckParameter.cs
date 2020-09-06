using System;
using System.Reflection;

namespace Reinforced.Tecture.Testing.Checks.ParameterDescription
{
    /// <summary>
    /// Check parameter that will be converted to the code of assertion delegate
    /// </summary>
    public class AssertionCheckParameter : ICheckParameter
    {
        /// <summary>
        /// Type of type to be asserted
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Delegate consuming command and returning object to assert on
        /// </summary>
        public Delegate Extractor { get; set; }

        /// <summary>
        /// List of properties to assert on
        /// </summary>
        public PropertyInfo[] PropertiesToAssert { get; set; }
    }
}
