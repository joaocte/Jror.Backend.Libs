using System;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Security.Abstractions.Application
{
    public interface IValidateToken : IDisposable
    {
        Task<bool> ExecuteAsync(string token);
    }
}