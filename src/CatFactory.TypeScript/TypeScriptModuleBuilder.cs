using System.Collections.Generic;
using System.Text;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptModuleBuilder : CodeBuilder
    {
        public static void CreateFiles(string outputDirectory, string subdirectory, string moduleFileName, bool forceOverwrite, params TypeScriptModuleDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                var codeBuilder = new TypeScriptModuleBuilder(definition)
                {
                    OutputDirectory = outputDirectory,
                    ModuleFileName = moduleFileName,
                    ForceOverwrite = forceOverwrite
                };

                codeBuilder.CreateFile(subdirectory);
            }
        }

        public TypeScriptModuleBuilder(TypeScriptModuleDefinition module)
        {
            Module = module;
        }

        public TypeScriptModuleDefinition Module { get; private set; }

        public string ModuleFileName { get; set; }

        public override string FileName
            => string.IsNullOrEmpty(ModuleFileName) ? Module.Name : ModuleFileName;

        public override string FileExtension
            => "ts";

        public override string Code
        {
            get
            {
                var output = new StringBuilder();

                if (Module.Namespaces.Count > 0)
                {
                    foreach (var import in Module.Namespaces)
                    {
                        output.AppendFormat("import {0};", import);
                        output.AppendLine();
                    }

                    output.AppendLine();
                }

                var start = string.IsNullOrEmpty(Module.Name) ? 0 : 1;

                if (Module.Constants.Count > 0)
                {
                    foreach (var constant in Module.Constants)
                    {
                        var constantDefinition = new List<string>();

                        constantDefinition.Add("export");
                        constantDefinition.Add("const");

                        constantDefinition.Add(string.Format("{0}: {1}", constant.Name, constant.Type));

                        if (constant.Value != null)
                        {
                            var cast = constant.Value as TypeScriptObjectValue;

                            if (cast == null)
                            {
                                constantDefinition.Add("=");

                                constantDefinition.Add(constant.Value.ToString());

                                output.AppendFormat("{0}{1};", Indent(start), string.Join(" ", constantDefinition));
                                output.AppendLine();
                            }
                            else
                            {
                                if (cast.Value is IEnumerable<ILine>)
                                {
                                    output.AppendFormat("{0}{1} = {{", Indent(start), string.Join(" ", constantDefinition));
                                    output.AppendLine();

                                    var content = cast.Value as IEnumerable<ILine>;

                                    foreach (var line in content)
                                    {
                                        output.AppendFormat("{0}{1}", Indent(start + line.Indent), line.Content);
                                        output.AppendLine();
                                    }

                                    output.Append("};");
                                    output.AppendLine();
                                }
                            }
                        }

                        
                    }
                }

                return output.ToString();
            }
        }
    }
}
