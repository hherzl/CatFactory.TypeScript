using System.Linq;
using CatFactory.OOP;
using Microsoft.Extensions.Logging;
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

            definition.Constructors.Add(new ClassConstructorDefinition());

            definition.Properties.Add(new PropertyDefinition("string", "firstName"));
            definition.Properties.Add(new PropertyDefinition("string", "middleName"));
            definition.Properties.Add(new PropertyDefinition("string", "lastName"));
            definition.Properties.Add(new PropertyDefinition("string", "gender"));
            definition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var validator = new TypeScriptClassDefinitionValidator();

            var validationResult = validator.Validate(definition);

            Assert.True(validationResult.ValidationMessages.Where(item => item.LogLevel == LogLevel.Error || item.LogLevel == LogLevel.Critical).Count() == 0);
        }

        [Fact]
        public void TestClassValidationWithErrors()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "Acme",
                Name = "SalesUow"
            };

            definition.Constructors.Add(new ClassConstructorDefinition());
            definition.Constructors.Add(new ClassConstructorDefinition());

            definition.Properties.Add(new PropertyDefinition("DbContext", "DbContext"));
            definition.Properties.Add(new PropertyDefinition("DbContext", "DbContext"));

            definition.Methods.Add(new MethodDefinition("number", "SaveChanges"));
            definition.Methods.Add(new MethodDefinition("number", "SaveChanges"));

            var validator = new TypeScriptClassDefinitionValidator();

            var validationResult = validator.Validate(definition);

            Assert.True(validationResult.ValidationMessages.Where(item => item.LogLevel == LogLevel.Error || item.LogLevel == LogLevel.Critical).Count() > 0);
        }
    }
}
