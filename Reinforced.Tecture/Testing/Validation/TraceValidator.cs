using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.BuiltInChecks;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Entity that is being used to validate story
    /// </summary>
    public class TraceValidator
    {
        enum FlowControl
        {
            TakeWhile,
            Immediate
        }
        struct ValidationEntry
        {
            public FlowControl FlowControl { get; set; }
            public ICommandCheck[] Assertions { get; set; }

            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public ValidationEntry(FlowControl flowControl, ICommandCheck[] assertions)
            {
                FlowControl = flowControl;
                Assertions = assertions;
            }
        }

        private ValidationEntry _current = new ValidationEntry(FlowControl.Immediate, null);
        private readonly Queue<ValidationEntry> _validationEntries = new Queue<ValidationEntry>();

        private readonly Trace _story;

        internal TraceValidator(Trace story)
        {
            _story = story;
        }
        /// <summary>
        /// Sets validator for upcoming side-effect
        /// </summary>
        /// <param name="assertions">Set of assertions that must take place for upcoming side-effect</param>
        /// <returns>Fluent</returns>
        public TraceValidator Then<TCommand>(params ICommandCheck<TCommand>[] assertions) where TCommand : CommandBase
        {
            _current.Assertions = assertions;
            _validationEntries.Enqueue(_current);
            _current = new ValidationEntry(FlowControl.Immediate, null);
            return this;
        }

        /// <summary>
        /// Skips side effects until next .Then validator will not fire.
        /// If end is reached then validation fails
        /// </summary>
        /// <returns>Fluent</returns>
        public TraceValidator SomethingHappens()
        {
            _current.FlowControl = FlowControl.TakeWhile;
            return this;
        }

        /// <summary>
        /// Denotes end of story and launches validation
        /// </summary>
        public void TheEnd()
        {
            var cmdsArray = _story.Commands.ToList();
            _validationEntries.Enqueue(new ValidationEntry(_current.FlowControl, new[] { new EndStoryCheck() }));
            var eIdx = 0;
            while (_validationEntries.Count > 0)
            {
                var currentValidator = _validationEntries.Dequeue();

                if (currentValidator.FlowControl == FlowControl.Immediate)
                {
                    CommandBase command = eIdx >= cmdsArray.Count ? null : cmdsArray[eIdx];
                    foreach (var asrt in currentValidator.Assertions)
                    {
                        if (command != null)
                        {
                            if (!asrt.CommandType.GetTypeInfo().IsAssignableFrom(command.GetType()))
                            {
                                throw new TectureCheckException(
                                    $"expected command of type {asrt.CommandType.Name}, but got {command.GetType().Name}");
                            }
                        }

                        asrt.Assert(command);
                    }

                    eIdx++;
                }
                else if (currentValidator.FlowControl == FlowControl.TakeWhile)
                {
                    int foundIdx = -1;
                    for (int i = eIdx; i < cmdsArray.Count; i++)
                    {
                        CommandBase cmd = cmdsArray[i];
                        bool allValid = true;
                        foreach (var asrt in currentValidator.Assertions)
                        {
                            allValid = allValid && asrt.IsValid(cmd);
                        }

                        if (allValid)
                        {
                            foundIdx = i; break;
                        }
                    }

                    if (foundIdx != -1) eIdx = foundIdx + 1;
                    else
                    {
                        foreach (var asrt in currentValidator.Assertions) asrt.Assert(null);
                    }
                }
                else throw new Exception("Unknown flow control");

            }

            if (eIdx < cmdsArray.Count) throw new TectureCheckException($"story is too short. Validation is longer.");
        }
    }
}
