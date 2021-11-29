using FluentValidation;

namespace Jror.Backend.Libs.Domain.Core.Validations
{
    public static partial class DocumentoExtensionsValidation
    {
        /// <summary>
        /// Rule to validate CPF.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> CpfValido<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CPFValidator<T, string>());
        }

        /// <summary>
        /// Rule to validate CNPJ.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> CnpjValido<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CNPJValidator<T, string>());
        }
    }
}