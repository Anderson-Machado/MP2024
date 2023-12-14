using Flunt.Validations;
using MP.CrossCutting.Utils.Resources;

namespace MP.CrossCutting.Utils.Extensions
{
    public static class FluntEluxExtensions
    {
        #region Enum Methods

        public static Contract<T> IsNullOrEnumText<T>(this Contract<T> contract, string? value, Type enumType, string? key)
        {
            return contract.IsNullOrEnumText<T>(value, enumType, key, Messages.MessageFailEnumConvert, value, enumType.Name);
        }

        public static Contract<T> IsNullOrEnumText<T>(this Contract<T> contract, string? value, Type enumType, string? key, string Message)
        {
            return contract.IsNullOrEnumText<T>(value, enumType, key, MessageFormat: Message);
        }

        public static Contract<T> IsNullOrEnumText<T>(this Contract<T> contract, string? value, Type enumType, string? key, string MessageFormat, params object?[] args)
        {
            if (!enumType.IsEnum) throw new ArgumentException("enumType must be an enumerated type");
            if (!string.IsNullOrWhiteSpace(value) && !Enum.TryParse(enumType, value.ToUpper(), out object? _))
            {
                contract.AddNotification(key, string.Format(MessageFormat, args));
            }

            return contract;
        }

        public static Contract<T> IsEnumText<T>(this Contract<T> contract, string? value, Type enumType, string? key)
        {
            return contract.IsEnumText<T>(value, enumType, key, Messages.MessageFailEnumConvert, value, enumType.Name);
        }

        public static Contract<T> IsEnumText<T>(this Contract<T> contract, string? value, Type enumType, string? key, string Message)
        {
            return contract.IsEnumText<T>(value, enumType, key, MessageFormat: Message);
        }

        public static Contract<T> IsEnumText<T>(this Contract<T> contract, string? value, Type enumType, string? key, string MessageFormat, params object?[] args)
        {
            if (!enumType.IsEnum) throw new ArgumentException("enumType must be an enumerated type");

            if (string.IsNullOrWhiteSpace(value) || !Enum.TryParse(enumType, value.ToUpper(), out object? _))
            {
                contract.AddNotification(key, string.Format(MessageFormat, args));
            }

            return contract;
        }

        #endregion
    }
}