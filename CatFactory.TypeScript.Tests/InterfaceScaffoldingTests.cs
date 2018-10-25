using System.Diagnostics;
using System.IO;
using CatFactory.ObjectOrientedProgramming;
using CatFactory.TypeScript.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class InterfaceScaffoldingTests
    {
        public string TscPath { get; }
        public string TsFilesPath { get; }
        public string OutPath { get; }

        public InterfaceScaffoldingTests()
        {
            TscPath = @"C:\Program Files (x86)\Microsoft SDKs\TypeScript\3.0\tsc.exe";
            TsFilesPath = @"C:\Temp\CatFactory.TypeScript";
            OutPath = @"c:\Temp\CatFactory.TypeScript\js";
        }

        [Fact]
        public void ScaffoldWarehouseServiceInterface()
        {
            var definition = new TypeScriptInterfaceDefinition
            {
                Name = "IWarehouseService",
                Methods =
                {
                    new MethodDefinition("Response", "getProducts", new ParameterDefinition("string", "productName")),
                    new MethodDefinition("Response", "getProduct", new ParameterDefinition("Product", "entity"))
                }
            };

            definition.AddImport("Response", "./Response");
            definition.AddImport("Product", "./Product");

            TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);

            Process.Start(TscPath, string.Format("{0} --outDir {1}", Path.Combine(TsFilesPath, "IWarehouseService.ts"), OutPath));
        }

        [Fact]
        public void TestRefactGamerInterface()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "Gamer",
                Properties =
                {
                    new PropertyDefinition("string", "firstName"),
                    new PropertyDefinition("string", "middleName"),
                    new PropertyDefinition("string", "lastName"),
                    new PropertyDefinition("string", "gender"),
                    new PropertyDefinition("Date", "birthDate")
                }
            };

            var interfaceDefinition = classDefinition.RefactInterface();

            Assert.True(classDefinition.Properties.Count == interfaceDefinition.Properties.Count);
            Assert.True(interfaceDefinition.Methods.Count == 0);
        }
    }
}
