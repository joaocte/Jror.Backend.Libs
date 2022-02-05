using Jror.Backend.Libs.Messaging.Abstractions.Tests.Messaging;
using System;
using Xunit;

namespace Jror.Backend.Libs.Messaging.Abstractions.Tests
{
    public class MessageFormatterBaseTest
    {
        [Fact]
        public  void Quando_Criar_Messagem_E_Formatar_Entao_Return_Json_Da_Mensagem()
        {
            var message = new MessageFake
            {
                Code = 1,
                MessageName = "MessageName"
            };
            MessageFakeFormatter formatter = new();

            var formatted = formatter.Format(message);

            Assert.NotNull(formatted);
            Assert.IsType<string>(formatted);
        }

        [Fact]
        public  void Quando_Criar_Uma_Mensagem_Null_Entao_Throw_ArgumentNullException()
        {
            var message = new MessageFake();
            message = null;

            MessageFakeFormatter formatter = new();

            Assert.Throws<ArgumentNullException>(() => formatter.Format(message));
        }
    }
}