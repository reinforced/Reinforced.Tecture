using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Stories;

namespace Reinforced.Tecture.Testing.Generation
{
   
    public abstract class TestGenerator
    {
        /// <summary>
        /// CommandsQueue
        /// </summary>
        protected IEnumerable<CommandBase> Commands { get; private set; }

        internal Dictionary<Type,List<CheckDescription>> _checksForCommands = new Dictionary<Type, List<CheckDescription>>();

        internal void Interpolate(IEnumerable<CommandBase> commands)
        {
            Commands = commands;
        }

        protected abstract void Before();

        protected abstract void After();

        protected abstract void DumpTest(TextWriter tw);

        protected abstract void Visit(CommandBase command, CheckDescription[] checks);
    }
}
