namespace MP.CrossCutting.Utils.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid? id) => !id.HasValue || id.Value == Guid.Empty;
    }
}