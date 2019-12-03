using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Strokes;

namespace Reinforced.Storage
{
    /// <summary>
    /// Bulk operator. Use SQL operations like .StrokeBefore/.After to perform things
    /// </summary>
    public class BulkOperator
    {
        private readonly Func<DirectSqlSideEffect, int> _exec;
        private readonly StrokeProcessor _strokeProcessor;
        private readonly HashSet<Type> _typesValidator;
        private readonly Type _dataType;
        internal BulkOperator(Func<DirectSqlSideEffect, int> exec, StrokeProcessor strokeProcessor, HashSet<Type> typesValidator, Type dataType)
        {
            _exec = exec;
            _strokeProcessor = strokeProcessor;
            _typesValidator = typesValidator;
            _dataType = dataType;
        }

        public int Sql(Expression<Func<string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T>(Expression<Func<T, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2>(Expression<Func<T1, T2, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2, T3>(Expression<Func<T1, T2, T3, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke, annotation);
        }

        public int Sql<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke, string annotation = null)
        {
            return RawStroke(stroke,annotation);
        }

        private int RawStroke(LambdaExpression expr, string annotation)
        {
            var p = _strokeProcessor.RevealQuery(expr);
            if (_typesValidator != null)
            {
                foreach (var pUsedType in p.UsedTypes)
                {
                    if (pUsedType != _dataType)
                    {
                        if (!_typesValidator.Contains(pUsedType))
                            throw new Exception($"Cannot touch entity {pUsedType} within bulk operation because it is not allowed");
                    }
                }
            }
            return _exec(new DirectSqlSideEffect(p.CommandText,p.CommandParameters).Annotate(annotation));
        }

    }
}
