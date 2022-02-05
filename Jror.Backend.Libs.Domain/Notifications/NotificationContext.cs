using FluentValidation.Results;
using Jror.Backend.Libs.Domain.Abstractions.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace Jror.Backend.Libs.Domain.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<NotificationAbstract> notifications;

        public IReadOnlyCollection<NotificationAbstract> GetNotifications() => notifications;

        public bool HasNotifications() => notifications.Any();

        public NotificationContext()
        {
            notifications = new List<NotificationAbstract>();
        }

        public virtual void AddNotification(string key, string message)
        {
            notifications.Add(new Notification(key, message));
        }

        public virtual void AddNotification(NotificationAbstract notification)
        {
            notifications.Add(notification);
        }

        public virtual void AddNotifications(IReadOnlyCollection<NotificationAbstract> notifications)
        {
            this.notifications.AddRange(notifications);
        }

        public virtual void AddNotifications(IList<NotificationAbstract> notifications)
        {
            this.notifications.AddRange(notifications);
        }

        public virtual void AddNotifications(ICollection<NotificationAbstract> notifications)
        {
            this.notifications.AddRange(notifications);
        }

        public virtual void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorCode, error.ErrorMessage);
            }
        }
    }
}