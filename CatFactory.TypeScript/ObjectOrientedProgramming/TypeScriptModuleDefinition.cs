using System.Collections.Generic;
using System.Diagnostics;
using CatFactory.OOP;

namespace CatFactory.TypeScript.ObjectOrientedProgramming
{
    public class TypeScriptModuleDefinition
    {
        public TypeScriptModuleDefinition()
        {
        }

        public string Name { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<string> m_namespaces;

        public List<string> Namespaces
        {
            get
            {
                return m_namespaces ?? (m_namespaces = new List<string>());
            }
            set
            {
                m_namespaces = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<ConstantDefinition> m_constants;

        public List<ConstantDefinition> Constants
        {
            get
            {
                return m_constants ?? (m_constants = new List<ConstantDefinition>());
            }
            set
            {
                m_constants = value;
            }
        }
    }
}
