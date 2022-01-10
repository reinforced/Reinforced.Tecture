using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Parse;
using Reinforced.Tecture.Aspects.DirectSql.Reveal;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.DirectSql.Commands
{
    /// <summary>
    /// SQL stroke command
    /// </summary>
    public sealed class Sql : CommandBase
    {
        public override string Code => "SQL";
        
        private readonly bool _isOnlyTracing;
        internal Sql(LambdaExpression strokeExpression)
        {
            _strokeExpression = strokeExpression;
        }

        internal Sql(InterpolatedQuery preview)
        {
            _isOnlyTracing = true;
            _preview = preview;
        }

        internal LambdaExpression StrokeExpression
        {
            get
            {
                if (_isOnlyTracing) throw new Exception("This Sql command is only for tracing purposes");
                return _strokeExpression;
            }
        }

        private readonly LambdaExpression _strokeExpression;

        private InterpolatedQuery _preview;

        /// <summary>
        /// SQL command preview without actual DB schema mapping
        /// </summary>
        public InterpolatedQuery Preview
        {
            get
            {
                if (_preview == null)
                {
                    FakeMapper fm = new FakeMapper();
                    LanguageInterpolator li = new LanguageInterpolator();
                    SchemaInterpolator schi = new SchemaInterpolator(fm);
                    _preview = _strokeExpression
                        .ParseStroke()
                        .VisitStroke(fm.IsEntityType)
                        .LanguageInterpolateStroke(li)
                        .SchemaInterpolateStroke(schi);
                }

                return _preview;
            }
        }

        [Validated("query text")]
        public string QueryText => Preview.Query.Trim();

        [Validated("query parameters collection")]
        public object[] QueryParameters => Preview.Parameters;
        
        protected override string ToStringActually()
        {
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
            if (!string.IsNullOrEmpty(Annotation)) tw.Write(Annotation);
            else tw.Write("Direct SQL will be sent to DB");
            if (Debug != null) tw.Write($" ({Debug.Location})");
            tw.WriteLine(":");

            tw.WriteLine("\t----------");
            tw.WriteLine($"\t {Preview.Query}");
            if (Preview.Parameters.Any())
            {
                tw.WriteLine("\t---");
                tw.WriteLine($"\t {string.Join(", ", Preview.Parameters.Select((o, idx) => $"@p{idx} = {o}"))}");
            }

            tw.Write("\t----------");
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new Sql(Preview.Clone());
        }
    }
}
