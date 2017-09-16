using System;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public interface ITypeScriptClassDefinition : IClassDefinition
    {
        Boolean Export { get; set; }
    }
}
