using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.DeletePk
{
    public static class DeletePKChecks
    {
        public static DeletePkKeyAndTypeCheck<T> DeleteByPK<T>(string explanation, params object[] keys) => new DeletePkKeyAndTypeCheck<T>(keys, explanation);
    }
}
