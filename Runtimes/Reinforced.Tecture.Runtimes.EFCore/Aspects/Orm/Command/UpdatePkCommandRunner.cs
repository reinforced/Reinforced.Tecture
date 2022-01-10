using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    class UpdatePkCommandRunner : CommandRunner<UpdatePk>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly TestingContext _aux;

        public UpdatePkCommandRunner(TestingContext aux, ILazyDisposable<DbContext> dc)
        {
            _aux = aux;
            _dc = dc;
        }

        private EntityEntry FindEntry(Type t, IPrimaryKey instance, PropertyInfo[] props)
        {
            var values = props.Select(x => x.GetValue(instance)).ToArray();
            var entryQuery = _dc.Value.ChangeTracker.Entries()
                .Where(x => x.Entity.GetType() == t)
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

            if (entryQuery == null)
            {
                return _dc.Value.Entry(instance);
            }
            return entryQuery;
        }
        private IPrimaryKey CreateInstance(Type t)
        {
            try
            {
                return (IPrimaryKey)Activator.CreateInstance(t);
            }
            catch (Exception e)
            {
                return (IPrimaryKey)t.InstanceNonpublic();
            }
        }
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(UpdatePk cmd)
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
                foreach (var cmdUpdateValue in cmd.UpdateValues)
                {
                    cmdUpdateValue.Key.SetValue(instance, cmdUpdateValue.Value);
                    entry.Property(cmdUpdateValue.Key.Name).IsModified = true;
                }
                //entry.State = EntityState.Modified;
                //_dc.Value.Update(entry.Entity);
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(UpdatePk cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
