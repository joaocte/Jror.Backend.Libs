using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Jror.Backend.Libs.Security.Infrastructure
{
    public class SecurityConfiguration : ISecurityConfiguration
    {
        public SecurityConfiguration(string connection, string dataBaseName)
        {
            Connection = connection;
            DataBaseName = dataBaseName;
            InMemoryCollection = new()
            {
                { nameof(Connection), Connection },
                { nameof(DataBaseName), DataBaseName }
            };
        }

        public string Connection { get; set; }
        public string DataBaseName { get; set; }
        public Dictionary<string, string> InMemoryCollection { get; }
    }
}