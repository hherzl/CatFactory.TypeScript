using System.Collections.Generic;
using CatFactory.CodeFactory;
using CatFactory.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.CodeFactory
{
    public static class TypeScriptCodeBuilderExtensions
    {
        private static void AddAttributes(TypeScriptCodeBuilder codeBuilder, List<MetadataAttribute> attributes, int start)
        {
            foreach (var attrib in attributes)
            {
                if (attrib.Sets.Count == 0)
                {
                    codeBuilder.Lines.Add(new CodeLine("@{0}()", attrib.Name));
                }
                else
                {
                    codeBuilder.Lines.Add(new CodeLine("@{0}({{", attrib.Name));

                    for (var i = 0; i < attrib.Sets.Count; i++)
                    {
                        var set = attrib.Sets[i];

                        codeBuilder.Lines.Add(new CodeLine("{0}{1}: {2}{3}", codeBuilder.Indent(start + 1), set.Name, set.Value, i < attrib.Sets.Count - 1 ? "," : string.Empty));
                    }

                    codeBuilder.Lines.Add(new CodeLine("})"));
                }
            }
        }

        public static void AddAttributes(this TypeScriptInterfaceBuilder codeBuilder, int start)
            => AddAttributes(codeBuilder, codeBuilder.ObjectDefinition.Attributes, start);

        public static void AddAttributes(this TypeScriptClassBuilder codeBuilder, int start)
            => AddAttributes(codeBuilder, codeBuilder.ObjectDefinition.Attributes, start);
    }
}
