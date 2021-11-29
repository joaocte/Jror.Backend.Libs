namespace Jror.Backend.Libs.Messaging.Abstractions.Interfaces
{
    public interface IMessageFormatter<TypeMessage, TTMessageOut> where TypeMessage : IIntegrationEvent
    {
        TTMessageOut Format(TypeMessage message);
    }
}