using System.Collections.Generic;
using CatFactory.CodeFactory;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public static class TypeScriptCodeBuilderExtensions
    {
        private static void AddAttributes(TypeScriptCodeBuilder codeBuilder, List<MetadataAttribute> attributes, int start)
        {
            foreach (var attribute in attributes)
            {
                var declaration = new List<string>
                {
                    string.Format("@{0}", attribute.Name),
                    "("
                };

                if (attribute.Sets.Count > 0)
                {
                    if (attribute.HasMembers)
                    {
                        declaration.Add("{");

                        codeBuilder.Lines.Add(new CodeLine(string.Join("", declaration)));

                        for (var i = 0; i < attribute.Sets.Count; i++)
                        {
                            var set = attribute.Sets[i];

                            codeBuilder.Lines.Add(new CodeLine("{0}{1}: {2}{3}", codeBuilder.Indent(start + 1), set.Name, set.Value, i < attribute.Sets.Count - 1 ? "," : string.Empty));
                        }
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
