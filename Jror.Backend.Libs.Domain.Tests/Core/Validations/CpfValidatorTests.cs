using FluentValidation;
using FluentValidation.Results;
using Jror.Backend.Libs.Domain.Core.Validations;
using System.Linq;
using Xunit;

namespace Jror.Backend.Libs.Domain.Tests.Core.Validations
{
    public class CPFValidatorTests
    {
        [Theory]
        [InlineData("40931834066")]
        [InlineData("97331405039")]
        [InlineData("94509906030")]
        [InlineData("51392442095")]
        [InlineData("331.738.730-09")]
        public  void Quando_CPF_Eh_Valido_Entao_O_Validador_Deve_Passar(string cpf)
        {
            TestExtensionsValidator<Pessoa> validator = new(x => x.RuleFor(r => r.Cpf).CpfValido());
            ValidationResult result = validator.Validate(new Pessoa { Cpf = cpf });

            Assert.True(result.IsValid);
        }

        [Fact]
        public  void Quando_CPF_Eh_Invalido_Com_Mensagem_Customizada_Entao_O_Validador_Deve_Falhar()
        {
            const string customMessage = "Custom Message";

            TestExtensionsValidator<Pessoa> validator = new(x => x.RuleFor(r => r.Cpf).CpfValido().WithMessage(customMessage));
            ValidationResult result = validator.Validate(new Pessoa { Cpf = "000.000.000-00" });

            string errorMessage = result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty;

            Assert.False(result.IsValid);
            Assert.Equal(customMessage, errorMessage);
        }

        [Theory]
        [InlineData("144.442.344-57")]
        [InlineData("543.434.321-76")]
        [InlineData("A822.420.106-62")]
        [InlineData("822.420.106-62a")]
        public  void Quando_O_CPF_Eh_Invalido_O_Validador_Deve_Falhar(string cpf)
        {
            TestExtensionsValidator<Pessoa> validator = new(x => x.RuleFor(r => r.Cpf).CpfValido());
            ValidationResult result = validator.Validate(new Pessoa { Cpf = cpf });

            Assert.False(result.IsValid);
            Assert.Equal("O CPF é inválido!", result.Errors.First().ErrorMessage);
        }
    }
}