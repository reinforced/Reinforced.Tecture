using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Stories
{
    public class EndCommand : CommandBase
    {
        internal EndCommand()
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
