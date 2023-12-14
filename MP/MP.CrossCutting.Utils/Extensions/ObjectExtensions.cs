namespace MP.CrossCutting.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static object? SetValueIfPropertyExists(this object? obj, string? propertyName, object? value)
        {
            if (obj is not null && !string.IsNullOrEmpty(propertyName))
            {
                var propertyInfo = obj.GetType().GetProperty(propertyName);
                propertyInfo?.SetValue(obj, value);
            }

            return obj;
        }
    }
}