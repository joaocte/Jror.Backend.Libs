using Jror.Backend.Libs.Messaging.Abstractions.Interfaces;
using Newtonsoft.Json;
using System;

namespace Jror.Backend.Libs.Messaging.Abstractions
{
    public abstract class MessageFormatterBase<TypeMessage> : IMessageFormatter<TypeMessage, string> where TypeMessage : IIntegrationEvent
    {
        /// <summary>
        /// Convert the <paramref name="message"/> to json.
        /// </summary>
        /// <param name="message"></param>
        /// <returns><paramref name="message"/> in json format</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        public string Format(TypeMessage message)
        {
            return message == null
                ? throw new ArgumentNullException("The message cannot be null")
                : JsonConvert.SerializeObject(message);
        }
    }
}