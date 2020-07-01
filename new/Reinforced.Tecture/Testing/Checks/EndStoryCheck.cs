using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Testing.Stories;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
{
    public class EndStoryCheck : CommandCheck<End>
    {
        internal EndStoryCheck(){}

        protected override string GetMessage(End command)
        {
            return $"story unexpectedly ends";
        }

        protected override bool IsActuallyValid(End command)
        {
            return true;
        }
    }
}
