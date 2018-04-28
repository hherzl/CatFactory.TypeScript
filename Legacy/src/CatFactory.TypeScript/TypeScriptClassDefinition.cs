using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassDefinition : ClassDefinition, ITypeScriptClassDefinition
    {
        public TypeScriptClassDefinition()
        {
        }

        public bool Export { get; set; } = true;
    }
}
