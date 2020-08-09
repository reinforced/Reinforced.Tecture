using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Common
{
    public class SaveAssertion : CommandCheck<SaveChangesSideEffect>
    {
        public override string GetMessage(SaveChangesSideEffect command)
        {
            return "expected saving to storage, but something went wrong";
        }

        public override bool IsActuallyValid(SaveChangesSideEffect effect)
        {
            return true;
        }
    }
}
