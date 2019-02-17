using CatFactory.CodeFactory;

namespace CatFactory.TypeScript.CodeFactory
{
    public class TypeScriptObjectValue : IObjectValue
    {
        public TypeScriptObjectValue()
        {
        }

        public TypeScriptObjectValue(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}
