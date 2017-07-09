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
            var classDefinition = new TypeScriptClassDefinition
            {
                Namespace = "Acme",
                Name = "SalesRepository"
            };

            classDefinition.AddImport("IRepository", "./IRepository");

            classDefinition.Constructors.Add(new ClassConstructorDefinition());

            classDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            classDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var validator = new TypeScriptClassDefinitionValidator();

            var validations = validator.Validate(classDefinition).ToList();

            Assert.True(validations.Where(item => item.LogLevel == LogLevel.Error || item.LogLevel == LogLevel.Critical).Count() == 0);
        }

        [Fact]
        public void TestClassValidationWithErrors()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Namespace = "Acme",
                Name = "SalesUow"
            };

            classDefinition.Constructors.Add(new ClassConstructorDefinition());
            classDefinition.Constructors.Add(new ClassConstructorDefinition());

            classDefinition.Properties.Add(new PropertyDefinition("DbContext", "DbContext"));
            classDefinition.Properties.Add(new PropertyDefinition("DbContext", "DbContext"));

            classDefinition.Methods.Add(new MethodDefinition("number", "SaveChanges"));
            classDefinition.Methods.Add(new MethodDefinition("number", "SaveChanges"));

            var validator = new TypeScriptClassDefinitionValidator();

            var validations = validator.Validate(classDefinition).ToList();

            Assert.True(validations.Where(item => item.LogLevel == LogLevel.Error || item.LogLevel == LogLevel.Critical).Count() > 0);
        }
    }
}
