using System.Collections.Generic;
using System.Linq;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassBuilder : TypeScriptCodeBuilder
    {
        public static void CreateFiles(string outputDirectory, string subdirectory, bool forceOverwrite, params TypeScriptClassDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                var codeBuilder = new TypeScriptClassBuilder
                {
                    OutputDirectory = outputDirectory,
                    ForceOverwrite = forceOverwrite,
                    ObjectDefinition = definition
                };

                codeBuilder.CreateFile(subdirectory);
            }
        }

        public TypeScriptClassBuilder()
        {
        }

        public new ITypeScriptClassDefinition ObjectDefinition { get; set; }

        public override string FileName
            => ObjectDefinition.Name;

        protected virtual string GetComment(string description)
            => string.Format("//{0}", description);

        protected string GetTodo(string description)
            => string.Format("// todo: {0}", description);

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

            var classDeclaration = new List<string>
            {
                ObjectDefinition.Export ? "export" : string.Empty,
                "class",
                ObjectDefinition.Name
            };

            if (ObjectDefinition.HasInheritance)
            {
                if (!string.IsNullOrEmpty(ObjectDefinition.BaseClass))
                {
                    classDeclaration.Add("extends");
                    classDeclaration.Add(ObjectDefinition.BaseClass);
                }

                if (ObjectDefinition.Implements.Count > 0)
                {
                    classDeclaration.Add("implements");
                    classDeclaration.Add(string.Join(", ", ObjectDefinition.Implements));
                }
            }

            classDeclaration.Add("{");

            Lines.Add(new CodeLine("{0}{1}", Indent(start), string.Join(" ", classDeclaration)));

            if (ObjectDefinition.Fields.Count > 0)
            {
                for (var i = 0; i < ObjectDefinition.Fields.Count; i++)
                {
                    var field = ObjectDefinition.Fields[i];

                    var fieldDefinition = new List<string>
                    {
                        field.AccessModifier.ToString().ToLower()
                    };

                    if (field.IsStatic)
                        fieldDefinition.Add("static");

                    if (field.IsReadOnly)
                        fieldDefinition.Add("readonly");

                    fieldDefinition.Add(field.Name);
                    fieldDefinition.Add(":");
                    fieldDefinition.Add(field.Type);

                    if (!string.IsNullOrEmpty(field.Value))
                    {
                        fieldDefinition.Add("=");
                        fieldDefinition.Add(field.Value);
                    }

                    Lines.Add(new CodeLine("{0}{1};", Indent(start + 1), string.Join(" ", fieldDefinition)));
                }
            }

            if (ObjectDefinition.Constructors.Count > 0)
            {
                Lines.Add(new CodeLine());

                var constructor = ObjectDefinition.Constructors.First();

                var parameters = constructor.Parameters.Select(item => string.Format("{0} {1}: {2}", item.AccessModifier.ToString().ToLower(), item.Name, item.Type)).ToList();

                Lines.Add(new CodeLine("{0}constructor({1}) {2}", Indent(start + 1), parameters.Count == 0 ? string.Empty : string.Join(", ", parameters), "{"));

                foreach (var line in constructor.Lines)
                    Lines.Add(new CodeLine("{0}{1}", Indent(start + 2), line.ToString()));

                Lines.Add(new CodeLine("{0}{1}", Indent(start + 1), "}"));
            }

            if (ObjectDefinition.Properties.Count > 0)
            {
                Lines.Add(new CodeLine());

                for (var i = 0; i < ObjectDefinition.Properties.Count; i++)
                {
                    var property = ObjectDefinition.Properties[i];

                    if (property.IsAutomatic)
                    {
                        var fieldName = NamingConvention.GetFieldName(property.Name);

                        Lines.Add(new CodeLine("{0}{1} get {2}(): {3} {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{"));

                        Lines.Add(new CodeLine("{0}return this.{1};", Indent(start + 2), fieldName));

                        Lines.Add(new CodeLine("{0}{1}", Indent(start + 1), "}"));

                        Lines.Add(new CodeLine());

                        Lines.Add(new CodeLine("{0}{1} set {2}(value: {3}) {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{"));

                        Lines.Add(new CodeLine("{0}this.{1} = value;", Indent(start + 2), fieldName));

                        Lines.Add(new CodeLine("{0}{1}", Indent(start + 1), "}"));

                        if (i < ObjectDefinition.Properties.Count - 1)
                            Lines.Add(new CodeLine());
                    }
                    else
                    {
                        Lines.Add(new CodeLine("{0}{1} get {2}(): {3} {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{"));

                        foreach (var line in property.GetBody)
                        {
                            var commentCast = line as CommentLine;

                            if (commentCast == null)
                                Lines.Add(new CodeLine("{0}{1}", Indent(start + line.Indent), line.Content));
                            else
                                Lines.Add(new CodeLine("{0}{1}", Indent(start + line.Indent), GetComment(line.Content)));
                        }

                        Lines.Add(new CodeLine("{0}{1}", Indent(start + 1), "}"));

                        Lines.Add(new CodeLine());

                        Lines.Add(new CodeLine("{0}{1} set {2}(value: {3}) {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{"));

                        foreach (var line in property.SetBody)
                        {
                            var commentCast = line as CommentLine;

                            if (commentCast == null)
                                Lines.Add(new CodeLine("{0}{1}", Indent(start + line.Indent), line.Content));
                            else
                                Lines.Add(new CodeLine("{0}{1}", Indent(start + line.Indent), GetComment(line.Content)));
                        }

                        Lines.Add(new CodeLine("{0}{1}", Indent(start + 1), "}"));

                        if (i < ObjectDefinition.Properties.Count - 1)
                            Lines.Add(new CodeLine());
                    }
                }
            }

            if (ObjectDefinition.Methods.Count > 0)
            {
                Lines.Add(new CodeLine());

                for (var i = 0; i < ObjectDefinition.Methods.Count; i++)
                {
                    var method = ObjectDefinition.Methods[i];

                    var parameters = method.Parameters.Select(item => string.Format("{0}: {1}", item.Name, item.Type));

                    Lines.Add(
                        new CodeLine("{0}{1} {2}({3}): {4} {5}", Indent(start + 1), method.AccessModifier.ToString().ToLower(), method.Name, method.Parameters.Count == 0 ? string.Empty : string.Join(", ", method.Parameters.Select(item => string.Format("{0}: {1}", item.Name, item.Type))), method.Type, "{")
                        );

                    foreach (var line in method.Lines)
                    {
                        if (line is CodeLine)
                            Lines.Add(new CodeLine("{0}{1}", Indent(start + 2 + line.Indent), line.Content));
                        else if (line is CommentLine)
                            Lines.Add(new CodeLine("{0}{1}", Indent(start + 2 + line.Indent), GetComment(line.Content)));
                        else if (line is TodoLine)
                            Lines.Add(new CodeLine("{0}{1}", Indent(start + 2 + line.Indent), GetTodo(line.Content)));
                    }

                    Lines.Add(new CodeLine("{0}{1}", Indent(start + 1), "}"));

                    if (i < ObjectDefinition.Methods.Count - 1)
                        Lines.Add(new CodeLine());
                }
            }

            Lines.Add(new CodeLine("{0}{1}", Indent(start), "}"));

            if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
                Lines.Add(new CodeLine("{0}", "}"));
        }
    }
}
