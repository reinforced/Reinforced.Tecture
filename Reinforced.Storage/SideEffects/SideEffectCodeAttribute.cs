using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.SideEffects
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SideEffectCodeAttribute : Attribute
    {
        public string Code { get; }

        public SideEffectCodeAttribute(string code)
        {
            Code = code;
        }
    }
}
