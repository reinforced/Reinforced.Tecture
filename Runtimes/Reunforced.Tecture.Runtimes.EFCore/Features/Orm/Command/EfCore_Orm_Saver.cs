using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.Commands.Delete;
using Reinforced.Tecture.Features.Orm.Commands.Derelate;
using Reinforced.Tecture.Features.Orm.Commands.Relate;
using Reinforced.Tecture.Features.Orm.Commands.Update;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class EfCore_Orm_Saver : Saver<Add, Delete, Update, Relate, Derelate>
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void Save()
        {
            
        }

        protected override Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Add> GetRunner1(Add command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Delete> GetRunner2(Delete command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Update> GetRunner3(Update command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Relate> GetRunner4(Relate command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Derelate> GetRunner5(Derelate command)
        {
            throw new NotImplementedException();
        }
    }
}
