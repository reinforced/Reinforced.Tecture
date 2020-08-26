using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Reinforced.Samples.ToyFactory.Tests
{
    public static class Root
    {
        private static readonly string _projectRoot;

        public static string RootNamespace
        {
            get { return typeof(Root).Namespace; }
        }

        public static string ProjectRoot
        {
            get { return _projectRoot; }
        }

        public static string FolderWithType(Type t)
        {
            var ns = t.Namespace;
            if (ns.StartsWith(RootNamespace))
            {
                ns = ns.Substring(RootNamespace.Length, ns.Length - RootNamespace.Length);
                ns = ns.TrimStart('.');
            }
            var pth = ns.Replace('.', Path.DirectorySeparatorChar);
            return Path.Combine(ProjectRoot, pth);
        }

        static Root()
        {
            var a = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new Uri(a);

            var path = uri.LocalPath;

            var folder = Path.GetDirectoryName(path);
            _projectRoot = GetProjectRoot(new DirectoryInfo(folder));

        }

        static string GetProjectRoot(DirectoryInfo di)
        {
            if (di.Name == "bin") return di.Parent == null ? string.Empty : di.Parent.FullName;
            return GetProjectRoot(di.Parent);
        }
    }
}
