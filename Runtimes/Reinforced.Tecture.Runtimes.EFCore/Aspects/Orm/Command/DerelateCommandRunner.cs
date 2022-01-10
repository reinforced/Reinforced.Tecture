using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Aspects.Orm.Commands.Derelate;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    class DerelateCommandRunner : CommandRunner<Derelate>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly TestingContext _aux;

        public DerelateCommandRunner(TestingContext aux, ILazyDisposable<DbContext> dc)
        {
            _dc = dc;
            _aux = aux;
        }
        private static readonly MethodInfo Def;

        static DerelateCommandRunner()
        {
            Def = typeof(DerelateCommandRunner).GetMethod(nameof(Default), BindingFlags.NonPublic|BindingFlags.Static);

        }
        private static T Default<T>()
        {
            return default(T);
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Derelate cmd)
        {
            var prop = cmd.PrimaryType.GetProperty(cmd.ForeignKeySpecifier,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

            var def = Def.MakeGenericMethod(prop.PropertyType);
            var df = def.Invoke(null, null);

            prop.SetValue(cmd.Primary, df);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Derelate cmd,CancellationToken token = default)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
