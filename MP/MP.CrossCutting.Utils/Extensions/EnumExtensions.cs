using MP.CrossCutting.Utils.Model;
using System.ComponentModel;
using System.Reflection;

namespace MP.CrossCutting.Utils.Extensions
{
    public static class EnumExtensions
    {

        public static IEnumerable<EnumDescription> GetEnumDescriptions<TEnum>(this Type _) where TEnum : struct, Enum
        {
            Type? type = typeof(TEnum);
            if (!type.IsEnum) return Array.Empty<EnumDescription>();

            return Enum.GetValues<TEnum>()
                .Select(e => e.GetEnumDescription());
        }

        public static EnumDescription GetEnumDescription(this Enum enumValue)
        {
            Type? enumType = enumValue.GetType();
            Type? enumUnderlyingType = Enum.GetUnderlyingType(enumType);

            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute? attribute = fi?.GetCustomAttribute<DescriptionAttribute>();

            var enumFieldInfo = enumType.GetField(enumValue.ToString());
            var underlyingValue = Convert.ChangeType(enumFieldInfo!.GetValue(enumValue), enumUnderlyingType);

            return new EnumDescription((int?)underlyingValue, enumValue.ToString(), (attribute is null) ? (enumValue?.ToString() ?? default) : attribute?.Description);
        }
    }
}