using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Services
{
    /// <summary>
    /// Attribute for method that should not be explained in debug call chain
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class UnexplainableAttribute : Attribute
    {
    }
}
