namespace Jror.Backend.Libs.Domain.Abstractions.Notifications
{
    public abstract class NotificationAbstract
    {
        public string Key { get; }
        public string Message { get; }

        public NotificationAbstract(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}