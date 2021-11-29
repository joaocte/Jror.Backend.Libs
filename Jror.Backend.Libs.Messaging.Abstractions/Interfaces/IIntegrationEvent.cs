using System;

namespace Jror.Backend.Libs.Messaging.Abstractions.Interfaces
{
    public abstract class IIntegrationEvent
    {
        protected Guid Id { get; }
        protected DateTime PublicatedDate { get; }
        protected string PublicatedTime { get; }

        protected IIntegrationEvent()
        {
            var UtcDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
            PublicatedDate = UtcDate.Date;
            PublicatedTime = UtcDate.ToString("T");
        }
    }
}