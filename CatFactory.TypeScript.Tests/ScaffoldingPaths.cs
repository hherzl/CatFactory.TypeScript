namespace CatFactory.TypeScript.Tests
{
    public static class ScaffoldingPaths
    {
        public static string TscPath { get; }
        public static string TsFilesPath { get; }
        public static string OutPath { get; }

        static ScaffoldingPaths()
        {
            TscPath = @"C:\Program Files (x86)\Microsoft SDKs\TypeScript\3.0\tsc.exe";
            TsFilesPath = @"C:\Temp\CatFactory.TypeScript";
            OutPath = @"c:\Temp\CatFactory.TypeScript\js";
        }
    }
}
