using NJsonSchema.Generation;

namespace FibaApi
{
    internal class CustomSwaggerSchemaNameGenerator : DefaultSchemaNameGenerator
    {
        public override string Generate(Type type)
        {
            var declaringTypes = new List<Type>();
            GetAllDeclaringTypes(declaringTypes, type);

            return declaringTypes.Count > 0
                ? $"{String.Join("", declaringTypes.Select(t => t.Name))}{type.Name}"
                : base.Generate(type);
        }

        private static void GetAllDeclaringTypes(IList<Type> declaringTypes, Type type)
        {
            while (true)
            {
                if (type.DeclaringType != null)
                {
                    declaringTypes.Insert(0, type.DeclaringType);
                    type = type.DeclaringType;
                    continue;
                }

                break;
            }
        }
    }
}
