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

            foreach (var filePath in TypeScriptInterfaceBuilder.CreateFiles(ScaffoldingPaths.TsFilesPath, string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void TestRefactGamerInterface()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "Person",
                Fields =
                {
                    new FieldDefinition(AccessModifier.Public, "number", "id"),
                    new FieldDefinition(AccessModifier.Public, "string", "firstName"),
                    new FieldDefinition(AccessModifier.Public, "string", "middleName"),
                    new FieldDefinition(AccessModifier.Public, "string", "lastName"),
                    new FieldDefinition(AccessModifier.Public, "string", "gender"),
                    new FieldDefinition(AccessModifier.Public, "Date", "birthDate")
                }
            };

            var interfaceDefinition = classDefinition.RefactInterface();

            TypeScriptClassBuilder.CreateFiles(ScaffoldingPaths.TsFilesPath, string.Empty, true, classDefinition);

            TypeScriptInterfaceBuilder.CreateFiles(ScaffoldingPaths.TsFilesPath, string.Empty, true, interfaceDefinition);

            Assert.True(classDefinition.Properties.Count == interfaceDefinition.Properties.Count);
            Assert.True(interfaceDefinition.Methods.Count == 0);
        }
    }
}
