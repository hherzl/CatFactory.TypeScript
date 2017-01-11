using System;
using System.Collections.Generic;

namespace CatFactory.TypeScript
{
    public class TypeScriptImport
    {
        public TypeScriptImport()
        {
        }

        public TypeScriptImport(IEnumerable<String> types, String from)
        {
            Types = new List<String>(types);
            From = from;
        }

        private List<String> m_types;

        public List<String> Types
        {
            get
            {
                return m_types ?? (m_types = new List<String>());
            }
            set
            {
                m_types = value;
            }
        }

        public String From { get; set; }

        public override String ToString()
        {
            return String.Format("{{ {0}}} from \"{1}\"", String.Join(",", Types), From);
        }
    }
}
