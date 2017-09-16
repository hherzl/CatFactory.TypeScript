using System;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassDefinition : ClassDefinition, ITypeScriptClassDefinition
    {
        public TypeScriptClassDefinition()
        {
        }

        public Boolean Export { get; set; } = true;
    }
}
