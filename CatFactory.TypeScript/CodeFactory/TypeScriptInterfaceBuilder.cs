using System.Collections.Generic;
using System.Linq;
using CatFactory.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using Microsoft.Extensions.Logging;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptInterfaceBuilder : TypeScriptCodeBuilder
    {
        public static IEnumerable<string> CreateFiles(string outputDirectory, string subdirectory, bool forceOverwrite, params TypeScriptInterfaceDefinition[] definitions)
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

                yield return codeBuilder.FilePath;
            }
        }

        public TypeScriptInterfaceBuilder()
            : base()
        {
        }

        public TypeScriptInterfaceBuilder(ILogger<TypeScriptInterfaceBuilder> logger)
            : base(logger)
        {
        }

        public new ITypeScriptInterfaceDefinition ObjectDefinition { get; set; }

        public override string FileName
            => ObjectDefinition.Name;

        public override void Translating()
        {
            if (ObjectDefinition.Namespaces.Count > 0)
            {
                foreach (var import in ObjectDefinition.Namespaces)
                {
                    Lines.Add(new CodeLine("import {0};", import));
                }

                Lines.Add(new CodeLine());
            }

            var start = 0;

            if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
            {
                Lines.Add(new CodeLine("namespace {0} {1}", ObjectDefinition.Namespace, "{"));

                start = 1;
            }

            this.AddAttributes(start);

            if (ObjectDefinition.Documentation.HasSummary)
                Lines.Add(new CodeLine("{0}/** {1} */", Indent(start), ObjectDefinition.Documentation.Summary));

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
                {
                    if (property.Documentation.HasSummary)
                    {
                        Lines.Add(new CommentLine("{0}/**", Indent(start + 1)));

                        Lines.Add(new CommentLine("{0}* {1}", Indent(start + 1), property.Documentation.Summary));

                        Lines.Add(new CommentLine("{0}*/", Indent(start + 1)));
                    }

                    Lines.Add(new CodeLine("{0}{1}: {2};", Indent(start + 1), property.Name, property.Type));
                }
            }

            if (ObjectDefinition.Methods.Count > 0)
            {
                foreach (var method in ObjectDefinition.Methods)
                {
                    if (method.Documentation.HasSummary)
                    {
                        Lines.Add(new CommentLine("{0}/**", Indent(start + 1)));

                        Lines.Add(new CommentLine("{0}* {1}", Indent(start + 1), method.Documentation.Summary));

                        foreach (var parameter in method.Parameters)
                        {
                            Lines.Add(new CodeLine("{0}* @{1} {2}", Indent(start + 1), parameter.Name, parameter.Documentation.Summary));
                        }

                        Lines.Add(new CommentLine("{0}*/", Indent(start + 1)));
                    }

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
