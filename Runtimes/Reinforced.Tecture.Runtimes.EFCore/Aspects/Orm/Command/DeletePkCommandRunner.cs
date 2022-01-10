using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    class DeletePkCommandRunner : CommandRunner<DeletePk>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly TestingContext _aux;

        public DeletePkCommandRunner(TestingContext aux, ILazyDisposable<DbContext> dc)
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
            catch (Exception)
            {
                return (IPrimaryKey) t.InstanceNonpublic();
            }
        }

        private EntityEntry<IPrimaryKey> FindEntry(Type t, IPrimaryKey instance, PropertyInfo[] props)
        {
            var values = props.Select(x => x.GetValue(instance)).ToArray();
            var entryQuery = _dc.Value.ChangeTracker.Entries<IPrimaryKey>()
                .Where(x=>x.Entity.GetType() == t)
                .Where(x =>
                {
                    for (int i = 0; i < props.Length; i++)
                    {
                        if (!object.Equals(props[i].GetValue(x.Entity), values[i]))
                            return false;
                    }

                    return true;
                })
                .FirstOrDefault();

            if (entryQuery == null) return _dc.Value.Entry(instance);
            return entryQuery;
        }

        protected override void Run(DeletePk cmd)
        {
            if (!_aux.ProvidesTestData)
            {
                var instance = CreateInstance(cmd.EntityType);
                var properties = instance.KeyProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    properties[i].SetValue(instance, cmd.KeyValues[i]);
                }

                var entry = FindEntry(cmd.EntityType, instance, properties);
                entry.State = EntityState.Deleted;
            }
        }

        protected override Task RunAsync(DeletePk cmd,CancellationToken token = default)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
