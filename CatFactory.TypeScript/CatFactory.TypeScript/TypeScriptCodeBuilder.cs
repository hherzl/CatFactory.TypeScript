using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptCodeBuilder : CodeBuilder
    {
        public override string FileExtension
            => "ts";

        public TypeScriptNamingConvention NamingConvention
            => new TypeScriptNamingConvention();
    }
}
