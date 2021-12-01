using System.ComponentModel.DataAnnotations;

namespace Jror.Backend.Libs.Domain.Tests
{
    public class Pessoa
    {
        [Key]
        public string Cpf { get; set; }

        public string Nome { get; set; }
    }
}