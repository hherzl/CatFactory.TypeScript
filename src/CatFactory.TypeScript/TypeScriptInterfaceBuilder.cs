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
            ObjectDefinition = new TypeScriptInterfaceDefinition();
        }

        public ITypeScriptInterfaceDefinition ObjectDefinition { get; set; }

        public override String FileName
        {
            get
            {
                return ObjectDefinition.Name;
            }
        }

        public override String Code
        {
            get
            {
                var output = new StringBuilder();

                var start = 0;

                if (!String.IsNullOrEmpty(ObjectDefinition.Namespace))
                {
                    output.AppendFormat("namespace {0} {1}", ObjectDefinition.Namespace, "{");
                    output.AppendLine();

                    start = 1;
                }

                output.AppendFormat("{0}{1}interface {2}", Indent(start), ObjectDefinition.AccessModifier == AccessModifier.Public ? "export " : String.Empty, ObjectDefinition.Name);

                if (ObjectDefinition.HasInheritance)
                {
                    if (ObjectDefinition.Implements.Count > 0)
                    {
                        output.AppendFormat(" implements {0}", String.Join(", ", ObjectDefinition.Implements));
                    }
                }

                output.AppendFormat(" {0}", "{");
                output.AppendLine();

                if (ObjectDefinition.Fields.Count > 0)
                {
                    foreach (var field in ObjectDefinition.Fields)
                    {
                        output.AppendFormat("{0}{1}: {2};", Indent(start + 1), field.Name, field.Type);
                        output.AppendLine();
                    }
                }

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
                        output.AppendFormat("{0}{1}({2}): {3};", Indent(start + 1), method.Name, method.Parameters.Count == 0 ? String.Empty : String.Join(", ", method.Parameters.Select(item => String.Format("{0}: {1}", item.Name, item.Type))), method.Type);
                        output.AppendLine();
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
