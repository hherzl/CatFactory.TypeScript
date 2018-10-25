using CatFactory.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.ObjectOrientedProgramming
{
    public class TypeScriptParameterDefinition : ParameterDefinition
    {
        public TypeScriptParameterDefinition()
            : base()
        {
        }

        public TypeScriptParameterDefinition(string type, string name, params MetadataAttribute[] attributes)
            : base(type, name, attributes)
        {
        }

        public TypeScriptParameterDefinition(AccessModifier accessModifier, string type, string name, params MetadataAttribute[] attributes)
            : base(type, name, attributes)
        {
            AccessModifier = accessModifier;
        }

        public TypeScriptParameterDefinition(string type, string name, string defaultValue, params MetadataAttribute[] attributes)
            : base(type, name, defaultValue, attributes)
        {
        }

        public TypeScriptParameterDefinition(AccessModifier accessModifier, string type, string name, string defaultValue, params MetadataAttribute[] attributes)
            : base(type, name, defaultValue, attributes)
        {
            AccessModifier = accessModifier;
        }

        public AccessModifier AccessModifier { get; set; }
    }
}
