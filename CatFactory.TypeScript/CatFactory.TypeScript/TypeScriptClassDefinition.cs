using System.Collections.Generic;
using System.Diagnostics;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptClassDefinition : ClassDefinition, ITypeScriptClassDefinition
    {
        public TypeScriptClassDefinition()
        {
        }

        public bool Export { get; set; } = true;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<TypeScriptClassConstructorDefinition> m_constructors;

        public new List<TypeScriptClassConstructorDefinition> Constructors
        {
            get
            {
                return m_constructors ?? (m_constructors = new List<TypeScriptClassConstructorDefinition>());
            }
            set
            {
                m_constructors = value;
            }
        }
    }
}
