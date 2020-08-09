using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories
{
    public class EndStoryAssertion : CommandCheck<EndStorySideEffect>
    {
        internal EndStoryAssertion()
        {
        }

        public override string GetMessage(EndStorySideEffect command)
        {
            return $"story unexpectedly ends";
        }

        public override bool IsActuallyValid(EndStorySideEffect effect)
        {
            return true;
        }
    }

    public class EndStorySideEffect : SideEffectBase
    {
        internal EndStorySideEffect()
        {
        }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write("::End of Story::");
        }
    }
}
