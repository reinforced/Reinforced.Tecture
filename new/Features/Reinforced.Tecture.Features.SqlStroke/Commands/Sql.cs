using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Parse;
using Reinforced.Tecture.Features.SqlStroke.Reveal;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit;

namespace Reinforced.Tecture.Features.SqlStroke.Commands
{
    [CommandCode("SQL")]
    public sealed class Sql : CommandBase
    {
        internal Sql(LambdaExpression strokeExpression)
        {
            _strokeExpression = strokeExpression;
        }

        private readonly LambdaExpression _strokeExpression;

        private InterpolatedQuery _preview;

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

        internal InterpolatedQuery Compile(IMapper mapper, LanguageInterpolator li, SchemaInterpolator schi)
        {
            return _strokeExpression
                .ParseStroke()
                .VisitStroke(mapper.IsEntityType)
                .LanguageInterpolateStroke(li)
                .SchemaInterpolateStroke(schi);
        }

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
    }
}
