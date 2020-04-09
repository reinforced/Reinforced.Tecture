using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Testing.Assumptions;

namespace Reinforced.Tecture.Testing
{
    public static class Extensions
    {
        /// <summary>
        /// Adds testing runtime to testing environment
        /// </summary>
        /// <param name="env">Testing environment</param>
        /// <param name="runtime">Testing runtime</param>
        /// <returns>Fluent</returns>
        public static TestingEnvironment WithTestRuntime(this TestingEnvironment env, ITestingRuntime runtime)
        {
            env._mx.AddRuntime(runtime);
            return env;
        }

        public static TestingEnvironment Assume(this TestingEnvironment env, Action<Assuming> assumptions)
        {
            if (assumptions != null) assumptions(env._assumptions);
            return env;
        }
    }
}
