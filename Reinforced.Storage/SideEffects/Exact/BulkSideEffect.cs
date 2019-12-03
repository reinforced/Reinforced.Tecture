using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Reinforced.Storage.Strokes;

namespace Reinforced.Storage.SideEffects.Exact
{
    /// <summary>
    /// Side effect for bulk operation
    /// </summary>
    [SideEffectCode("BULK")]
    public class BulkSideEffect : SideEffectBase
    {
        public Type ElementType { get; internal set; }

        public IEnumerable Data { get; internal set; }

        public Action<BulkOperator> Actions { get; internal set; }

        public HashSet<Type> EntitiesValidator { get; internal set; }

        private readonly StrokeProcessor _strokeProcessor;
        private readonly HashSet<Type> _typesValidator;

        internal BulkSideEffect(StrokeProcessor strokeProcessor, HashSet<Type> typesValidator)
        {
            _strokeProcessor = strokeProcessor;
            _typesValidator = typesValidator;
        }

        public void Run(string tableName, Func<DirectSqlSideEffect, int> exec)
        {
            _strokeProcessor.RegisterTemporaryTypeMap(ElementType,tableName);
            var bo = new BulkOperator(exec, _strokeProcessor, _typesValidator, ElementType);
            Actions(bo);
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
            var bo = new BulkOperator(s =>
                {
                    tw.WriteLine("=======");
                    tw.WriteLine($" {s}");
                    tw.WriteLine("---");
                    tw.WriteLine(string.Join(", ", s.Parameters.Select((o, idx) => $"@p{idx} = {o}")));
                    tw.WriteLine("=======");
                    return 0;
                }
                , _strokeProcessor, _typesValidator, ElementType);
            Actions(bo);
            _strokeProcessor.RemoveTemporaryTypeMap(ElementType);

        }
    }
}
