using System;
using System.Collections;
using System.IO;
using System.Linq;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Tracing
{
    /// <summary>
    /// Synthetic command that means query that was made to the external system
    /// </summary>
    [CommandCode("▷")]
    public class QueryRecord : CommandBase, ITracingOnly
    {
        internal QueryRecord(Type channel, bool isTestData)
        {
            Channel = channel;
            IsTestData = isTestData;
        }

        /// <summary>
        /// Gets data type that is returned by the query
        /// </summary>
        public Type DataType { get; private set; }

        /// <summary>
        /// Query hash
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// The time query evaluation took
        /// </summary>
        public TimeSpan TimeTaken { get; private set; }

        /// <summary>
        /// Query result
        /// </summary>
        public object Result { get; private set; }

        internal void SetResult<T>(T result, T clone, string hash, string description, TimeSpan timeTaken)
        {
            var type = typeof(T);
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                type = result.GetType();
            }

            Result = result;
            DataType = type;
            Hash = hash;
            TimeTaken = timeTaken;
            Annotation = description;
            foreach (var commandBase in KnownClones)
            {
                var kc = (QueryRecord) commandBase;
                kc.Result = clone;
                kc.DataType = type;
                kc.Hash = hash;
                kc.TimeTaken = timeTaken;
                kc.Annotation = description;
            }
        }

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
            if (IsTestData) tw.Write("[Mock] ");
            else FormatTime(tw);
            tw.Write(this.Annotation ?? $"Query made to '{Channel.Name}' ({Hash})");
            if (IsTestData) return;
            tw.Write(": ");
            if (Result == null)
            {
                tw.Write("result is null");
            }
            else if (Result is string s)
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
                    tw.Write(desc.Describe());
                }
                else
                {
                    tw.Write($"'{Result}' obtained");
                }
            }
        }

        private void FormatTime(TextWriter tw)
        {
            if (TimeTaken.TotalMinutes > 1)
            {
                tw.Write($"[{TimeTaken:mm:ss:fff}]\t");
                return;
            }

            if (TimeTaken.TotalSeconds > 5)
            {
                tw.Write($"[{TimeTaken:ss:fff} s]\t");
                return;
            }

            tw.Write($"[{TimeTaken:fff}ms]\t");
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new QueryRecord(Channel, IsTestData)
            {
                DataType = DataType,
                Hash = Hash,
                Result = DeepCloner.DeepClone(Result),
                TimeTaken = TimeTaken
            };
        }

        private string Description(object o)
        {
            if (o == null) return "null";
            if (o is IDescriptive descr) return descr.Describe();
            return o.ToString();
        }
    }
}