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

                case "String":
                    value = "string";
                    break;

                case "money":
                case "decimal":
                case "numeric":
                case "real":
                case "smallint":
                case "int":
                case "bigint":

                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Decimal":
                case "Double":
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
                case "Boolean":
                    value = "bool";
                    break;

                case "datetime":
                case "datetime2":
                case "DateTime":
                    value = "Date";
                    break;

                default:
                    value = "any";
                    break;
            }

            return value;
        }
    }
}
