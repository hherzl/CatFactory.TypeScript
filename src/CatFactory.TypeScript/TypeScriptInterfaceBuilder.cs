using System;
using System.Linq;
using System.Text;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptInterfaceBuilder : TypeScriptCodeBuilder
    {
        public TypeScriptInterfaceBuilder()
        {
        }

        public ITypeScriptInterfaceDefinition ObjectDefinition { get; set; } = new TypeScriptInterfaceDefinition();

        public override string FileName
            => ObjectDefinition.Name;

        public override string Code
        {
            get
            {
                var output = new StringBuilder();

                var start = 0;

                if (!string.IsNullOrEmpty(ObjectDefinition.Namespace))
                {
                    output.AppendFormat("namespace {0} {1}", ObjectDefinition.Namespace, "{");
                    output.AppendLine();

                    start = 1;
                }

                foreach (var attribute in ObjectDefinition.Attributes)
                {
                    output.AppendFormat("{0}@{1}(", Indent(start), attribute.Name);

                    if (attribute.Sets.Count > 0)
                    {
                        output.Append("{");

                        output.AppendLine();

                        for (var i = 0; i < attribute.Sets.Count; i++)
                        {
                            output.AppendFormat("{0}{1}", Indent(start + 1), attribute.Sets[i]);

                            if (i < attribute.Sets.Count - 1)
                            {
                                output.Append(",");
                            }

                            output.AppendLine();
                        }

                        output.Append("}");
                    }

                    output.AppendFormat(")");
                    output.AppendLine();
                }

                output.AppendFormat("{0}{1}interface {2}", Indent(start), ObjectDefinition.AccessModifier == AccessModifier.Public ? "export " : string.Empty, ObjectDefinition.Name);

                if (ObjectDefinition.HasInheritance)
                {
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
                        output.AppendFormat("{0}{1}: {2};", Indent(start + 1), property.Name, property.Type);
                        output.AppendLine();
                    }
                }

                if (ObjectDefinition.Methods.Count > 0)
                {
                    foreach (var method in ObjectDefinition.Methods)
                    {
                        var parameters = string.Join(", ", method.Parameters.Select(item => string.Format("{0}: {1}", item.Name, item.Type)));

                        output.AppendFormat("{0}{1}({2}): {3};", Indent(start + 1), method.Name, method.Parameters.Count == 0 ? string.Empty : parameters, method.Type);
                        output.AppendLine();
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
