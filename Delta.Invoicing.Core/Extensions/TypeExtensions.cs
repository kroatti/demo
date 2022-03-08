namespace Delta.Invoicing.Core.Extensions
{
    public static class TypeExtensions
    {
        public static string GetNameConcat(this Type type)
        {
            string name = type.Name;

            while (type.DeclaringType != null)
            {
                name = type.DeclaringType.Name + name;
                type = type.DeclaringType;
            }

            return name;
        }
    }
}
