using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reinforced.Tecture.Commands
{
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