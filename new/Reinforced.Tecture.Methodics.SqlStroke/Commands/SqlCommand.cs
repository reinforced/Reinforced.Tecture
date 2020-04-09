using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Methodics.SqlStroke.Commands
{
    [CommandCode("SQL")]
    public class SqlCommand : CommandBase
    {
        public string Command { get; internal set; }

        public object[] Parameters { get; internal set; }

        public override string ToString()
        {
            return String.Format(Command, Parameters);
        }

        public SqlCommand(string commandText, object[] parameters = null)
        {
            OriginalParameters = parameters;
            // here we do parameters reformatting
            // which brings arrays support to our command
            // It made for correct WHERE Id In (...) composing
            // with ability to pass array of ints here

            if (parameters == null) parameters = new object[0];
            if (parameters.Any(c => c != null && c.GetType().IsArray))
            {
                List<object> newParameters = new List<object>();
                string[] reformat = new string[parameters.Length];
                int cnt = 0;
                for (int index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    var at = parameter?.GetType();
                    if (parameter != null && at.IsArray)
                    {
                        var p = (Array)parameter;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < p.Length; i++)
                        {
                            var val = p.GetValue(i);
                            if (val != null && val.GetType().IsEnum) val = Convert.ToInt64(val);
                            sb.Append(val);
                            if (i < p.Length - 1) sb.Append(", ");
                        }
                        reformat[index] = sb.ToString();
                    }
                    else
                    {
                        newParameters.Add(parameter);
                        reformat[index] = string.Concat("{", cnt, "}");
                        cnt++;
                    }
                }
                commandText = string.Format(commandText, reformat);
                parameters = newParameters.ToArray();
            }

            if (parameters.Length > 0)
            {
                commandText = string.Format(commandText, parameters.Select((x, i) => $"@p{i}").Cast<object>().ToArray());
            }
            Command = commandText;
            Parameters = parameters;
        }

        /// <summary>
        /// Gets original parameters of command without mounted-in arrays
        /// </summary>
        public object[] OriginalParameters { get; private set; }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Annotation)) tw.Write(Annotation);
            else tw.Write("Direct SQL will be sent to DB");
            if (Debug != null) tw.Write($" ({Debug.Location})");
            tw.WriteLine(":");

            tw.WriteLine("\t----------");
            tw.WriteLine($"\t {Command}");
            if (Parameters.Any())
            {
                tw.WriteLine("\t---");
                tw.WriteLine($"\t {string.Join(", ", Parameters.Select((o, idx) => $"@p{idx} = {o}"))}");
            }

            tw.Write("\t----------");
        }
    }
}
