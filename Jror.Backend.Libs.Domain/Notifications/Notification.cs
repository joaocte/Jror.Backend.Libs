using Jror.Backend.Libs.Domain.Abstractions.Notifications;

namespace Jror.Backend.Libs.Domain.Notifications
{
    public class Notification : NotificationAbstract
    {
        public Notification(string key, string message) : base(key, message)
        {
        }
    }
}