using System.Collections.Generic;
using System.Diagnostics;
using CatFactory.ObjectOrientedProgramming;

namespace CatFactory.TypeScript.ObjectOrientedProgramming
{
    public class TypeScriptModuleDefinition : ObjectDefinition
    {
        public TypeScriptModuleDefinition()
            : base()
        {
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
