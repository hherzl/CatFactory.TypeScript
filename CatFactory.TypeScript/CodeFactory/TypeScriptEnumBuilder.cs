using System.Collections.Generic;
using System.Linq;
using CatFactory.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using Microsoft.Extensions.Logging;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptEnumBuilder : TypeScriptCodeBuilder
    {
        public static IEnumerable<string> CreateFiles(string outputDirectory, string subdirectory, bool forceOverwrite, params TypeScriptEnumDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                var codeBuilder = new TypeScriptEnumBuilder
                {
                    OutputDirectory = outputDirectory,
                    ForceOverwrite = forceOverwrite,
                    ObjectDefinition = definition
                };

                codeBuilder.CreateFile(subdirectory);

                yield return codeBuilder.FilePath;
            }
        }

        public TypeScriptEnumBuilder()
            : base()
        {
        }

        public TypeScriptEnumBuilder(ILogger<TypeScriptEnumBuilder> logger)
            : base(logger)
        {
        }

        public new ITypeScriptEnumDefinition ObjectDefinition { get; set; }

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

            if (ObjectDefinition.Documentation.HasSummary)
                Lines.Add(new CodeLine("{0}/** {1} */", Indent(start), ObjectDefinition.Documentation.Summary));

            var declaration = new List<string>
            {
                ObjectDefinition.Export ? "export " : string.Empty,
                "enum",
                ObjectDefinition.Name,
                "{"
            };

            Lines.Add(new CodeLine("{0}{1}", Indent(start), string.Join(" ", declaration.Where(item => !string.IsNullOrEmpty(item)))));

            for (var i = 0; i < ObjectDefinition.Sets.Count; i++)
            {
                var set = ObjectDefinition.Sets[i];
                var separator = i < ObjectDefinition.Sets.Count - 1 ? "," : string.Empty;

                if (string.IsNullOrEmpty(set.Value))
                    Lines.Add(new CodeLine("{0}{1}{2}", Indent(start + 1), set.Name, separator));
                else
                    Lines.Add(new CodeLine("{0}{1} = {2}{3}", Indent(start + 1), set.Name, set.Value, separator));
            }

            Lines.Add(new CodeLine("{0}{1}", Indent(start), "}"));

            if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
                Lines.Add(new CodeLine("}"));
        }
    }
}
