using CatFactory.CodeFactory;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptCodeBuilder : CodeBuilder
    {
        public override string FileExtension
            => "ts";

        public TypeScriptNamingConvention NamingConvention
            => new TypeScriptNamingConvention();
    }
}
