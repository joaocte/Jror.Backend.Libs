using Jror.Backend.Libs.Messaging.Abstractions.Interfaces;

namespace Jror.Backend.Libs.Messaging.Abstractions.Tests.Messaging
{
    public class MessageFake : IEvent
    {
        public string MessageName { get; set; }
        public int Code { get; set; }
    }
}