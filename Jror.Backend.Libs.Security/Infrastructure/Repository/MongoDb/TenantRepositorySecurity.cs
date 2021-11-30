using Jror.Backend.Libs.Security.Abstractions.Entity;
using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;

namespace Jror.Backend.Libs.Security.Infrastructure.Repository.MongoDb
{
    public class TenantRepositorySecurity : MongoRepositorySecurity<Tenant>, ITenantRepositorySecurity
    {
        public TenantRepositorySecurity(IMongoContextSecurity context, string collectionName) : base(context, collectionName)
        {
        }
    }
}