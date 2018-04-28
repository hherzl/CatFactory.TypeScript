using System.Collections.Generic;
using CatFactory.OOP;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class InterfaceGenerationTests
    {
        [Fact]
        public void TestTypeScriptInterfaceWithPropertiesGeneration()
        {
            var definition = new TypeScriptInterfaceDefinition
            {
                Name = "IPerson"
            };

            definition.Properties.Add(new PropertyDefinition("string", "firstName"));
            definition.Properties.Add(new PropertyDefinition("string", "middleName"));
            definition.Properties.Add(new PropertyDefinition("string", "lastName"));
            definition.Properties.Add(new PropertyDefinition("string", "gender"));
            definition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptInterfaceWithNamespaceAndPropertiesGeneration()
        {
            var definition = new TypeScriptInterfaceDefinition
            {
                Namespace = "ContactManager",
                Name = "IContact"
            };

            definition.Properties.Add(new PropertyDefinition("string", "firstName"));
            definition.Properties.Add(new PropertyDefinition("string", "middleName"));
            definition.Properties.Add(new PropertyDefinition("string", "lastName"));
            definition.Properties.Add(new PropertyDefinition("string", "gender"));
            definition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestRefactInterface()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "Gamer",
                Implements = new List<string>
                {
                    "IGamer"
                }
            };

            classDefinition.AddImport("IGamer", "./IGamer");

            classDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, classDefinition);

            var interfaceDefinition = classDefinition.RefactInterface();

            TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, interfaceDefinition);
        }

        [Fact]
        public void TestTypeScriptRepositoryGeneration()
        {
            var definition = new TypeScriptInterfaceDefinition
            {
                Name = "ISalesRepository"
            };

            definition.Properties.Add(new PropertyDefinition("DbContext", "dbContext"));
            definition.Methods.Add(new MethodDefinition("number", "saveChanges"));
            definition.Methods.Add(new MethodDefinition("number", "saveChangesAsync"));

            TypeScriptInterfaceBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }
    }
}
