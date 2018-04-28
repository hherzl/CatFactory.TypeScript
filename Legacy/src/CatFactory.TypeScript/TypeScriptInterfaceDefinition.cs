using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptInterfaceDefinition : InterfaceDefinition, ITypeScriptInterfaceDefinition
    {
        public TypeScriptInterfaceDefinition()
        {
        }

        public bool Export { get; set; } = true;
    }
}
