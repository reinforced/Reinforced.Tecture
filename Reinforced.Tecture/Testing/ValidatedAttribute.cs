using System;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Attribute that muse be placed on command's properties that must be validated
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatedAttribute : Attribute
    {
        public string Meaning { get; set; }

        /// <summary>
        /// Gets or sets opt-out flag of this particular validation
        /// </summary>
        public string OptOutFlag { get; set; }

        /// <summary>
        /// Creates validation attribute
        /// </summary>
        /// <param name="meaning"> {0} - property name, {1} - property expected value </param>
        public ValidatedAttribute(string meaning)
        {
            Meaning = meaning;
        }

        /// <summary>
        /// Creates validation attribute
        /// </summary>
        /// <param name="meaning"> {0} - property name, {1} - property expected value </param>
        /// <param name="optOutFlag"> Opt-out flag of this particular validation </param>
        public ValidatedAttribute(string meaning, string optOutFlag)
        {
            Meaning = meaning;
            OptOutFlag = optOutFlag;
        }

        public ValidatedAttribute()
        {
        }
    }
}