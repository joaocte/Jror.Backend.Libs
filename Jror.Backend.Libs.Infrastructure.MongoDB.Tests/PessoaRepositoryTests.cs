using Jror.Backend.Libs.Domain.Tests;
using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jror.Backend.Libs.Infrastructure.MongoDB.Tests.TestObjects;
using MongoDB.Driver;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Jr.Backend.Libs.Tests.Infrastructure
{
    public class PessoaRepositoryTests : InfrastructureTestBase
    {
        [Fact]
        public virtual void Quando_Receber_Um_Objeto_Valido_Entao_Adicionar_Este_Objeto()
        {
            var mockContext = Substitute.For<IMongoContext>();
            var mockCollection = CreateMockCollection<Pessoa>();
            var pessoa = new Pessoa
            {
                Cpf = "15235133064"
            };

            mockCollection.InsertOne(pessoa);

            mockContext.GetCollection<Pessoa>("Pessoa").Returns(mockCollection);
            var baixarArquivoRepository = new PessoaRepository(mockContext, "Pessoa");
            baixarArquivoRepository.AddAsync(pessoa).Wait();
            mockCollection.Received(1).InsertOne(pessoa);
        }

        [Fact]
        public virtual void Quando_Receber_Um_Id_Valido_Retornar_Objeto_Encontrado()
        {
            var mockContext = Substitute.For<IMongoContext>();
            var mockCollection = CreateMockCollection<Pessoa>();
            var pessoa = new Pessoa
            {
                Cpf = "15235133064"
            };
            mockCollection.InsertOne(pessoa);

            mockContext.GetCollection<Pessoa>("Pessoa").Returns(mockCollection);
            var baixarArquivoRepository = new PessoaRepository(mockContext, "Pessoa");
            var arquivos = new List<Pessoa>();
            arquivos.Add(pessoa);
            var asyncCursor = Substitute.For<IAsyncCursor<Pessoa>>();
            asyncCursor.Current.Returns(arquivos);
            asyncCursor.MoveNext(Arg.Any<CancellationToken>()).Returns(true, false);
            asyncCursor.MoveNextAsync(Arg.Any<CancellationToken>()).Returns(true, false);
            mockCollection.FindAsync(Arg.Any<FilterDefinition<Pessoa>>(),
                Arg.Any<FindOptions<Pessoa>>(),
                Arg.Any<CancellationToken>()).Returns(asyncCursor);
            var retorno = baixarArquivoRepository.GetByIdAsync(pessoa.Cpf).Result;

            Assert.Equal(pessoa.Cpf, retorno.Cpf);
        }

        [Fact]
        public virtual void Quanto_Consultar_Todos_Objetos_Entao_Retornar_Objeto_Encontrado()
        {
            var mockContext = Substitute.For<IMongoContext>();
            var mockCollection = CreateMockCollection<Pessoa>();
            var pessoa = new Pessoa
            {
                Cpf = "15235133064"
            };
            var pessoa2 = new Pessoa
            {
                Cpf = "23884464060"
            };
            mockCollection.InsertOne(pessoa);
            mockCollection.InsertOne(pessoa2);

            mockContext.GetCollection<Pessoa>("Pessoa").Returns(mockCollection);
            var baixarArquivoRepository = new PessoaRepository(mockContext, "Pessoa");
            var pessoas = new List<Pessoa>();
            pessoas.Add(pessoa);
            pessoas.Add(pessoa2);
            var asyncCursor = Substitute.For<IAsyncCursor<Pessoa>>();
            asyncCursor.Current.Returns(pessoas);
            asyncCursor.MoveNext(Arg.Any<CancellationToken>()).Returns(true, false);
            asyncCursor.MoveNextAsync(Arg.Any<CancellationToken>()).Returns(true, false);
            mockCollection.FindAsync(Arg.Any<FilterDefinition<Pessoa>>(),
                Arg.Any<FindOptions<Pessoa>>(),
                Arg.Any<CancellationToken>()).Returns(asyncCursor);
            var retorno = baixarArquivoRepository.GetAllAsync().Result;

            var collection = retorno.ToArray();
            Assert.Contains(pessoa, collection);
            Assert.Contains(pessoa2, collection);
        }

        [Fact]
        public virtual void Quando_Receber_Objeto_Valido_Para_Atualizar_Entao_Atualizar_Objeto()
        {
            var mockContext = Substitute.For<IMongoContext>();
            var mockCollection = CreateMockCollection<Pessoa>();
            var pessoa = new Pessoa
            {
                Cpf = "15235133064"
            };
            mockCollection.InsertOne(pessoa);

            mockContext.GetCollection<Pessoa>("Pessoa").Returns(mockCollection);
            var baixarArquivoRepository = new PessoaRepository(mockContext, "Pessoa");
            var arquivoAtualizado = pessoa;

            arquivoAtualizado.Cpf = "05283068048";
            baixarArquivoRepository.UpdateAsync(arquivoAtualizado).Wait();

            mockCollection.DidNotReceive().ReplaceOneAsync(Arg.Any<FilterDefinition<Pessoa>>(), arquivoAtualizado);
        }

        [Fact]
        public virtual void Quando_Receber_Objeto_Valido_Entao_Remover()
        {
            var mockContext = Substitute.For<IMongoContext>();
            var mockCollection = CreateMockCollection<Pessoa>();
            var pessoa = new Pessoa
            {
                Cpf = "15235133064"
            };
            mockCollection.InsertOne(pessoa);

            mockContext.GetCollection<Pessoa>("Pessoa").Returns(mockCollection);
            var baixarArquivoRepository = new PessoaRepository(mockContext, "Pessoa");

            baixarArquivoRepository.RemoveAsync(pessoa.Cpf).Wait();

            mockCollection.DidNotReceive().DeleteOneAsync(Arg.Any<FilterDefinition<Pessoa>>());
        }

        [Fact]
        public virtual void Quando_Terminar_Operacao_Deve_Realizar_Dispose()
        {
            var mockContext = Substitute.For<IMongoContext>();
            var mockCollection = CreateMockCollection<Pessoa>();
            var pessoa = new Pessoa
            {
                Cpf = "15235133064"
            };
            mockCollection.InsertOne(pessoa);

            mockContext.GetCollection<Pessoa>("Pessoa").Returns(mockCollection);
            using (var baixarArquivoRepository = new PessoaRepository(mockContext, "Pessoa"))
            {
                baixarArquivoRepository.AddAsync(pessoa).Wait();
            }
            mockContext.Received(1).Dispose();
        }
    }
}