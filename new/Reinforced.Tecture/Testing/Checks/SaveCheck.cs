using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Testing.Stories;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
{
    public class SaveCheck : CommandCheck<SaveCommand>
    {
        internal SaveCheck() { }

        protected override string GetMessage(SaveCommand command)
        {
            return "expected saving to storage, but something went wrong";
        }

        protected override bool IsActuallyValid(SaveCommand effect)
        {
            return true;
        }
    }
}
