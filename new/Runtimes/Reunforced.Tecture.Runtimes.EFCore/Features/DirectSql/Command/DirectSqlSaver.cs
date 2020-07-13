using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Command;
using Reinforced.Tecture.Savers;

namespace Reunforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlSaver : Saver<Sql>
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void Save()
        {
            throw new NotImplementedException();
        }

        protected override Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Sql> GetRunner1(Sql command)
        {
            throw new NotImplementedException();
        }
    }
}
