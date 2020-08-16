using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reinforced.Tecture.Testing
{
    interface IGenerating
    {
        void Dump(TextWriter tw);
    }

    public class GenerationOutput
    {
        private readonly IGenerating _gen;

        internal GenerationOutput(IGenerating gen)
        {
            _gen = gen;
        }

        public string ToFullString()
        {
            StringBuilder sb = new StringBuilder();
            using (var strw = new StringWriter(sb))
            {
                _gen.Dump(strw);
                strw.Flush();
                return sb.ToString();
            }
        }

        public void ToFile(string fileName,bool nobackup = false)
        {
            if (File.Exists(fileName))
            {
                if (nobackup)
                {
                    File.Delete(fileName);
                }
                else
                {
                    var backup = $"{fileName}.bak";
                    if (File.Exists(backup)) File.Delete(backup);
                    File.Move(fileName, backup);
                }
            }

            using (var fs = new StreamWriter(fileName))
            {
                _gen.Dump(fs);
            }
        }

    }
}
