using System.Collections.Generic;
using System.Diagnostics;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassConstructorDefinition : ClassConstructorDefinition
    {
        public TypeScriptClassConstructorDefinition()
            : base()
        {
        }

        public TypeScriptClassConstructorDefinition(params TypeScriptParameterDefinition[] parameters)
            : base()
        {
            Parameters.AddRange(parameters);
        }

        public TypeScriptClassConstructorDefinition(AccessModifier accessModifier, params TypeScriptParameterDefinition[] parameters)
            : base()
        {
            AccessModifier = accessModifier;
            Parameters.AddRange(parameters);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<TypeScriptParameterDefinition> m_parameters;

        public new List<TypeScriptParameterDefinition> Parameters
        {
            get
            {
                return m_parameters ?? (m_parameters = new List<TypeScriptParameterDefinition>());
            }
            set
            {
                m_parameters = value;
            }
        }
    }
}
