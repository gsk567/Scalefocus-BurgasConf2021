using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using BurgasConf.Generator.Attributes;

namespace BurgasConf.Generator.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSourceForGeneration(this Type type) =>
            type.CustomAttributes.Any(x => x.AttributeType == typeof(GeneratorSourceAttribute));

        public static bool IsSourceForGeneration(this MethodInfo methodInfo) =>
            methodInfo.CustomAttributes.Any(x => x.AttributeType == typeof(GeneratorSourceAttribute));

        public static bool IsSourceForGeneration(this PropertyInfo propertyInfo) =>
            propertyInfo.CustomAttributes.Any(x => x.AttributeType == typeof(GeneratorSourceAttribute));

        public static string GetBindableBuildTypeName(this Type type)
        {
            var targetType = type;
            if (type.IsArray)
            {
                targetType = type.GetElementType();
            }
            else if (type.IsCollection())
            {
                targetType = type.GetGenericArguments().FirstOrDefault();
            }

            string classSuffix = targetType.IsClass && targetType != typeof(string) ? "BindableModel" : string.Empty;

            string typeName = $"{targetType.Name}{classSuffix}";

            if (type.IsArray || type.IsCollection())
            {
                typeName = $"ObservableCollection<{typeName}>";
            }

            return typeName;
        }

        public static bool IsCollection(this Type type) => type.GetInterfaces().Any(x => x == typeof(IEnumerable)) && type != typeof(string);
    }
}