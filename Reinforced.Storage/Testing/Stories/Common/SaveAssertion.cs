using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Common
{
    public class SaveAssertion : SideEffectAssertion<SaveChangesSideEffect>
    {
        public override string GetMessage(SaveChangesSideEffect effect)
        {
            return "expected saving to storage, but something went wrong";
        }

        public override bool IsValid(SaveChangesSideEffect effect)
        {
            return true;
        }
    }
}
