using System.Diagnostics;
using System.IO;
using CatFactory.ObjectOrientedProgramming;
using CatFactory.TypeScript.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class EnumScaffoldingTests
    {
        [Fact]
        public void ScaffoldDirectionEnum()
        {
            var definition = new TypeScriptEnumDefinition
            {
                Name = "Direction",
                Sets =
                {
                    new NameValue { Name = "Up", Value = "100" },
                    new NameValue { Name = "Down", Value = "200" },
                    new NameValue { Name = "Right", Value = "300" },
                    new NameValue { Name = "Left", Value = "400" }
                }
            };

            foreach (var filePath in TypeScriptEnumBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void ScaffoldConnectionStatusEnum()
        {
            var definition = new TypeScriptEnumDefinition
            {
                Documentation = new Documentation
                {
                    Summary = "Represents the state for connections"
                },
                Name = "ConnectionState",
                Sets =
                {
                    new NameValue { Name = "Broken" },
                    new NameValue { Name = "Closed" },
                    new NameValue { Name = "Connecting" },
                    new NameValue { Name = "Executing" },
                    new NameValue { Name = "Fetching" },
                    new NameValue { Name = "Open" }
                }
            };

            foreach (var filePath in TypeScriptEnumBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }
    }
}
