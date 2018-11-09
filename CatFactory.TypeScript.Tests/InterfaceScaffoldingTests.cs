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
            
            foreach (var filePath in TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
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
                    new FieldDefinition("number", "m_id"),
                    new FieldDefinition("string", "m_firstName"),
                    new FieldDefinition("string", "m_middleName"),
                    new FieldDefinition("string", "m_lastName"),
                    new FieldDefinition("string", "m_gender"),
                    new FieldDefinition("Date", "m_birthDate")
                },
                Properties =
                {
                    new PropertyDefinition("number", "id"),
                    new PropertyDefinition("string", "firstName"),
                    new PropertyDefinition("string", "middleName"),
                    new PropertyDefinition("string", "lastName"),
                    new PropertyDefinition("string", "gender"),
                    new PropertyDefinition("Date", "birthDate")
                }
            };

            var interfaceDefinition = classDefinition.RefactInterface();

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, classDefinition);

            TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, interfaceDefinition);

            Assert.True(classDefinition.Properties.Count == interfaceDefinition.Properties.Count);
            Assert.True(interfaceDefinition.Methods.Count == 0);
        }
    }
}
