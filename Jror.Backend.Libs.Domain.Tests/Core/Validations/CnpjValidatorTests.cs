using FluentValidation;
using FluentValidation.Results;
using Jror.Backend.Libs.Domain.Core.Validations;
using System.Linq;
using Xunit;

namespace Jror.Backend.Libs.Domain.Tests.Core.Validations
{
    public class CnpjValidatorTests
    {
        [Theory]
        [InlineData("05570796000131")]
        [InlineData("37964167000182")]
        [InlineData("70540281000150")]
        [InlineData("69537649000142")]
        [InlineData("13.558.309/0001-43")]
        public void Quando_CNPJ_Eh_Valido_Entao_O_Validador_Deve_Passar(string cnpj)
        {
            TestExtensionsValidator<Empresa> validator = new(x => x.RuleFor(r => r.Cnpj).CnpjValido());
            ValidationResult result = validator.Validate(new Empresa { Cnpj = cnpj });

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("00000000000000")]
        [InlineData("11111111111111")]
        [InlineData("22222222222222")]
        public void Quando_CPF_Eh_Invalido_Com_Mensagem_Customizada_Entao_O_Validador_Deve_Falhar(string cnpj)
        {
            const string customMessage = "Custom Message";

            TestExtensionsValidator<Empresa> validator = new(x => x.RuleFor(r => r.Cnpj).CnpjValido().WithMessage(customMessage));
            ValidationResult result = validator.Validate(new Empresa { Cnpj = cnpj });

            string errorMessage = result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty;

            Assert.False(result.IsValid);
            Assert.Equal(customMessage, errorMessage);
        }

        [Theory]
        [InlineData("13.558.309/0001-42")]
        [InlineData("06.233.205/0001-02")]
        [InlineData("89.475.532/0001-00")]
        [InlineData("56.288.443/0001-84a")]
        public void Quando_O_CPF_Eh_Invalido_O_Validador_Deve_Falhar(string cnpj)
        {
            TestExtensionsValidator<Empresa> validator = new(x => x.RuleFor(r => r.Cnpj).CpfValido());
            ValidationResult result = validator.Validate(new Empresa { Cnpj = cnpj });

            Assert.False(result.IsValid);
            Assert.Equal("O CPF é inválido!", result.Errors.First().ErrorMessage);
        }
    }
}