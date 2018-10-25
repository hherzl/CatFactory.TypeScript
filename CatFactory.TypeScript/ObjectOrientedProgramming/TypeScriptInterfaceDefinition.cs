using CatFactory.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.ObjectOrientedProgramming
{
    public class TypeScriptInterfaceDefinition : InterfaceDefinition, ITypeScriptInterfaceDefinition
    {
        public TypeScriptInterfaceDefinition()
            : base()
        {
        }

        public bool Export { get; set; } = true;
    }
}
