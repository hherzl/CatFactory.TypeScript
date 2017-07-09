using System;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptObjectValue : IObjectValue
    {
        public Object Value { get; set; }
    }

    public class TypeScriptArrayValue : IArrayValue
    {
        public Object[] Value { get; set; }
    }
}
