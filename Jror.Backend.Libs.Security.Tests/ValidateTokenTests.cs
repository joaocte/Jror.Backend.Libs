using Jror.Backend.Libs.Security.Abstractions.Application;
using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using Jror.Backend.Libs.Security.Application;
using NSubstitute;
using System;
using Xunit;

namespace Jror.Backend.Libs.Security.Tests
{
    public class ValidateTokenTests
    {
        private readonly ITenantRepositorySecurity tenantRepository;
        private Guid clientIdGuid = new Guid("58402e4a-58fe-480d-91a8-20582b375447");
        private Guid clientSecretGuid = new Guid("3881777e-bcc8-4735-a9ed-6b716c59de5c");
        private readonly IValidateToken validateToken;
        public ValidateTokenTests()
        {
            tenantRepository = Substitute.For<ITenantRepositorySecurity>();
            tenantRepository.GetAsync(x =>
                x.ClientId == clientIdGuid && x.ClientSecret == clientSecretGuid && x.Status == "Ativo")
                .ReturnsForAnyArgs(new Abstractions.Entity.Tenant
                {
                    ClientId = clientIdGuid,
                    ClientSecret = clientSecretGuid,
                    Status = "Ativo"
                });

            validateToken = new ValidateToken(tenantRepository);
        }
        [Fact]          
        public void QuandoTokenEhValidoReturnTrue()
        {
            var token = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJDbGllbnRJZCI6IjU4NDAyZTRhLTU4ZmUtNDgwZC05MWE4LTIwNTgyYjM3NTQ0NyIsIkNsaWVudFNlY3JldCI6IjM4ODE3NzdlLWJjYzgtNDczNS1hOWVkLTZiNzE2YzU5ZGU1YyIsIlRlbmFudE5hbWUiOiJBcGlQZXNzb2EiLCJUZW5hbnRLZXkiOiJBcGlQZXNzb2EiLCJuYmYiOjE2NTUyMDEwNDQsImV4cCI6MjEyODU4NjY0NCwiaWF0IjoxNjU1MjAxMDQ0fQ.30gGiEoml4c30Yuk0PGk1tKOW2fQ8i4PC9mmKXQ5GDPKQE2jydwNTWlIUUHwxm3Gf7kM5AuQmGmoIcoDo_y8Qg";
            var retorno = validateToken.ExecuteAsync(token).Result;

            Assert.True(retorno);




        }
    }
}
