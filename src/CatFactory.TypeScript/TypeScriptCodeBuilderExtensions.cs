using System.Collections.Generic;

namespace CatFactory.TypeScript
{
    public static class TypeScriptCodeBuilderExtensions
    {
        public static void AddImport(this TypeScriptClassDefinition objectDefinition, IEnumerable<string> types, string from)
            => objectDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", string.Join(", ", types), from));

        public static void AddImport(this TypeScriptClassDefinition objectDefinition, string type, string from)
            => objectDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", type, from));

        public static void AddImport(this TypeScriptInterfaceDefinition objectDefinition, IEnumerable<string> types, string from)
            => objectDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", string.Join(", ", types), from));

        public static void AddImport(this TypeScriptInterfaceDefinition objectDefinition, string type, string from)
            => objectDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", type, from));

        public static void AddImport(this TypeScriptModuleDefinition objectDefinition, IEnumerable<string> types, string from)
            => objectDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", string.Join(", ", types), from));

        public static void AddImport(this TypeScriptModuleDefinition objectDefinition, string type, string from)
            => objectDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", type, from));
    }
}
