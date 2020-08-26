using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.SqlStroke.Testing.Checks
{
    public static class SqlChecks
    {
        public static SqlCommandTextCheck SqlCommand(string command) => new SqlCommandTextCheck(command);

        public static SqlCommandParametersCheck SqlParameters(params object[] p) => new SqlCommandParametersCheck(p);
    }

    
}
