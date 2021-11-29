namespace Jror.Backend.Libs.Domain.Core.Validations
{
    /// <summary>
    /// Class that validates the CNPJ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class CNPJValidator<T, TProperty> : DocumentoGenericoValidator<T, TProperty>
    {
        /// <inheritdoc/>
        internal CNPJValidator(int validLength, string errorMessage)
            : base(validLength, errorMessage)
        { }

        /// <inheritdoc/>
        public CNPJValidator(string errorMessage)
            : this(14, errorMessage)
        { }

        /// <inheritdoc/>
        public CNPJValidator()
            : this("O CNPJ é inválido!")
        { }

        protected override int[] FirstMultiplierCollection => new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        protected override int[] SecondMultiplierCollection => new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public override string Name => "CNPJValidator";
    }
}