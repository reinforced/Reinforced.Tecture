using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Services
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class UnexplainableAttribute : Attribute
    {
    }
}
