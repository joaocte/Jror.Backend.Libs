using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        Task AddCommand(Func<Task> func);

        Task<int> SaveChanges();

        IMongoCollection<T> GetCollection<T>(string name);
    }
}