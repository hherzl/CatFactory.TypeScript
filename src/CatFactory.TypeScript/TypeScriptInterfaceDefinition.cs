using System;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptInterfaceDefinition : InterfaceDefinition, ITypeScriptInterfaceDefinition
    {
        public TypeScriptInterfaceDefinition()
        {
        }

        public Boolean Export { get; set; } = true;
    }
}
