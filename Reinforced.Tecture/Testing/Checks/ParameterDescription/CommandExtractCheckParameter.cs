using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks.ParameterDescription
{
    public class CommandExtractCheckParameter : ICheckParameter
    {
        public Type Type { get; set; }

        // command => object
        public Delegate Extractor { get; set; }
    }
}
