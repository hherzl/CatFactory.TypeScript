using System.Collections.Generic;
using CatFactory.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.ObjectOrientedProgramming
{
    public interface ITypeScriptClassDefinition : IClassDefinition
    {
        bool Export { get; set; }

        new List<TypeScriptClassConstructorDefinition> Constructors { get; set; }
    }
}
