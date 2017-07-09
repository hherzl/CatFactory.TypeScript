using System;
using System.Linq;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public static class TypeScriptClassDefinitionExtensions
    {
        public static void AddConstant(this TypeScriptClassDefinition classDefinition, string type, string name, string value)
        {
            classDefinition.Fields.Add(new FieldDefinition(type, name) { IsStatic = true, IsReadOnly = true, Value = value });
        }

        public static TypeScriptInterfaceDefinition RefactInterface(this TypeScriptClassDefinition classDefinition, params string[] exclusions)
        {
            var interfaceDefinition = new TypeScriptInterfaceDefinition();
            var namingConvention = new TypeScriptNamingConvention();

            interfaceDefinition.Name = namingConvention.GetInterfaceName(classDefinition.Name);

            interfaceDefinition.Namespaces = classDefinition.Namespaces;

            foreach (var @event in classDefinition.Events.Where(item => item.AccessModifier == AccessModifier.Public && !exclusions.Contains(item.Name)))
            {
                interfaceDefinition.Events.Add(new EventDefinition(@event.Type, @event.Name));
            }

            foreach (var property in classDefinition.Properties.Where(item => item.AccessModifier == AccessModifier.Public && !exclusions.Contains(item.Name)))
            {
                interfaceDefinition.Properties.Add(new PropertyDefinition(property.Type, property.Name)
                {
                    IsAutomatic = property.IsAutomatic,
                    IsReadOnly = property.IsReadOnly
                });
            }

            foreach (var method in classDefinition.Methods.Where(item => item.AccessModifier == AccessModifier.Public && !exclusions.Contains(item.Name)))
            {
                interfaceDefinition.Methods.Add(new MethodDefinition(method.Type, method.Name, method.Parameters.ToArray()));
            }

            return interfaceDefinition;
        }
    }
}
