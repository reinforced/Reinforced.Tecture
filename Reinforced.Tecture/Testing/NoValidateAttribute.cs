using System;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// This attribute explicitly forbids to generate validation for particular properties of objects
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NoValidateAttribute : Attribute
    {
        
    }
}