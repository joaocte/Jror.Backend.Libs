namespace Jror.Backend.Libs.Domain.Core.Validations
{
    /// <summary>
    /// Class that validates the CPF
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class CPFValidator<T, TProperty> : DocumentoGenericoValidator<T, TProperty>
    {
        /// <inheritdoc/>
        internal CPFValidator(int validLength, string errorMessage)
            : base(validLength, errorMessage)
        { }

        /// <inheritdoc/>
        public CPFValidator(string errorMessage)
            : this(11, errorMessage)
        { }

        /// <inheritdoc/>
        public CPFValidator()
            : this("O CPF é inválido!")
        { }

        protected override int[] FirstMultiplierCollection => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        protected override int[] SecondMultiplierCollection => new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        public override string Name => "CPFValidator";
    }
}