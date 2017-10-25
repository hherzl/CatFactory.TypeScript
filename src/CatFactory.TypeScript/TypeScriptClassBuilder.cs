using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatFactory.CodeFactory;
using CatFactory.OOP;

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

        public ITypeScriptClassDefinition ObjectDefinition { get; set; }

        public override string FileName
            => ObjectDefinition.Name;

        protected virtual string GetComment(string description)
            => string.Format("//{0}", description);

        protected string GetTodo(string description)
            => string.Format("// todo: {0}", description);

        public override string Code
        {
            get
            {
                var output = new StringBuilder();

                if (ObjectDefinition.Namespaces.Count > 0)
                {
                    foreach (var import in ObjectDefinition.Namespaces)
                    {
                        output.AppendFormat("import {0};", import);
                        output.AppendLine();
                    }

                    output.AppendLine();
                }

                var start = 0;

                if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
                {
                    output.AppendFormat("namespace {0} {1}", ObjectDefinition.Namespace, "{");
                    output.AppendLine();

                    start = 1;
                }

                this.AddAttributes(output, start);
                
                output.AppendFormat("{0}{1}class {2}", Indent(start), ObjectDefinition.Export ? "export " : string.Empty, ObjectDefinition.Name);

                if (ObjectDefinition.HasInheritance)
                {
                    if (!string.IsNullOrEmpty(ObjectDefinition.BaseClass))
                    {
                        output.AppendFormat(" extends {0}", ObjectDefinition.BaseClass);
                    }

                    if (ObjectDefinition.Implements.Count > 0)
                    {
                        output.AppendFormat(" implements {0}", string.Join(", ", ObjectDefinition.Implements));
                    }
                }

                output.AppendFormat(" {0}", "{");
                output.AppendLine();

                if (ObjectDefinition.Properties.Count > 0)
                {
                    foreach (var property in ObjectDefinition.Properties)
                    {
                        if (property.IsAutomatic)
                        {
                            ObjectDefinition.Fields.Add(new FieldDefinition(AccessModifier.Private, property.Type, NamingConvention.GetFieldName(property.Name)));
                        }
                    }
                }

                if (ObjectDefinition.Fields.Count > 0)
                {
                    foreach (var field in ObjectDefinition.Fields)
                    {
                        var fieldDefinition = new List<string>();

                        fieldDefinition.Add(field.AccessModifier.ToString().ToLower());

                        if (field.IsStatic)
                        {
                            fieldDefinition.Add("static");
                        }

                        if (field.IsReadOnly)
                        {
                            fieldDefinition.Add("readonly");
                        }

                        fieldDefinition.Add(string.Format("{0}:", field.Name));
                        fieldDefinition.Add(field.Type);

                        if (!string.IsNullOrEmpty(field.Value))
                        {
                            fieldDefinition.Add("=");
                            fieldDefinition.Add(field.Value);
                        }

                        output.AppendFormat("{0}{1};", Indent(start + 1), string.Join(" ", fieldDefinition));
                        output.AppendLine();
                    }
                }

                if (ObjectDefinition.Constructors.Count > 0)
                {
                    output.AppendLine();

                    foreach (var constructor in ObjectDefinition.Constructors)
                    {
                        var parameters = constructor.Parameters.Select(item => string.Format("{0} {1}: {2}", item.AccessModifier.ToString().ToLower(), item.Name, item.Type)).ToList();

                        output.AppendFormat("{0}constructor({1}) {2}", Indent(start + 1), parameters.Count == 0 ? string.Empty : string.Join(", ", parameters), "{");
                        output.AppendLine();

                        foreach (var line in constructor.Lines)
                        {
                            output.AppendFormat("{0}{1}", Indent(start + 2), line);
                            output.AppendLine();
                        }

                        output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                        output.AppendLine();
                    }
                }

                if (ObjectDefinition.Properties.Count > 0)
                {
                    output.AppendLine();

                    for (var i = 0; i < ObjectDefinition.Properties.Count; i++)
                    {
                        var property = ObjectDefinition.Properties[i];

                        if (property.IsAutomatic)
                        {
                            var fieldName = NamingConvention.GetFieldName(property.Name);

                            output.AppendFormat("{0}{1} get {2}(): {3} {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{");
                            output.AppendLine();

                            output.AppendFormat("{0}return this.{1};", Indent(start + 2), fieldName);
                            output.AppendLine();

                            output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                            output.AppendLine();

                            output.AppendLine();

                            output.AppendFormat("{0}{1} set {2}(value: {3}) {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{");
                            output.AppendLine();

                            output.AppendFormat("{0}this.{1} = value;", Indent(start + 2), fieldName);
                            output.AppendLine();

                            output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                            output.AppendLine();

                            if (i < ObjectDefinition.Properties.Count - 1)
                            {
                                output.AppendLine();
                            }
                        }
                        else
                        {
                            output.AppendFormat("{0}{1} get {2}(): {3} {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{");
                            output.AppendLine();

                            foreach (var line in property.GetBody)
                            {
                                var commentCast = line as CommentLine;

                                if (commentCast == null)
                                {
                                    output.AppendFormat("{0}{1}", Indent(start + line.Indent), line.Content);
                                    output.AppendLine();
                                }
                                else
                                {
                                    output.AppendFormat("{0}{1}", Indent(start + line.Indent), GetComment(line.Content));
                                    output.AppendLine();
                                }
                            }

                            output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                            output.AppendLine();

                            output.AppendLine();

                            output.AppendFormat("{0}{1} set {2}(value: {3}) {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{");
                            output.AppendLine();

                            foreach (var line in property.SetBody)
                            {
                                var commentCast = line as CommentLine;

                                if (commentCast == null)
                                {
                                    output.AppendFormat("{0}{1}", Indent(start + line.Indent), line.Content);
                                    output.AppendLine();
                                }
                                else
                                {
                                    output.AppendFormat("{0}{1}", Indent(start + line.Indent), GetComment(line.Content));
                                    output.AppendLine();
                                }
                            }

                            output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                            output.AppendLine();

                            if (i < ObjectDefinition.Properties.Count - 1)
                            {
                                output.AppendLine();
                            }
                        }
                    }
                }

                if (ObjectDefinition.Methods.Count > 0)
                {
                    output.AppendLine();

                    for (var i = 0; i < ObjectDefinition.Methods.Count; i++)
                    {
                        var method = ObjectDefinition.Methods[i];

                        var parameters = method.Parameters.Select(item => string.Format("{0}: {1}", item.Name, item.Type));

                        output.AppendFormat("{0}{1} {2}({3}): {4} {5}", Indent(start + 1), method.AccessModifier.ToString().ToLower(), method.Name, method.Parameters.Count == 0 ? string.Empty : string.Join(", ", method.Parameters.Select(item => string.Format("{0}: {1}", item.Name, item.Type))), method.Type, "{");
                        output.AppendLine();

                        foreach (var line in method.Lines)
                        {
                            if (line is CodeLine)
                            {
                                output.AppendFormat("{0}{1}", Indent(start + 2 + line.Indent), line.Content);
                                output.AppendLine();
                            }
                            else if (line is CommentLine)
                            {
                                output.AppendFormat("{0}{1}", Indent(start + 2 + line.Indent), GetComment(line.Content));
                                output.AppendLine();
                            }
                            else if (line is TodoLine)
                            {
                                output.AppendFormat("{0}{1}", Indent(start + 2 + line.Indent), GetTodo(line.Content));
                                output.AppendLine();
                            }
                        }

                        output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                        output.AppendLine();

                        if (i < ObjectDefinition.Methods.Count - 1)
                        {
                            output.AppendLine();
                        }
                    }
                }

                output.AppendFormat("{0}{1}", Indent(start), "}");
                output.AppendLine();

                if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
                {
                    output.AppendFormat("{0}", "}");
                    output.AppendLine();
                }

                return output.ToString();
            }
        }
    }
}
