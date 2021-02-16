using System;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Simple command code for quick distinguish
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandCodeAttribute : Attribute
    {
        /// <summary>
        /// Gets command code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Constructs instance of command code attribute
        /// </summary>
        /// <param name="code">Command code</param>
        public CommandCodeAttribute(string code)
        {
            Code = code;
        }
    }
}
