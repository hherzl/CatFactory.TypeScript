using System;

namespace CatFactory.TypeScript
{
    public class TypeScriptTypeResolver : ITypeResolver
    {
        public TypeScriptTypeResolver()
        {
        }

        public virtual string Resolve(string type)
        {
            var value = string.Empty;

            switch (type)
            {
                case "char":
                case "varchar":
                case "text":
                case "nchar":
                case "nvarchar":
                case "ntext":
                case "xml":
                    value = "String";
                    break;

                case "money":
                case "decimal":
                case "numeric":
                case "real":
                case "smallint":
                case "int":
                case "bigint":
                    value = "number";
                    break;

                case "tinyint":
                    value = "Byte";
                    break;

                case "image":
                    value = "Byte[]";
                    break;
                    
                case "uniqueidentifier":
                    value = "Guid";
                    break;

                case "bit":
                    value = "bool";
                    break;

                case "datetime":
                case "datetime2":
                    value = "Date";
                    break;

                default:
                    break;
            }

            return value;
        }
    }
}
