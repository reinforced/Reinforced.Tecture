using System;
using System.Collections;
using System.IO;
using System.Linq;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Tracing
{
    /// <summary>
    /// Synthetic command that means query that was made to the external system
    /// </summary>
    [CommandCode(" ->")]
    public class QueryRecord : CommandBase, ITracingOnly
    {
        internal QueryRecord(Type channel, Type dataType, string hash, object result, bool isTestData)
        {
            Channel = channel;
            Hash = hash;
            Result = result;
            IsTestData = isTestData;
            DataType = dataType;
        }

        /// <summary>
        /// Gets data type that is returned by the query
        /// </summary>
        public Type DataType { get; }

        /// <summary>
        /// Gets channel this query was made to
        /// </summary>
        public Type Channel { get; }

        /// <summary>
        /// Query hash
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Query result
        /// </summary>
        public object Result { get; }

        /// <summary>
        /// Gets whether test (mock) data is returned
        /// </summary>
        public bool IsTestData { get; }

        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw">Log writer</param>
        public override void Describe(TextWriter tw)
        {
            if (IsTestData) tw.Write("[TEST DATA] ");

            tw.Write(this.Annotation ?? $"Query made to '{Channel.Name}' ({Hash})");
            if (IsTestData) return;
            tw.Write(": ");
            if (Result == null)
            {
                tw.Write("result is null");
            }

            if (Result is string s)
            {
                tw.Write(s);
            }
            else if (Result is IEnumerable e)
            {
                var res = e.Cast<object>();
                var cnt = res.Count();
                if (cnt > 10 || !res.All(x => x is IDescriptive))
                {
                    tw.Write($"{cnt} results obtained");
                }
                else
                {
                    bool first = true;
                    tw.Write(" {");
                    foreach (var re in res)
                    {
                        if (!first) tw.Write(", ");
                        else first = false;
                        tw.Write(Description(re));
                    }
                    tw.Write("}");
                }
            }
            else
            {
                if (Result is IDescriptive desc)
                {
                    tw.Write(desc.Descibe());
                }
                else
                {
                    tw.Write($"'{Result}' obtained");
                }
            }
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new QueryRecord(Channel, DataType, Hash, Result, IsTestData);
        }

        private string Description(object o)
        {
            if (o == null) return "null";
            if (o is IDescriptive descr) return descr.Descibe();
            return o.ToString();
        }
    }
}
