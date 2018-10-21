using CatFactory.OOP;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using CatFactory.TypeScript.ObjectOrientedProgramming.Validation;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void TestClassValidation()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "Acme",
                Name = "SalesRepository"
            };

            definition.AddImport("IRepository", "./IRepository");

            definition.Constructors.Add(new TypeScriptClassConstructorDefinition());

            definition.Properties.Add(new PropertyDefinition("string", "firstName"));
            definition.Properties.Add(new PropertyDefinition("string", "middleName"));
            definition.Properties.Add(new PropertyDefinition("string", "lastName"));
            definition.Properties.Add(new PropertyDefinition("string", "gender"));
            definition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var validator = new TypeScriptClassDefinitionValidator();

            var validationResult = validator.Validate(definition);

            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void TestClassValidationWithErrors()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "Acme",
                Name = "SalesUow"
            };

            definition.Constructors.Add(new TypeScriptClassConstructorDefinition());
            definition.Constructors.Add(new TypeScriptClassConstructorDefinition());

            definition.Properties.Add(new PropertyDefinition("DbContext", "DbContext"));
            definition.Properties.Add(new PropertyDefinition("DbContext", "DbContext"));

            definition.Methods.Add(new MethodDefinition("number", "SaveChanges"));
            definition.Methods.Add(new MethodDefinition("number", "SaveChanges"));

            var validator = new TypeScriptClassDefinitionValidator();

            var validationResult = validator.Validate(definition);

            Assert.False(validationResult.IsValid);
        }
    }
}
