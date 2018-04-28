using System.Collections.Generic;

namespace CatFactory.TypeScript
{
    public static class TypeScriptCodeBuilderExtensions
    {
        public static void AddImport(this TypeScriptClassDefinition classDefinition, IEnumerable<string> types, string from)
            => classDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", string.Join(", ", types), from));

        public static void AddImport(this TypeScriptClassDefinition classDefinition, string type, string from)
            => classDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", type, from));

        public static void AddImport(this TypeScriptInterfaceDefinition interfaceDefinition, IEnumerable<string> types, string from)
            => interfaceDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", string.Join(", ", types), from));

        public static void AddImport(this TypeScriptInterfaceDefinition interfaceDefinition, string type, string from)
            => interfaceDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", type, from));

        public static void AddImport(this TypeScriptModuleDefinition moduleDefinition, IEnumerable<string> types, string from)
            => moduleDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", string.Join(", ", types), from));

        public static void AddImport(this TypeScriptModuleDefinition moduleDefinition, string type, string from)
            => moduleDefinition.Namespaces.Add(string.Format("{{ {0} }} from \"{1}\"", type, from));
    }
}
