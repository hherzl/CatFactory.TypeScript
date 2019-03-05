namespace CatFactory.TypeScript.Tests
{
    using System.Linq;
    using System.Xml.XPath;
    using Microsoft.EntityFrameworkCore.Internal;

    public static class ScaffoldingPaths
    {
        public static string TscPath { get; }
        public static string TsFilesPath { get; }
        public static string OutPath { get; }

        static ScaffoldingPaths()
        {
            ScaffoldingPaths.TscPath = ScaffoldingPaths.WhereTsc();
            ScaffoldingPaths.TsFilesPath = @"C:\Temp\CatFactory.TypeScript";
            ScaffoldingPaths.OutPath = @"c:\Temp\CatFactory.TypeScript\js";
        }

        /// <summary>
        /// Get the Last Known TypeScript Version
        /// </summary>
        /// <returns></returns>
        static string WhereTsc()
        {
            var LastKnownTypeScriptVersion = "3.0";
            var root = @"C:\Program Files (x86)\Microsoft SDKs\TypeScript";
            var path = System.IO.Path.Combine(root, @"versions");
            var di = new System.IO.DirectoryInfo(path);
            if (di.Exists)
            {
                LastKnownTypeScriptVersion = System.Xml.Linq.XDocument.Load
                    (di.EnumerateFileSystemInfos("*.props").Last().FullName).Root?.Value;
            }
            return System.IO.Path.Combine(root, LastKnownTypeScriptVersion, "tsc.exe");
        }
    }
}
