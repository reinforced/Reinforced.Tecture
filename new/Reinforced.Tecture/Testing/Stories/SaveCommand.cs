using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Stories
{
    [CommandCode("SAVE")]
    public class SaveCommand : CommandBase
    {
        internal SaveCommand() { }
        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write("====== Saved =====");
        }
    }
}
