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
        private Guid clientIdGuid = new Guid("7756ab70-0311-403b-a31d-c5dbbbf3e9cc");
        private Guid clientSecretGuid = new Guid("61d22235-27ad-4382-9680-c5e94273496f");
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
            var token = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJDbGllbnRJZCI6Ijc3NTZhYjcwLTAzMTEtNDAzYi1hMzFkLWM1ZGJiYmYzZTljYyIsIkNsaWVudFNlY3JldCI6IjYxZDIyMjM1LTI3YWQtNDM4Mi05NjgwLWM1ZTk0MjczNDk2ZiIsIlRlbmFudE5hbWUiOiJBcGlQZXNzb2EiLCJUZW5hbnRLZXkiOiJBcGlQZXNzb2EiLCJuYmYiOjE2NDc2MDA2MTUsImV4cCI6MTY0NzYwNzgxNSwiaWF0IjoxNjQ3NjAwNjE1fQ.d2jZfnkSGlXcEDkxatqhPidU2P9IlIRO0UoyUztePQdnpHL3RZ_LhIaJstR80-_YWAxwStWxW2S2rbjCQbSc2Q";

            var retorno = validateToken.ExecuteAsync(token).Result;

            Assert.True(retorno);




        }
    }
}
