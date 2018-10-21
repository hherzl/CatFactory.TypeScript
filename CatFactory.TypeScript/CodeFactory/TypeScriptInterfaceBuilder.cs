using System.Collections.Generic;
using System.Linq;
using CatFactory.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptInterfaceBuilder : TypeScriptCodeBuilder
    {
        public static void CreateFiles(string outputDirectory, string subdirectory, bool forceOverwrite, params TypeScriptInterfaceDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                var codeBuilder = new TypeScriptInterfaceBuilder
                {
                    OutputDirectory = outputDirectory,
                    ForceOverwrite = forceOverwrite,
                    ObjectDefinition = definition
                };

                codeBuilder.CreateFile(subdirectory);
            }
        }

        public TypeScriptInterfaceBuilder()
        {
        }

        public new ITypeScriptInterfaceDefinition ObjectDefinition { get; set; }

        public override string FileName
            => ObjectDefinition.Name;

        public override void Translating()
        {
            var start = 0;

            if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
            {
                Lines.Add(new CodeLine("namespace {0} {1}", ObjectDefinition.Namespace, "{"));

                start = 1;
            }

            foreach (var attribute in ObjectDefinition.Attributes)
            {
                var dec = new List<string>
                {
                    string.Format("{0}@{1}(", Indent(start), attribute.Name)
                };

                if (attribute.Sets.Count > 0)
                {
                    dec.Add("{");
                    dec.Add("\r\n");

                    for (var i = 0; i < attribute.Sets.Count; i++)
                    {
                        dec.Add(string.Format("{0}{1}", Indent(start + 1), attribute.Sets[i]));

                        if (i < attribute.Sets.Count - 1)
                            dec.Add(";");

                        dec.Add("\r\n");
                    }

                    dec.Add("}");
                }
            }

            var declaration = new List<string>
            {
                string.Format("{0}", ObjectDefinition.Export ? "export" : string.Empty),
                "interface",
                ObjectDefinition.Name
            };

            if (ObjectDefinition.HasInheritance && ObjectDefinition.Implements.Count > 0)
            {
                declaration.Add("implements");
                declaration.Add(string.Join(", ", ObjectDefinition.Implements));
            }

            declaration.Add("{");

            Lines.Add(new CodeLine("{0}{1}", Indent(start), string.Join(" ", declaration)));

            if (ObjectDefinition.Properties.Count > 0)
            {
                foreach (var property in ObjectDefinition.Properties)
                    Lines.Add(new CodeLine("{0}{1}: {2};", Indent(start + 1), property.Name, property.Type));
            }

            if (ObjectDefinition.Methods.Count > 0)
            {
                foreach (var method in ObjectDefinition.Methods)
                {
                    var parameters = string.Join(", ", method.Parameters.Select(item => string.Format("{0}: {1}", item.Name, item.Type)));

                    Lines.Add(new CodeLine("{0}{1}({2}): {3};", Indent(start + 1), method.Name, method.Parameters.Count == 0 ? string.Empty : parameters, method.Type));
                }
            }

            Lines.Add(new CodeLine("{0}{1}", Indent(start), "}"));

            if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
                Lines.Add(new CodeLine("}"));
        }
    }
}
