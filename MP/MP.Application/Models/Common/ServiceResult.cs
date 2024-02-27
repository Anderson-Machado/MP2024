using Dawn;
using Flunt.Notifications;

namespace MP.Application.Models.Common
{
    public class ServiceResult<T> : Notifiable<Notification>
            where T : class
    {
        public T? Value { get; protected set; }

        public ServiceResultTypes ResultType { get; protected set; }

        private ServiceResult(ServiceResultTypes type)
        {
            ResultType = type;
        }

        private ServiceResult(T? value)
        {
            ResultType = ServiceResultTypes.Success;
            Value = value;
        }

        public static ServiceResult<T> CreateSuccess(T value)
        {
            Guard.Argument(value, nameof(value)).NotNull();

            return new ServiceResult<T>(value);
        }

        public static ServiceResult<T> CreateNotFound()
        {
            return new ServiceResult<T>(ServiceResultTypes.NotFound);
        }

        public static ServiceResult<T> CreateNoContent()
        {
            return new ServiceResult<T>(ServiceResultTypes.NoContent);
        }

        public static ServiceResult<T> CreateUnprocessable()
        {
            return new ServiceResult<T>(ServiceResultTypes.Unprocessable);
        }

        public static ServiceResult<T> CreateWithErrors(IReadOnlyCollection<Notification> notifications)
        {
            Guard.Argument(notifications, nameof(notifications)).NotNull();
            var obj = new ServiceResult<T>(ServiceResultTypes.Error);
            obj.AddNotifications(notifications);
            return obj;
        }
        public static ServiceResult<T> CreateWithError(Notification notification)
        {
            Guard.Argument(notification, nameof(notification)).NotNull();
            var obj = new ServiceResult<T>(ServiceResultTypes.Error);
            obj.AddNotification(notification);
            return obj;
        }

        public static ServiceResult<T> CreateWithError(string property, string message)
        {
            Guard.Argument(property, nameof(property)).NotNull().NotEmpty();
            Guard.Argument(message, nameof(message)).NotNull().NotEmpty();

            var obj = new ServiceResult<T>(ServiceResultTypes.Error);
            obj.AddNotification(property, message);
            return obj;
        }
    }

    public enum ServiceResultTypes
    {
        Success,
        Error,
        NotFound,
        Unprocessable,
        NoContent
    }
}