using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;

namespace Reinforced.Tecture.Tracing
{
    [CommandCode("QRY")]
    public class QueryRecord : CommandBase
    {
        public QueryRecord(Type channel, Type dataType, string hash, object result, bool isTestData)
        {
            Channel = channel;
            Hash = hash;
            Result = result;
            IsTestData = isTestData;
            DataType = dataType;
        }

        public Type DataType { get; }

        public Type Channel { get; }

        public string Hash { get; }

        public object Result { get; }

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

        private string Description(object o)
        {
            if (o == null) return "null";
            if (o is IDescriptive descr) return descr.Descibe();
            return o.ToString();
        }
    }
}
