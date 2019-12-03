using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories.Common
{
    public class AnnotationAssertion : SideEffectAssertion<SideEffectBase>
    {
        private readonly string _requiredAnnotation;

        public AnnotationAssertion(string requiredAnnotation)
        {
            _requiredAnnotation = requiredAnnotation;
        }

        public override string GetMessage(SideEffectBase effect)
        {
            if (effect== null) return $"expected effect with annotation '{_requiredAnnotation}', but story unexpectedly ended";
            return $"expected effect with annotation '{_requiredAnnotation}', but got with '{effect.Annotation}' one";
        }

        public override bool IsValid(SideEffectBase effect)
        {
            if (effect == null) return false;
            return effect.Annotation == _requiredAnnotation;
        }
    }
}
