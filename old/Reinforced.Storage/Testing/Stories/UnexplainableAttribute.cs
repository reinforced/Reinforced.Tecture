using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class UnexplainableAttribute : Attribute
    {
    }
}
