using CatFactory.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.ObjectOrientedProgramming
{
    public class TypeScriptEnumDefinition : EnumDefinition, ITypeScriptEnumDefinition
    {
        public TypeScriptEnumDefinition()
            : base()
        {
        }

        public bool Export { get; }
    }
}
