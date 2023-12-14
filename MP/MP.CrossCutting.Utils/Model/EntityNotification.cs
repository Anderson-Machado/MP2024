using Flunt.Notifications;

namespace MP.CrossCutting.Utils.Model
{
    public class EntityNotification : Notification
    {
        public string? ErrorCode { get; set; }

        public EntityNotification() : base() { }

        public EntityNotification(string key, string message) : base(key, message) { }

        public EntityNotification(string key, string errorCode, string message) : base(key, message)
        {
            ErrorCode = errorCode;
        }

        public EntityNotification(string key, string messageFormat, params object[] messageArgs)
            : base(key, string.Format(messageFormat, messageArgs)) { }

        public EntityNotification(string key, string errorCode, string messageFormat, params object[] messageArgs)
            : base(key, string.Format(messageFormat, messageArgs))
        {
            ErrorCode = errorCode;
        }
    }
}