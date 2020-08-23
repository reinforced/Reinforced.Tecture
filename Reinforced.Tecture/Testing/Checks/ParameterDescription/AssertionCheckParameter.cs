using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks.ParameterDescription
{
    public class AssertionCheckParameter : ICheckParameter
    {
        public Type Type { get; set; }
        public Delegate Extractor { get; set; }
        public PropertyInfo[] PropertiesToAssert { get; set; }
    }
}
