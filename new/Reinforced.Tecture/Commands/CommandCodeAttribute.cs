using System;

namespace Reinforced.Tecture.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandCodeAttribute : Attribute
    {
        public string Code { get; }

        public CommandCodeAttribute(string code)
        {
            Code = code;
        }
    }
}
