using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.BuiltInChecks;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Entity that is being used to validate story
    /// </summary>
    public class TraceValidator
    {
        struct ValidationEntry
        {
            public Type CommandType { get; set; }
            public ICommandCheck[] Assertions { get; set; }

            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public ValidationEntry(Type commandType, ICommandCheck[] assertions)
            {
                Assertions = assertions;
                CommandType = commandType;
            }
        }

        private ValidationEntry _current = new ValidationEntry(null, null);
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
        public void Then<TCommand>(params ICommandCheck<TCommand>[] assertions) where TCommand : CommandBase
        {
            _current.CommandType = typeof(TCommand);
            _current.Assertions = assertions;
            _validationEntries.Enqueue(_current);
            _current = new ValidationEntry(null, null);
        }

        /// <summary>
        /// Denotes end of story and launches validation
        /// </summary>
        public void TheEnd()
        {
            var cmdsArray = _story.Commands.ToList();
            _validationEntries.Enqueue(new ValidationEntry(typeof(End), null));
            var eIdx = 0;
            while (_validationEntries.Count > 0)
            {
                var currentValidator = _validationEntries.Dequeue();


                CommandBase command = eIdx >= cmdsArray.Count ? null : cmdsArray[eIdx];
                // check command type
                if (currentValidator.CommandType != null)
                {
                    if (!currentValidator.CommandType.IsInstanceOfType(command))
                    {
                        throw new TectureCheckException(
                            $"expected command of type {currentValidator.CommandType.Name}, but got {command.GetType().Name}");
                    }
                }
                //perform assertions
                if (currentValidator.Assertions != null)
                {
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
                }

                eIdx++;
            }

            if (eIdx < cmdsArray.Count) throw new TectureCheckException($"story is too short. Validation is longer.");
        }
    }
}
