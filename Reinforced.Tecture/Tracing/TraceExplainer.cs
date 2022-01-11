using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Tracing
{
    /// <summary>
    /// Explains trace into corresponding text writer.
    /// This class can be inherit and Explain method overloads can be added for any command type.
    /// They will be called by reflection
    /// </summary>
    public class TraceExplainer
    {
        private readonly Dictionary<Type, MethodInfo> _explanatoryMethods = new Dictionary<Type, MethodInfo>();

        private TraceExplainer()
        {
            var thisType = this.GetType();
            var methods = thisType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                if (method.Name != "Explain") continue;
                var parameters = method.GetParameters();
                if (parameters.Length != 1) continue;
                var p0 = parameters[0];
                if (!typeof(CommandBase).IsAssignableFrom(p0.ParameterType)) continue;
                _explanatoryMethods[p0.ParameterType] = method;
            }
        }

        private string _result;

        public sealed override string ToString()
        {
            if (!string.IsNullOrEmpty(_result)) return _result;
            return "Output redirected";
        }

        public TraceExplainer(TextWriter writer = null) : this()
        {
            Writer = writer;
        }

        protected TextWriter Writer { get; private set; }

        protected int Index { get; private set; }

        internal void ExplainTrace(Trace trace)
        {
            if (Writer == null)
            {
                var sb = new StringBuilder();
                using (var tw = new StringWriter(sb))
                {
                    Writer = tw;
                    ExplainTraceActually(trace);
                }

                _result = sb.ToString();
                return;
            }

            ExplainTraceActually(trace);
        }

        private void ExplainTraceActually(Trace trace)
        {
            Index = 0;

            foreach (var commandBase in trace.All)
            {
                if (ShouldSkip(commandBase)) continue;

                var type = commandBase.GetType();
                if (_explanatoryMethods.ContainsKey(type))
                {
                    _explanatoryMethods[type].Invoke(this, new object[] { commandBase });
                }
                else
                {
                    GenericExplain(commandBase);
                }

                Index++;
            }

            Writer.Flush();
        }

        protected virtual bool ShouldSkip(CommandBase command) => false;

        private static void RevealException(Exception ex, StringBuilder sb, int level)
        {
            if (ex is AggregateException ae)
            {
                foreach (var innerException in ae.InnerExceptions)
                {
                    RevealException(innerException,sb,level+1);
                }
            }
            else
            {
                for (int i = 0; i < level; i++)
                {
                    sb.Append("\t");
                }
            
                sb.AppendLine(ex.Message);

                if (ex.InnerException != null)
                {
                    RevealException(ex.InnerException,sb,level+1);
                }    
            }
        }
        
        /// <summary>
        /// Generic explainer suitable for any command type
        /// </summary>
        /// <param name="command"></param>
        protected virtual void GenericExplain(CommandBase command)
        {
            if (!command.IsExecutable) Writer.Write("   ");
            else Writer.Write(command.IsExecuted ? "[v]" : "[x]");
            Writer.Write($" {Index+1}. ");

            if ((command.IsExecutable || command is QueryRecord || command is Save) &&
                command.TimeTaken != TimeSpan.Zero)
            {
                Writer.Write($"[{command.TimeTaken.FormatCommandTimeTaken()}] ");
            }

            if (!string.IsNullOrEmpty(command.ChannelName)) Writer.Write($"[{command.ChannelName}] ");
            else Writer.Write(" ");


            Writer.Write(string.IsNullOrEmpty(command.Code) ? command.GetType().Name : $"[{command.Code}] ");

            Writer.Write(" ");
            if (!string.IsNullOrEmpty(command.Annotation)) Writer.Write(command.Annotation);
            else Writer.Write(command.ToString());

            Writer.WriteLine();

            if (command.Exception != null)
            {
                Writer.WriteLine($"[ERROR]");
                var sb = new StringBuilder();
                RevealException(command.Exception,sb,0);
                Writer.Write(sb.ToString());
                if (!string.IsNullOrEmpty(command.Exception.StackTrace))
                {
                    Writer.WriteLine("---");
                    Writer.WriteLine(command.Exception.StackTrace);
                }
                Writer.WriteLine($"[/ERROR]");
            }
        }
    }
}