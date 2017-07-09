using System;
using System.Collections.Generic;
using CatFactory.OOP;

namespace CatFactory.TypeScript
{
    public class TypeScriptModuleDefinition
    {
        private List<ConstantDefinition> m_constants;


        public TypeScriptModuleDefinition()
        {
        }

        public string Name { get; set; }

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
