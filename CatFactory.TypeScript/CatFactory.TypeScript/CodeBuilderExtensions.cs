using System.Collections.Generic;
using System.Text;
using CatFactory.CodeFactory;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public static class CodeBuilderExtensions
    {
        private static void AddAttributes(TypeScriptCodeBuilder codeBuilder, List<MetadataAttribute> attributes, StringBuilder output, int start)
        {
            foreach (var attribute in attributes)
            {
                var foo = new List<string>();

                //output.AppendFormat("@{0}", attribute.Name);
                //output.Append("(");

                foo.Add(string.Format("@{0}", attribute.Name));
                foo.Add("(");

                if (attribute.Sets.Count > 0)
                {
                    if (attribute.HasMembers)
                    {
                        output.Append("{");

                        foo.Add("{");

                        codeBuilder.Lines.Add(new CodeLine(string.Join("", foo)));

                        //output.AppendLine();

                        //codeBuilder.Lines.Add(new CodeLine());

                        for (var i = 0; i < attribute.Sets.Count; i++)
                        {
                            var set = attribute.Sets[i];

                            //output.AppendFormat("{0}{1}: {2}", codeBuilder.Indent(start + 1), set.Name, set.Value);

                            codeBuilder.Lines.Add(new CodeLine("{0}{1}: {2}{3}", codeBuilder.Indent(start + 1), set.Name, set.Value, i < attribute.Sets.Count - 1 ? "," : string.Empty));
                            
                            if (i < attribute.Sets.Count - 1)
                            {
                                //output.Append(",");
                                //output.AppendLine();

                                //codeBuilder.Lines.Add(new CodeLine());
                            }
                        }

                        //output.AppendLine();

                        //codeBuilder.Lines.Add(new CodeLine());
                    }

                    //output.Append("}");

                    //codeBuilder.Lines.Add(new CodeLine("}"));

                    codeBuilder.Lines.Add(new CodeLine("})"));
                }

                //output.Append(")");

                

                //output.AppendLine();
            }
        }

        public static void AddAttributes(this TypeScriptInterfaceBuilder codeBuilder, StringBuilder output, int start)
            => AddAttributes(codeBuilder, codeBuilder.ObjectDefinition.Attributes, output, start);

        public static void AddAttributes(this TypeScriptClassBuilder codeBuilder, StringBuilder output, int start)
            => AddAttributes(codeBuilder, codeBuilder.ObjectDefinition.Attributes, output, start);
    }
}
