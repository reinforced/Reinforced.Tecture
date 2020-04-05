using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Testing.Stories;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
{
    public class EndStoryCheck : CommandCheck<EndCommand>
    {
        internal EndStoryCheck(){}

        protected override string GetMessage(EndCommand command)
        {
            return $"story unexpectedly ends";
        }

        protected override bool IsActuallyValid(EndCommand command)
        {
            return true;
        }
    }
}
