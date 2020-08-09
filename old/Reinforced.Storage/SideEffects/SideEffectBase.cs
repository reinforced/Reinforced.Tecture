using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Reinforced.Storage.SideEffects
{
    public static class SideEffectExtensions
    {
        public static T Annotate<T>(this T effect, string annotation) where T : SideEffectBase
        {
            effect.Annotation = annotation;
            return effect;
        }
    }

    /// <summary>
    /// Side effect interface
    /// </summary>
    public abstract class SideEffectBase
    {
        /// <summary>
        /// Side effect annotation
        /// </summary>
        public string Annotation { get; internal set; } = string.Empty;

        /// <summary>
        /// Contains side-effect debugging information. Available only in testing mode
        /// </summary>
        public DebugInfo Debug { get; internal set; }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public abstract void Describe(TextWriter tw);
    }

    /// <summary>
    /// Provides debug information about occuring side effect
    /// </summary>
    public class DebugInfo
    {
        /// <summary>
        /// Service that has initiated side effect. Available in debug only.
        /// </summary>
        public Type SourceService { get; internal set; }

        /// <summary>
        /// Service that side effect was initiated. Available in debug only.
        /// </summary>
        public MethodBase SourceMethod { get; internal set; }

        /// <summary>
        /// Source line where side effect was initiated
        /// </summary>
        public int LineNumber { get; internal set; }

        /// <summary>
        /// File name where side effect was initiated
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
                if (LineNumber!=0) address.Enqueue($"line: {LineNumber}");

                return string.Join(", ", address);
            }
        }
    }
}
