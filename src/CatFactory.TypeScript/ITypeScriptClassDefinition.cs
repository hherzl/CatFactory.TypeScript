using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public interface ITypeScriptClassDefinition : IClassDefinition
    {
        bool Export { get; set; }
    }
}
