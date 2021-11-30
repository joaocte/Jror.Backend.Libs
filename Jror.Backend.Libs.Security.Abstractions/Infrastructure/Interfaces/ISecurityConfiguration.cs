using System.Collections.Generic;

namespace Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces
{
    public interface ISecurityConfiguration
    {
        string Connection { get; set; }
        string DataBaseName { get; set; }

        Dictionary<string, string> InMemoryCollection { get; }
    }
}