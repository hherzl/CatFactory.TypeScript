using System;
using System.Linq;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptNamingConvention : INamingConvention
    {
        public String ValidName(String name)
        {
            // todo: add logic to validate name

            return String.Join("", name.Split(' ').Select(item => NamingConvention.GetPascalCase(item)));
        }

        public String GetInterfaceName(String value)
        {
            return NamingConvention.GetPascalCase(String.Format("I{0}", ValidName(value)));
        }

        public String GetClassName(String value)
        {
            return NamingConvention.GetPascalCase(ValidName(value));
        }

        public String GetFieldName(String value)
        {
            return String.Format("m_{0}", NamingConvention.GetCamelCase(ValidName(value)));
        }

        public String GetPropertyName(String value)
        {
            return NamingConvention.GetCamelCase(ValidName(value));
        }

        public String GetMethodName(String value)
        {
            return NamingConvention.GetCamelCase(ValidName(value));
        }

        public String GetParameterName(String value)
        {
            return NamingConvention.GetCamelCase(ValidName(value));
        }
    }
}
