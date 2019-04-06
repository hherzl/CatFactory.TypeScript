namespace CatFactory.TypeScript.Tests
{
    public static class ScaffoldingPaths
    {
        public static string TscPath { get; }
        public static string TsFilesPath { get; }
        public static string OutPath { get; }

        static ScaffoldingPaths()
        {
            TscPath = @"C:\Program Files (x86)\Microsoft SDKs\TypeScript\3.1\tsc.exe";
            TsFilesPath = @"C:\Temp\CatFactory.TypeScript";
            OutPath = @"C:\Temp\CatFactory.TypeScript\js";
        }

        // todo: Fix this method

        ///// <summary>
        ///// Get the Last Known TypeScript Version
        ///// </summary>
        ///// <returns></returns>
        //static string WhereTsc()
        //{
        //    var LastKnownTypeScriptVersion = "3.3";
        //    var root = @"C:\Program Files (x86)\Microsoft SDKs\TypeScript";
        //    var path = Path.Combine(root, @"versions");
        //    var di = new DirectoryInfo(path);

        //    if (di.Exists)
        //        LastKnownTypeScriptVersion = XDocument.Load(di.EnumerateFileSystemInfos("*.props").Last().FullName).Root?.Value;

        //    return Path.Combine(root, LastKnownTypeScriptVersion, "tsc.exe");
        //}
    }
}
