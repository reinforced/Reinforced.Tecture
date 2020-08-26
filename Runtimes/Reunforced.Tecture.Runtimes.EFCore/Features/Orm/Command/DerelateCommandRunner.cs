﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Derelate;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class DerelateCommandRunner : CommandRunner<Derelate>
    {
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
        protected override Task RunAsync(Derelate cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
