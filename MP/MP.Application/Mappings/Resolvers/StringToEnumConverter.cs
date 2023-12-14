using AutoMapper;

namespace MP.Application.Mappings.Resolvers
{
    public class StringToEnumConverter<TEnum> : IValueConverter<string?, TEnum?> where TEnum : struct, Enum
    {
        public TEnum? Convert(string? sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember) && Enum.TryParse(typeof(TEnum), sourceMember.ToUpper(), out object? enumValue))
            {
                return (TEnum?)enumValue;
            }
            return default;
        }
    }
}