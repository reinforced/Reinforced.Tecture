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
        protected override string GetMessage(CommandBase command)
        {
            if (command == null) return $"expected command with annotation '{_requiredAnnotation}', but story unexpectedly ends";
            return $"expected command with annotation '{_requiredAnnotation}', but got with '{command.Annotation}' one";
        }

        /// <inheritdoc cref="CommandCheck{TCommand}.IsActuallyValid"/>
        protected override bool IsActuallyValid(CommandBase command)
        {
            if (command == null) return false;
            return command.Annotation == _requiredAnnotation;
        }


    }
}
