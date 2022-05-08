using System;
using System.Collections.Generic;
using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Testing;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Common command extensions
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Specifies command annotation that helps to debug command chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd">Command</param>
        /// <param name="annotation">Command annotation</param>
        /// <returns>Fluent</returns>
        public static T Annotate<T>(this T cmd, string annotation) where T : CommandBase
        {
            cmd.Annotation = annotation;
            return cmd;
        }
    }

    /// <summary>
    /// Base class for all commands.
    /// Try to keep it simple and serializable
    /// </summary>
    public abstract class CommandBase
    {
        private Type _channel;

        private string _annotation = string.Empty;
        private DebugInfo _debug;
        private int _order;
        private bool _isExecuted;
        private Type _service;
        private TimeSpan _timeTaken = TimeSpan.Zero;
        private Exception _exception;
        internal bool _lightMode = false;

        public virtual string Code => string.Empty;
        
        
        /// <summary>
        /// Gets whether command is captured in light mode, so does not contain majority of data
        /// </summary>
        public bool IsLightMode => _lightMode;

        /// <summary>
        /// Discriminates data source type for command.
        /// Here I use .NET full Type's name
        /// </summary>
        public Type Channel
        {
            get => _channel;
            internal set
            {
                _channel = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Channel = value;
                }
            }
        }

        /// <summary>
        /// Discriminates data source type for command.
        /// Here I use .NET full Type's name
        /// </summary>
        public string ChannelId => _channel.FullName;

        /// <summary>
        /// Gets whether command is executable
        /// </summary>
        public virtual bool IsExecutable => true;
        
        /// <summary>
        /// Gets friendly channel name
        /// </summary>
        public string ChannelName => typeof(NoChannel).IsAssignableFrom(_channel) ? string.Empty : _channel.Name;

        /// <summary>
        /// Command annotation
        /// </summary>
        [Validated("Annotation")]
        public string Annotation
        {
            get => _annotation;
            internal set
            {
                _annotation = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Annotation = value;
                }
            }
        }

        /// <summary>
        /// Contains command debugging information. Available only in testing mode
        /// </summary>
        public DebugInfo Debug
        {
            get => _debug;
            internal set
            {
                _debug = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Debug = value;
                }
            }
        }

        private readonly Lazy<string> _toString;
        protected CommandBase()
        {
            _toString = new Lazy<string>(ToStringActually);
        }

        public sealed override string ToString()
        {
            return _toString.Value;
        }

        protected virtual string ToStringActually()
        {
            return Annotation ?? "Command";
        }

        /// <summary>
        /// Gets order of command in commands queue
        /// </summary>
        public int Order
        {
            get => _order;
            internal set
            {
                _order = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Order = value;
                }
            }
        }

        private readonly List<CommandBase> _knownClones = new List<CommandBase>();

        /// <summary>
        /// Collection of known clones of the command
        /// </summary>
        protected IEnumerable<CommandBase> KnownClones => _knownClones;

        /// <summary>
        /// Gets whether command was executed or not
        /// </summary>
        public Exception Exception
        {
            get => _exception;
            internal set
            {
                _exception = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Exception = value;
                }
            }
        }

        /// <summary>
        /// Gets whether command was executed or not
        /// </summary>
        public bool IsExecuted
        {
            get => _isExecuted;
            internal set
            {
                _isExecuted = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.IsExecuted = value;
                }
            }
        }

        /// <summary>
        /// Gets the time that particular command execution or query evaluation took
        /// </summary>
        public TimeSpan TimeTaken
        {
            get => _timeTaken;
            internal set
            {
                _timeTaken = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.TimeTaken = value;
                }
            }
        }
        
        /// <summary>
        /// Gets service type where command was received from
        /// </summary>
        public Type Service
        {
            get => _service;
            internal set
            {
                _service = value;
                if (_lightMode) return;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Service = value;
                }
            }
        }
        

        internal CommandBase TraceClone()
        {
            if (_lightMode)
                throw new TectureException("Light commands are not suitable for cloning. Check the logic");
            
            var clone = DeepCloneForTracing();
            clone.Channel = Channel;
            clone.Annotation = Annotation;
            clone.Order = Order;
            clone.IsExecuted = IsExecuted;
            clone.Debug = Debug;
            _knownClones.Add(clone);
            return clone;
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected abstract CommandBase DeepCloneForTracing();
    }
}