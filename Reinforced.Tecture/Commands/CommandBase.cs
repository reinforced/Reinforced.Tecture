using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        /// <summary>
        /// Discriminates data source type for command.
        /// Here I use .NET full Type's name
        /// </summary>
        public Type Channel
        {
            get { return _channel; }
            internal set
            {
                _channel = value;
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
        /// Gets friendly channel name
        /// </summary>
        public string ChannelName => typeof(NoChannel).IsAssignableFrom(_channel) ? string.Empty : _channel.Name;

        /// <summary>
        /// Command annotation
        /// </summary>
        [Validated("Annotation")]
        public string Annotation
        {
            get { return _annotation; }
            internal set
            {
                _annotation = value;
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
            get { return _debug; }
            internal set
            {
                _debug = value;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.Debug = value;
                }
            }
        }

        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw">Log writer</param>
        public virtual void Describe(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Annotation))
            {
                tw.Write(Annotation);
                if (Debug != null)
                {
                    tw.Write($" ({Debug.Location})");
                }
            }
        }

        /// <summary>
        /// Gets order of command in commands queue
        /// </summary>
        public int Order
        {
            get { return _order; }
            internal set
            {
                _order = value;
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
        protected IEnumerable<CommandBase> KnownClones
        {
            get { return _knownClones; }
        }

        /// <summary>
        /// Gets whether command was executed or not
        /// </summary>
        public bool IsExecuted
        {
            get { return _isExecuted; }
            internal set
            {
                _isExecuted = value;
                foreach (var commandBase in _knownClones)
                {
                    commandBase.IsExecuted = value;
                }
            }
        }

        internal CommandBase TraceClone()
        {
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

    /// <summary>
    /// Provides debug information about occuring command
    /// </summary>
    public class DebugInfo
    {
        /// <summary>
        /// Service that has initiated command. Available in debug only.
        /// </summary>
        public Type SourceService { get; internal set; }

        /// <summary>
        /// Service that command was initiated. Available in debug only.
        /// </summary>
        public MethodBase SourceMethod { get; internal set; }

        /// <summary>
        /// Source line where command was initiated
        /// </summary>
        public int LineNumber { get; internal set; }

        /// <summary>
        /// File name where command was initiated from
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets the location where debug entry occured
        /// </summary>
        public string Location
        {
            get
            {
                Queue<string> address = new Queue<string>();
                if (SourceService != null) address.Enqueue(SourceService.Name);
                if (SourceMethod != null)
                {
                    if (SourceMethod.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
                        address.Enqueue("anonymous method");
                    else address.Enqueue(SourceMethod.Name);
                }

                if (!string.IsNullOrEmpty(FileName)) address.Enqueue($"file: {FileName}");
                if (LineNumber != 0) address.Enqueue($"line: {LineNumber}");

                return string.Join(", ", address);
            }
        }
    }
}