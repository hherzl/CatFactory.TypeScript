using CatFactory.CodeFactory;
using Microsoft.Extensions.Logging;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptCodeBuilder : CodeBuilder
    {
        public TypeScriptCodeBuilder()
            : base()
        {
        }

        public TypeScriptCodeBuilder(ILogger<TypeScriptCodeBuilder> logger)
            : base(logger)
        {
        }

        public override string FileExtension
            => "ts";

        protected virtual string GetComment(string description)
            => string.Format("//{0}", description);

        protected virtual string GetTodo(string description)
            => string.Format("// todo: {0}", description);

        public virtual ICodeNamingConvention NamingConvention
            => new TypeScriptNamingConvention();
    }
}
