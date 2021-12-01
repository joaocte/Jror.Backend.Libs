using Jror.Backend.Libs.Domain.Tests;
using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jror.Backend.Libs.Infrastructure.MongoDB.Repository;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.Tests.TestObjects
{
    public class PessoaRepository : MongoRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(IMongoContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}