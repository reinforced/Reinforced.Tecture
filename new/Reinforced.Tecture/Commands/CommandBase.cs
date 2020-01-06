using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Reinforced.Tecture.Commands
{
    
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

    public abstract class CommandBase
    {
        /// <summary>
        /// Command annotation
        /// </summary>
        public string Annotation { get; internal set; } = string.Empty;

        /// <summary>
        /// Contains command debugging information. Available only in testing mode
        /// </summary>
        public DebugInfo Debug { get; internal set; }

        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw"></param>
        public abstract void Describe(TextWriter tw);
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

        public string Location
        {
            get
            {
                Queue<string> address = new Queue<string>();
                if (SourceService != null) address.Enqueue(SourceService.Name);
                if (SourceMethod != null)
                {
                    if (SourceMethod.GetCustomAttribute<CompilerGeneratedAttribute>() != null) address.Enqueue("anonymous method");
                    else address.Enqueue(SourceMethod.Name);
                }

                if (!string.IsNullOrEmpty(FileName)) address.Enqueue($"file: {FileName}");
                if (LineNumber != 0) address.Enqueue($"line: {LineNumber}");

                return string.Join(", ", address);
            }
        }
    }
}
