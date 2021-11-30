using MongoDB.Driver;
using System;

namespace Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces
{
    public interface IMongoContextSecurity : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}