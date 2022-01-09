using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Commands;
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
            public Type ChannelType { get; set; }
            public Delegate Assertion { get; set; }

            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public ValidationEntry(Type commandType, Delegate assertion)
            {
                Assertion = assertion;
                CommandType = commandType;
                ChannelType = null;
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
        /// <param name="assertions">Assertion delegate</param>
        /// <returns>Fluent</returns>
        public void Then<TCommand>(Action<TCommand> assertions = null) where TCommand : CommandBase
        {
            _current.CommandType = typeof(TCommand);
            _current.Assertion = assertions;
            _validationEntries.Enqueue(_current);
            _current = new ValidationEntry(null, null);
        }
        
        /// <summary>
        /// Sets validator for upcoming side-effect
        /// </summary>
        /// <param name="assertions">Assertion delegate</param>
        /// <returns>Fluent</returns>
        public void Then<TChannel,TCommand>(Action<TCommand> assertions = null) where TCommand : CommandBase
        {
            _current.CommandType = typeof(TCommand);
            _current.Assertion = assertions;
            _current.ChannelType = typeof(TChannel);
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
                        throw new TectureValidationException(
                            $"expected command of type {currentValidator.CommandType.Name}, but got {command.GetType().Name}");
                    }
                }
                
                // check channel type
                if (currentValidator.ChannelType != null)
                {
                    if (command != null)
                        if (currentValidator.ChannelType != command.Channel)
                            throw new TectureValidationException(
                                $"expected command for channel {currentValidator.ChannelType.Name}, but got {command.Channel.Name}");
                }
                
                //perform assertions
                if (currentValidator.Assertion != null)
                {
                    if (command != null)
                    {
                        currentValidator.Assertion.DynamicInvoke(command);
                    }
                }

                eIdx++;
            }

            if (eIdx < cmdsArray.Count) throw new TectureValidationException($"story is too short. Validation is longer.");
        }
    }
}
