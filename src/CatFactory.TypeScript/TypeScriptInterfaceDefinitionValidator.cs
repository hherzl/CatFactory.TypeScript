using System;
using System.Collections.Generic;
using System.Linq;
using CatFactory.Diagnostics;
using CatFactory.OOP;
using Microsoft.Extensions.Logging;

namespace CatFactory.TypeScript
{
    public class TypeScriptInterfaceDefinitionValidator : InterfaceDefinitionValidator
    {
        public override IEnumerable<ValidationMessage> Validate(IInterfaceDefinition interfaceDefinition)
        {
            if (interfaceDefinition == null)
            {
                throw new ArgumentNullException(nameof(interfaceDefinition));
            }

            foreach (var method in interfaceDefinition.Methods)
            {
                if (interfaceDefinition.Methods.Where(m => m.Name == method.Name).Count() > 1)
                {
                    yield return new ValidationMessage
                    {
                        LogLevel = LogLevel.Error,
                        Message = string.Format("There is more than one method with name '{0}'", method.Name)
                    };
                }
            }

            var validations = base.Validate(interfaceDefinition);

            foreach (var item in validations)
            {
                yield return item;
            }
        }
    }
}
