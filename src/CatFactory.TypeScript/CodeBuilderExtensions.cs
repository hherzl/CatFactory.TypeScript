using System;
using System.Collections.Generic;
using System.Text;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public static class CodeBuilderExtensions
    {
        private static void AddAttributes(TypeScriptCodeBuilder codeBuilder, List<MetadataAttribute> attributes, StringBuilder output, Int32 start)
        {
            foreach (var attribute in attributes)
            {
                output.AppendFormat("@{0}", attribute.Name);
                output.Append("(");

                if (attribute.Sets.Count > 0)
                {
                    if (attribute.HasMembers)
                    {
                        output.Append("{");
                        output.AppendLine();

                        for (var i = 0; i < attribute.Sets.Count; i++)
                        {
                            var set = attribute.Sets[i];

                            output.AppendFormat("{0}{1}: {2}", codeBuilder.Indent(start + 1), set.Name, set.Value);

                            if (i < attribute.Sets.Count - 1)
                            {
                                output.Append(",");
                                output.AppendLine();
                            }
                        }

                        output.AppendLine();
                    }

                    output.Append("}");
                }

                output.Append(")");

                output.AppendLine();
            }
        }

        public static void AddAttributes(this TypeScriptInterfaceBuilder codeBuilder, StringBuilder output, Int32 start)
        {
            AddAttributes(codeBuilder, codeBuilder.ObjectDefinition.Attributes, output, start);
        }

        public static void AddAttributes(this TypeScriptClassBuilder codeBuilder, StringBuilder output, Int32 start)
        {
            AddAttributes(codeBuilder, codeBuilder.ObjectDefinition.Attributes, output, start);
        }
    }
}
