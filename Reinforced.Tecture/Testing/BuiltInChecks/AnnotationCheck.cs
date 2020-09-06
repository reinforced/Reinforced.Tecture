using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    /// <summary>
    /// Performs command's annotation check
    /// </summary>
    public class AnnotationCheck : CommandCheck<CommandBase>
    {
        private readonly string _requiredAnnotation;

        internal AnnotationCheck(string requiredAnnotation)
        {
            _requiredAnnotation = requiredAnnotation;
        }

        /// <inheritdoc cref="CommandCheck{TCommand}.GetMessage"/>
        protected override string GetMessage(CommandBase effect)
        {
            if (effect == null) return $"expected effect with annotation '{_requiredAnnotation}', but story unexpectedly ended";
            return $"expected effect with annotation '{_requiredAnnotation}', but got with '{effect.Annotation}' one";
        }

        /// <inheritdoc cref="CommandCheck{TCommand}.IsActuallyValid"/>
        protected override bool IsActuallyValid(CommandBase effect)
        {
            if (effect == null) return false;
            return effect.Annotation == _requiredAnnotation;
        }


    }
}
