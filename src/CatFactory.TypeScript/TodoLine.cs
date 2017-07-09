using System;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TodoLine : Line
    {
        public TodoLine()
            : base()
        {
        }

        public TodoLine(Int32 indent, string content, params string[] values)
            : base(indent, content, values)
        {
        }

        public TodoLine(string content, params string[] values)
            : base(content, values)
        {
        }

        public override string ToString()
            => Content;
    }
}
