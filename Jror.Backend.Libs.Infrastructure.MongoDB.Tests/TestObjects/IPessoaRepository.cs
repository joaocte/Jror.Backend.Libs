using Jror.Backend.Libs.Domain.Tests;
using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.Tests.TestObjects
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
    }
}