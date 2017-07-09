using System;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptCodeBuilder : CodeBuilder
    {
        public override string FileExtension
            => "ts";

        public new TypeScriptNamingConvention NamingConvention
            => new TypeScriptNamingConvention();
    }
}
