using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Jror.Backend.Libs.Extensions.Tests
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public  void Quando_Receber_Uma_Collection_Valida_Adicionar_A_Outra_Collection()
        {
            ICollection<string> palavras1 = new List<string> { "abc", "def", "ghi" };
            ICollection<string> palavras2 = new List<string> { "jkl", "mno", "pqr" };

            palavras1.AddRange(palavras2);

            palavras1
                .Should()
                .NotBeEmpty()
                .And.Contain(palavras2);
        }

        [Fact]
        public  void Quando_Receber_Uma_Collection_Com_Dados_Para_Adicionar_Em_Uma_Collection_Vazia_Adicionar_A_Outra_Collection()
        {
            ICollection<string> palavras1 = new List<string>();
            ICollection<string> palavras2 = new List<string> { "jkl", "mno", "pqr" };

            palavras1.AddRange(palavras2);

            palavras1
                .Should()
                .NotBeEmpty()
                .And.Contain(palavras2);
        }

        [Fact]
        public  void Quando_Receber_Uma_Collection_Nula_Nao_Adicionar_A_Outra_Collection()
        {
            ICollection<string> palavras1 = new List<string>();
            ICollection<string> palavras2 = null;

            palavras1.AddRange(palavras2);

            palavras1
                .Should()
                .BeEmpty().And.HaveCount(0);
        }

        [Fact]
        public  void Quando_Receber_Uma_Collection_Vazia_Nao_Adicionar_A_Outra_Collection()
        {
            ICollection<string> palavras1 = new List<string> { "abc", "def", "ghi" };
            ICollection<string> palavras2 = new List<string>();

            palavras1.AddRange(palavras2);

            palavras1
                .Should()
                .NotBeEmpty().And.HaveCount(3);
        }
    }
}