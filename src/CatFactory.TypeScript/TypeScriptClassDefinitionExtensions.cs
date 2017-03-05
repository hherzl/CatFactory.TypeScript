using System;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public static class TypeScriptClassDefinitionExtensions
    {
        public static void AddConstant(this TypeScriptClassDefinition classDefinition, String type, String name, String value)
        {
            classDefinition.Fields.Add(new FieldDefinition(type, name) { IsStatic = true, IsReadOnly = true, Value = value });
        }
    }
}
