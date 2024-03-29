﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Tracing
{
    /// <summary>
    /// Synthetic command that means query that was made to the external system
    /// </summary>
    public class QueryRecord : CommandBase, ITracingOnly
    {
        public override bool IsExecutable => false;
        public override string Code => "-> ";
        
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
        /// Query result
        /// </summary>
        public object Result { get; private set; }

        internal void SetException<T>(Exception ex, string description, TimeSpan timeTaken)
        {
            var type = typeof(T);
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                type = typeof(object);
            }
            Result = null;
            DataType = type;
            Hash = String.Empty;
            TimeTaken = timeTaken;
            Annotation = description;
            Exception = ex;
            if (_lightMode) return;
            foreach (var commandBase in KnownClones)
            {
                var kc = (QueryRecord) commandBase;
                kc.Result = null;
                kc.DataType = type;
                kc.Hash = String.Empty;
                kc.TimeTaken = timeTaken;
                kc.Annotation = description;
            }
        }
        
        internal void SetLightResult<T>(string description, TimeSpan timeTaken)
        {
            var type = typeof(T);
            
            Result = null;
            DataType = type;
            Hash = "<cut>";
            TimeTaken = timeTaken;
            Annotation = description;
        }

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
            if (_lightMode) return;
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
        protected override string ToStringActually()
        {
            if (_lightMode)
            {
                if (!string.IsNullOrEmpty(Annotation)) return Annotation;
                return $"{(IsTestData ? "[Mock] " : "")} Query made to '{Channel.Name}'. Result is not captured.";
            }
                
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                Describe(tw);
                tw.Flush();
            }

            return sb.ToString();
        }

        /// <inheritdoc cref="CommandBase" />
        private void Describe(TextWriter tw)
        {
            if (IsTestData) tw.Write("[Mock] ");
            tw.Write(string.IsNullOrEmpty(this.Annotation)? $"Query made to '{Channel.Name}' ({Hash})": Annotation);
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