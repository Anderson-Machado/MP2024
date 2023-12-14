using AutoMapper;

namespace MP.Application.Mappings.Resolvers
{
    public class StringToNullableGuidConverter : IValueConverter<string?, Guid?>
    {
        public Guid? Convert(string? sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember) && Guid.TryParse(sourceMember, out Guid guidValue))
            {
                return guidValue;
            }
            return default;
        }
    }
}