using FluentValidation.Results;
using System.Collections.Generic;

namespace Jror.Backend.Libs.Domain.Abstractions.Notifications
{
    public interface INotificationContext
    {
        void AddNotification(string key, string message);

        void AddNotification(NotificationAbstract notification);

        void AddNotifications(IReadOnlyCollection<NotificationAbstract> notifications);

        void AddNotifications(IList<NotificationAbstract> notifications);

        void AddNotifications(ICollection<NotificationAbstract> notifications);

        void AddNotifications(ValidationResult validationResult);

        bool HasNotifications();

        IReadOnlyCollection<NotificationAbstract> GetNotifications();
    }
}