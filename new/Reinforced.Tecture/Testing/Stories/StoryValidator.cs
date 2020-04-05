using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Stories
{
    /// <summary>
    /// Entity that is being used to validate story
    /// </summary>
    public class StoryValidator
    {
        enum FlowControl
        {
            TakeWhile,
            Immediate
        }
        struct ValidationEntry
        {
            public FlowControl FlowControl { get; set; }
            public CommandCheck[] Assertions { get; set; }

            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public ValidationEntry(FlowControl flowControl, CommandCheck[] assertions)
            {
                FlowControl = flowControl;
                Assertions = assertions;
            }
        }

        private ValidationEntry _current = new ValidationEntry(FlowControl.Immediate, null);
        private readonly Queue<ValidationEntry> _entries = new Queue<ValidationEntry>();

        private readonly StorageStory _story;

        internal StoryValidator(StorageStory story)
        {
            _story = story;
        }

        /// <summary>
        /// Sets validator for upcoming side-effect
        /// </summary>
        /// <param name="assertion">Set of assertions that must take place for upcoming side-effect</param>
        /// <returns>Fluent</returns>
        public StoryValidator Then(params CommandCheck[] assertion)
        {
            foreach (var sa in assertion)
            {
                sa.Environment = _story._environment;
            }
            _current.Assertions = assertion;
            _entries.Enqueue(_current);
            _current = new ValidationEntry(FlowControl.Immediate, null);
            return this;
        }

        /// <summary>
        /// Sets validator for upcoming side-effect
        /// </summary>
        /// <param name="times">Sets for how many upcoming side-effects assertion must take place</param>
        /// <param name="assertion">Set of assertions that must take place for upcoming side-effect</param>
        /// <returns>Fluent</returns>
        public StoryValidator Then(int times, params CommandCheck[] assertion)
        {
            foreach (var sa in assertion)
            {
                sa.Environment = _story._environment;
            }
            _current.Assertions = assertion;
            _entries.Enqueue(_current);
            for (int i = 0; i < times - 1; i++)
            {
                _entries.Enqueue(new ValidationEntry(FlowControl.Immediate, assertion));
            }
            _current = new ValidationEntry(FlowControl.Immediate, null);
            return this;
        }

        /// <summary>
        /// Skips side effects until next .Then validator will not fire.
        /// If end is reached then validation fails
        /// </summary>
        /// <returns>Fluent</returns>
        public StoryValidator SomethingHappens()
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
            cmdsArray.Add(new EndCommand());
            _entries.Enqueue(new ValidationEntry(_current.FlowControl, new[] { new EndStoryCheck() { Environment = _story._environment } }));
            var eIdx = 0;
            while (_entries.Count > 0)
            {
                var currentValidator = _entries.Dequeue();

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
                                    $"expected side effect of type {asrt.CommandType.Name}, but got {command.GetType().Name}");
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
