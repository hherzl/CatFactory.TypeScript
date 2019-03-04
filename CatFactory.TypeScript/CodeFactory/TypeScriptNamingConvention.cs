using System.Linq;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptNamingConvention : ICodeNamingConvention
    {
        // todo: add logic to validate name
        public string ValidName(string name)
            => string.Join("", name.Split(' ').Select(item => NamingConvention.GetPascalCase(item)));

        public string GetNamespace(params string[] values)
            => ValidName(string.Join(".", values.Select(item => item)));

        public string GetInterfaceName(string value)
            => ValidName($"I{NamingConvention.GetPascalCase(value)}");

        public string GetClassName(string value)
            => ValidName(NamingConvention.GetPascalCase(value));

        public string GetConstantName(string value)
            => ValidName(NamingConvention.GetCamelCase(value));

        public string GetFieldName(string value)
            => ValidName(NamingConvention.GetCamelCase(value));

        public string GetPropertyName(string value)
            => ValidName(NamingConvention.GetCamelCase(value));

        public string GetMethodName(string value)
            => ValidName(NamingConvention.GetCamelCase(value));

        public string GetParameterName(string value)
            => ValidName(NamingConvention.GetCamelCase(value));
    }
}
