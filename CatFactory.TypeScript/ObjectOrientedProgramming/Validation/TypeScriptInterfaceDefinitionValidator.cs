using System;
using System.Linq;
using CatFactory.Diagnostics;
using CatFactory.OOP;
using Microsoft.Extensions.Logging;

namespace CatFactory.TypeScript.ObjectOrientedProgramming.Validation
{
    public class TypeScriptInterfaceDefinitionValidator : InterfaceDefinitionValidator
    {
        public override ValidationResult Validate(IInterfaceDefinition interfaceDefinition)
        {
            if (interfaceDefinition == null)
                throw new ArgumentNullException(nameof(interfaceDefinition));

            var validationResult = base.Validate(interfaceDefinition);

            foreach (var method in interfaceDefinition.Methods)
            {
                if (interfaceDefinition.Methods.Where(m => m.Name == method.Name).Count() > 1)
                    validationResult.ValidationMessages.Add(new ValidationMessage(LogLevel.Error, string.Format("There is more than one method with name '{0}'", method.Name)));
            }

            return validationResult;
        }
    }
}
