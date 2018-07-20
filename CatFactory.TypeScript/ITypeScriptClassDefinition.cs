using System.Collections.Generic;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public interface ITypeScriptClassDefinition : IClassDefinition
    {
        bool Export { get; set; }

        new List<TypeScriptClassConstructorDefinition> Constructors { get; set; }
    }
}
