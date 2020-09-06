using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Parse;
using Reinforced.Tecture.Features.SqlStroke.Reveal;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit;

namespace Reinforced.Tecture.Features.SqlStroke.Commands
{
    /// <summary>
    /// SQL stroke command
    /// </summary>
    [CommandCode("SQL")]
    public sealed class Sql : CommandBase
    {
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

        
        /// <inheritdoc />
        public override string ToString()
        {
            return String.Format(Preview.Query, Preview.Parameters);
        }
        
        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
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
