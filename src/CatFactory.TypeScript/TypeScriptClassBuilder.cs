using System;
using System.Linq;
using System.Text;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassBuilder : TypeScriptCodeBuilder
    {
        public TypeScriptClassBuilder()
        {
        }

        public ITypeScriptClassDefinition ObjectDefinition { get; set; } = new TypeScriptClassDefinition();

        public override String FileName
            => ObjectDefinition.Name;

        public override String Code
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

                if (!String.IsNullOrEmpty(ObjectDefinition.Namespace))
                {
                    output.AppendFormat("namespace {0} {1}", ObjectDefinition.Namespace, "{");
                    output.AppendLine();

                    start = 1;
                }

                this.AddAttributes(output, start);

                output.AppendFormat("{0}{1}class {2}", Indent(start), ObjectDefinition.AccessModifier == AccessModifier.Public ? "export " : String.Empty, ObjectDefinition.Name);

                if (ObjectDefinition.HasInheritance)
                {
                    if (!String.IsNullOrEmpty(ObjectDefinition.BaseClass))
                    {
                        output.AppendFormat(" extends {0}", ObjectDefinition.BaseClass);
                    }

                    if (ObjectDefinition.Implements.Count > 0)
                    {
                        output.AppendFormat(" implements {0}", String.Join(", ", ObjectDefinition.Implements));
                    }
                }

                output.AppendFormat(" {0}", "{");
                output.AppendLine();

                var namingConvention = new TypeScriptNamingConvention();

                if (ObjectDefinition.Properties.Count > 0)
                {
                    foreach (var property in ObjectDefinition.Properties)
                    {
                        if (property.IsAutomatic)
                        {
                            ObjectDefinition.Fields.Add(new FieldDefinition(AccessModifier.Private, property.Type, namingConvention.GetFieldName(property.Name)));
                        }
                    }
                }

                if (ObjectDefinition.Fields.Count > 0)
                {
                    foreach (var field in ObjectDefinition.Fields)
                    {
                        output.AppendFormat("{0}{1} {2}: {3};", Indent(start + 1), field.AccessModifier.ToString().ToLower(), field.Name, field.Type);
                        output.AppendLine();
                    }
                }

                if (ObjectDefinition.Constructors.Count > 0)
                {
                    output.AppendLine();

                    foreach (var constructor in ObjectDefinition.Constructors)
                    {
                        var parameters = constructor.Parameters.Select(item => String.Format("{0} {1}: {2}", item.AccessModifier.ToString().ToLower(), item.Name, item.Type)).ToList();

                        output.AppendFormat("{0}constructor({1}) {2}", Indent(start + 1), parameters.Count == 0 ? String.Empty : String.Join(", ", parameters), "{");
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
                            var fieldName = namingConvention.GetFieldName(property.Name);

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
                                output.AppendFormat("{0}{1};", Indent(start + line.Indent), line.Content);
                                output.AppendLine();
                            }

                            output.AppendFormat("{0}{1}", Indent(start + 1), "}");
                            output.AppendLine();

                            output.AppendLine();

                            output.AppendFormat("{0}{1} set {2}(value: {3}) {4}", Indent(start + 1), property.AccessModifier.ToString().ToLower(), property.Name, property.Type, "{");
                            output.AppendLine();

                            foreach (var line in property.SetBody)
                            {
                                output.AppendFormat("{0}{1};", Indent(start + line.Indent), line.Content);
                                output.AppendLine();
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

                        var parameters = method.Parameters.Select(item => String.Format("{0}: {1}", item.Name, item.Type));

                        output.AppendFormat("{0}{1} {2}({3}): {4} {5}", Indent(start + 1), method.AccessModifier.ToString().ToLower(), method.Name, method.Parameters.Count == 0 ? String.Empty : String.Join(", ", method.Parameters.Select(item => String.Format("{0}: {1}", item.Name, item.Type))), method.Type, "{");
                        output.AppendLine();

                        foreach (var line in method.Lines)
                        {
                            output.AppendFormat("{0}{1}", Indent(start + 2 + line.Indent), line);
                            output.AppendLine();
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

                if (!String.IsNullOrEmpty(ObjectDefinition.Namespace))
                {
                    output.AppendFormat("{0}", "}");
                    output.AppendLine();
                }

                return output.ToString();
            }
        }
    }
}
