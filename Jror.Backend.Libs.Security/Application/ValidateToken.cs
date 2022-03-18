using Jror.Backend.Libs.Security.Abstractions;
using Jror.Backend.Libs.Security.Abstractions.Application;
using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Security.Application
{
    public class ValidateToken : IValidateToken
    {
        private readonly ITenantRepositorySecurity tenantRepository;

        public ValidateToken(ITenantRepositorySecurity tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        public async Task<bool> ExecuteAsync(string token)
        {
            var validToken = token.Replace("bearer ", string.Empty
                , StringComparison.CurrentCultureIgnoreCase).TrimStart();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(validToken);
            var clientId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "ClientId")?.Value;
            var clientSecret = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "ClientSecret")?.Value;
            var tenantName = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "TenantName")?.Value;
            var tenantKey = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "TenantKey")?.Value;

            var clientIdGuid = string.IsNullOrWhiteSpace(clientId) ? Guid.Empty : new Guid(clientId);
            var clientSecretGuid = string.IsNullOrWhiteSpace(clientSecret) ? Guid.Empty : new Guid(clientSecret);

            var tenant = await tenantRepository.GetAsync(x =>
                x.ClientId == clientIdGuid && x.ClientSecret == clientSecretGuid && x.Status == "Ativo");

            if (tenant == null)
                throw new UnauthorizedAccessException("Token Informado é Iválido");

            var validationParameters = Constants.TokenValidationParameters;
            var principal = handler.ValidateToken(validToken, validationParameters, out _);
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                tenantRepository?.Dispose();
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }
    }
}