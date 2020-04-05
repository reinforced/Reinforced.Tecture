using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories.Common
{
    public class AnnotationAssertion : CommandCheck<SideEffectBase>
    {
        private readonly string _requiredAnnotation;

        public AnnotationAssertion(string requiredAnnotation)
        {
            _requiredAnnotation = requiredAnnotation;
        }

        public override string GetMessage(SideEffectBase command)
        {
            if (command== null) return $"expected effect with annotation '{_requiredAnnotation}', but story unexpectedly ended";
            return $"expected effect with annotation '{_requiredAnnotation}', but got with '{command.Annotation}' one";
        }

        public override bool IsActuallyValid(SideEffectBase effect)
        {
            if (effect == null) return false;
            return effect.Annotation == _requiredAnnotation;
        }
    }
}
