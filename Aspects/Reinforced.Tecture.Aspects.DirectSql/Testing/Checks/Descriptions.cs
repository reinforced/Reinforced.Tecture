using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;

namespace Reinforced.Tecture.Aspects.DirectSql.Testing.Checks
{
    class SqlCommandTextCheckDescription : CheckDescription<Sql>
    {
        public override MethodInfo Method => UseMethod((a, c) => SqlChecks.SqlCommand(c.Preview.Query));
    }

    class SqlCommandParmatersCheckDescription : CheckDescription<Sql>
    {
        public override MethodInfo Method => UseMethod((a, c) => SqlChecks.SqlParameters(c.Preview.Parameters));

        private static object Parameter1(Sql cmd)
        {
            return cmd.Preview.Parameters[0];
        }
        private static object Parameter2(Sql cmd)
        {
            return cmd.Preview.Parameters[1];
        }
        private static object Parameter3(Sql cmd)
        {
            return cmd.Preview.Parameters[2];
        }
        private static object Parameter4(Sql cmd)
        {
            return cmd.Preview.Parameters[3];
        }
        private static object Parameter5(Sql cmd)
        {
            return cmd.Preview.Parameters[4];
        }
        private static object Parameter6(Sql cmd)
        {
            return cmd.Preview.Parameters[5];
        }
        private static object Parameter7(Sql cmd)
        {
            return cmd.Preview.Parameters[6];
        }
        private static object Parameter8(Sql cmd)
        {
            return cmd.Preview.Parameters[7];
        }
        private static Func<Sql, object> NthParameter(int n)
        {
            if (n == 0) return Parameter1;
            if (n == 1) return Parameter2;
            if (n == 2) return Parameter3;
            if (n == 3) return Parameter4;
            if (n == 4) return Parameter5;
            if (n == 5) return Parameter6;
            if (n == 6) return Parameter7;
            if (n == 7) return Parameter8;

            object Result(Sql s) => s.Preview.Parameters[n];
            return Result;
        }
        protected override IEnumerable<ICheckParameter> GetCheckParameters(Sql commandBase)
        {
            var p = commandBase.Preview.Parameters;
            int n = 0;
            foreach (var o in p)
            {
                yield return new CommandExtractCheckParameter()
                {
                    Extractor = NthParameter(n),
                    Type = o == null ? typeof(string) : o.GetType()
                };
                n++;
            }
        }
    }
}
