using Jror.Backend.Libs.Domain.Abstractions.Exceptions;
using Jror.Backend.Libs.Framework.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Jror.Backend.Libs.Framework.Tests
{
    public class ExceptionFilterTests
    {
        private readonly ActionContext actionContext;
        private readonly ActionDescriptor actionDescriptor;
        private readonly ExceptionFilter exceptionFilter;
        private readonly ExceptionContext exceptionContext;
        private readonly List<IFilterMetadata> filters;
        private readonly HttpContext httpContext;
        private readonly RouteData routeData;

        public ExceptionFilterTests()
        {
            httpContext = Substitute.For<HttpContext>();
            routeData = Substitute.For<RouteData>();
            actionDescriptor = Substitute.For<ActionDescriptor>();
            actionContext = Substitute.For<ActionContext>(httpContext, routeData, actionDescriptor);
            filters = Substitute.For<List<IFilterMetadata>>();
            exceptionContext = Substitute.For<ExceptionContext>(actionContext, filters);
            exceptionFilter = new ExceptionFilter();
        }

        [Fact]
        public void Quando_Context_Tiver_AlreadyRegisteredException_Entao_Return_Status_Code_Conflict()
        {
            var expectedStatusCode = (int)HttpStatusCode.Conflict;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new AlreadyRegisteredException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Quando_Context_Tiver_BadRequestException_Entao_Return_Status_Code_BadRequest()
        {
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new BadRequestException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Quando_Context_Tiver_ForbiddenAccessException_Entao_Return_Status_Code_Forbidden()
        {
            var expectedStatusCode = (int)HttpStatusCode.Forbidden;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new ForbiddenAccessException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Quando_Context_Tiver_InfrastructureException_Entao_Return_Status_Code_ServiceUnavailable()
        {
            var expectedStatusCode = (int)HttpStatusCode.ServiceUnavailable;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new InfrastructureException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Quando_Context_Tiver_NotFoundException_Entao_Return_Status_Code_NotFound()
        {
            var expectedStatusCode = (int)HttpStatusCode.NotFound;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new NotFoundException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Quando_Context_Tiver_NoContentException_Entao_Return_Status_Code_NoContent()
        {
            var expectedStatusCode = (int)HttpStatusCode.NoContent;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new NoContentException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Quando_Context_Tiver_UnauthorizedException_Entao_Return_Status_Code_Unauthorized()
        {
            var expectedStatusCode = (int)HttpStatusCode.Unauthorized;
            var exceptionMsg = "msg-qualquer";
            Exception exception = new UnauthorizedException(exceptionMsg);
            exceptionContext.Exception.Returns(exception);
            var erroResultExpected = new ObjectResult(exception);
            exceptionFilter.OnException(exceptionContext);

            var result = (ObjectResult)exceptionContext.Result;

            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
    }
}