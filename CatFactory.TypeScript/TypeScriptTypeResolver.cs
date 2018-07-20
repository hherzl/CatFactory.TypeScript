namespace CatFactory.TypeScript
{
    public static class TypeScriptTypeResolver
    {
        public static string Resolve(string type)
        {
            var value = string.Empty;

            switch (type)
            {
                case "string":
                case "String":
                    value = "string";
                    break;

                case "short":
                case "int":
                case "long":
                case "decimal":
                case "double":
                case "float":
                case "Int16":
                case "Int32":
                case "Int64":
                case "Decimal":
                case "Double":
                case "Single":
                    value = "number";
                    break;

                case "bool":
                case "Boolean":
                    value = "bool";
                    break;

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
