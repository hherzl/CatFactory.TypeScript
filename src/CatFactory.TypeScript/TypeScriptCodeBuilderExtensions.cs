using System;
using System.Collections.Generic;

namespace CatFactory.TypeScript
{
    public static class TypeScriptCodeBuilderExtensions
    {
        public static void AddImport(this TypeScriptClassDefinition objectDefinition, IEnumerable<String> types, String from)
        {
            objectDefinition.Namespaces.Add(String.Format("{{ {0} }} from \"{1}\"", String.Join(", ", types), from));
        }

        public static void AddImport(this TypeScriptInterfaceDefinition objectDefinition, IEnumerable<String> types, String from)
        {
            objectDefinition.Namespaces.Add(String.Format("{{ {0} }} from \"{1}\"", String.Join(", ", types), from));
        }
    }
}
