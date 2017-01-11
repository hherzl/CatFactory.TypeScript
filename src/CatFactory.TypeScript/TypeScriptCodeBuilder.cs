using System;
using CatFactory.CodeFactory;

namespace CatFactory.TypeScript
{
    public class TypeScriptCodeBuilder : CodeBuilder
    {
        public override String FileExtension
        {
            get
            {
                return "ts";
            }
        }
    }
}
