using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    internal class CycleTraceContext : ICycleTraceContext
    {
        private readonly Pipeline _pipeline;
        private int _iterations = 0;
        private readonly int _commandsBefore = 0;
        private readonly string _annotation;
        internal CycleTraceContext(Pipeline pipeline, string annotation)
        {
            _pipeline = pipeline;
            _annotation = annotation;
            _commandsBefore = pipeline.CommandsCount;

            pipeline.Enqueue(new Cycle()
            {
                Annotation = annotation,
            });
        }

        public void Iteration(string annotation = null)
        {
            _iterations++;
            _pipeline.Enqueue(new Iteration() { Annotation = annotation ?? string.Empty });
        }

        public void Dispose()
        {
            _pipeline.Enqueue(new EndCycle()
            {
                Annotation = _annotation,
                IterationsCount = _iterations,
                TotalCommands = _pipeline.CommandsCount - _commandsBefore
            });
        }
}
}
