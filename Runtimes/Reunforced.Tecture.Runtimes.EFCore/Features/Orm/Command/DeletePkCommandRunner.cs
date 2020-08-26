using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.DeletePk;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class DeletePkCommandRunner : CommandRunner<DeletePk>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly Auxilary _aux;

        public DeletePkCommandRunner(Auxilary aux, ILazyDisposable<DbContext> dc)
        {
            _dc = dc;
            _aux = aux;
        }

        private IPrimaryKey CreateInstance(Type t)
        {
            try
            {
                return (IPrimaryKey) Activator.CreateInstance(t);
            }
            catch (Exception e)
            {
                return (IPrimaryKey) t.InstanceNonpublic();
            }
        }

        protected override void Run(DeletePk cmd)
        {
            if (_aux.IsCommandRunNeeded)
            {
                var instance = CreateInstance(cmd.EntityType);
                var properties = instance.KeyProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    properties[i].SetValue(instance, cmd.KeyValues[i]);
                }

                var inst = _dc.Value.Attach(instance);
                inst.State = EntityState.Deleted;
            }
        }

        protected override Task RunAsync(DeletePk cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
