using System;
using System.Linq;

namespace CatFactory.TypeScript
{
    public class TypeScriptNamingConvention : INamingConvention
    {
        public string ValidName(string name)
        {
            // todo: add logic to validate name

            return string.Join("", name.Split(' ').Select(item => NamingConvention.GetPascalCase(item)));
        }

        public string GetInterfaceName(string value)
        {
            return NamingConvention.GetPascalCase(string.Format("I{0}", ValidName(value)));
        }

        public string GetClassName(string value)
        {
            return NamingConvention.GetPascalCase(ValidName(value));
        }

        public string GetFieldName(string value)
        {
            return string.Format("m_{0}", NamingConvention.GetCamelCase(ValidName(value)));
        }

        public string GetPropertyName(string value)
        {
            return NamingConvention.GetCamelCase(ValidName(value));
        }

        public string GetMethodName(string value)
        {
            return NamingConvention.GetCamelCase(ValidName(value));
        }

        public string GetParameterName(string value)
        {
            return NamingConvention.GetCamelCase(ValidName(value));
        }
    }
}
