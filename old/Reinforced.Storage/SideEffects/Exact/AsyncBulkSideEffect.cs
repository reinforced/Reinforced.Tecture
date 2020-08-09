using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Storage.Strokes;

namespace Reinforced.Storage.SideEffects.Exact
{
    /// <summary>
    /// Side effect for bulk operation
    /// </summary>
    [SideEffectCode("ABULK")]
    public class AsyncBulkSideEffect : SideEffectBase
    {

        public Type ElementType { get; internal set; }

        public IEnumerable Data { get; internal set; }

        public Func<AsyncBulkOperator, Task> Actions { get; internal set; }

        public HashSet<Type> EntitiesValidator { get; internal set; }

        private readonly StrokeProcessor _strokeProcessor;
        private readonly HashSet<Type> _typesValidator;

        internal AsyncBulkSideEffect(StrokeProcessor strokeProcessor, HashSet<Type> typesValidator)
        {
            _strokeProcessor = strokeProcessor;
            _typesValidator = typesValidator;
        }

        public async Task Run(string tableName, Func<DirectSqlSideEffect, Task<int>> exec)
        {
            _strokeProcessor.RegisterTemporaryTypeMap(ElementType, tableName);
            var bo = new AsyncBulkOperator(exec, _strokeProcessor, _typesValidator, ElementType);
            await Actions(bo);
            _strokeProcessor.RemoveTemporaryTypeMap(ElementType);
        }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Annotation))
            {
                tw.Write(Annotation);
            }
            else
            {
                tw.Write($"Upload {Data.Cast<object>().Count()} entries of {ElementType.Name}");
            }

            if (Debug != null) tw.Write($" ({Debug.Location})");
            tw.WriteLine();
            tw.WriteLine("Following SQL will be applied:");

            _strokeProcessor.RegisterTemporaryTypeMap(ElementType, "<% TMP TABLE %>");
            var bo = new AsyncBulkOperator(s =>
                {
                    tw.WriteLine("=======");
                    tw.WriteLine($" {s}");
                    tw.WriteLine("---");
                    tw.WriteLine(string.Join(", ", s.Parameters.Select((o, idx) => $"@p{idx} = {o}")));
                    tw.WriteLine("=======");
                    return Task.FromResult(0);
                }
                , _strokeProcessor, _typesValidator, ElementType);
            var t = Actions(bo);
            t.Wait();

            _strokeProcessor.RemoveTemporaryTypeMap(ElementType);

        }
    }
}
