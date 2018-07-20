using System;
using System.Linq;
using CatFactory.Diagnostics;
using CatFactory.OOP;
using Microsoft.Extensions.Logging;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassDefinitionValidator : ClassDefinitionValidator
    {
        public override ValidationResult Validate(IClassDefinition classDefinition)
        {
            if (classDefinition == null)
                throw new ArgumentNullException(nameof(classDefinition));

            var validationResult = base.Validate(classDefinition);

            if (classDefinition.Constructors.Count > 1)
            {
                validationResult.ValidationMessages.Add(new ValidationMessage
                {
                    LogLevel = LogLevel.Error,
                    Message = "Class definitions on typescript only have one constructor"
                });
            }

            foreach (var method in classDefinition.Methods)
            {
                if (classDefinition.Methods.Where(m => m.Name == method.Name).Count() > 1)
                {
                    validationResult.ValidationMessages.Add(new ValidationMessage
                    {
                        LogLevel = LogLevel.Error,
                        Message = string.Format("There is more than one method with name '{0}'", method.Name)
                    });
                }
            }

            return validationResult;
        }
    }
}
