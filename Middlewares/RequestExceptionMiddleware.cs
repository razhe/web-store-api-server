using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using web_store_server.Common.Exceptions;

namespace web_store_server.Middlewares
{
    public class RequestExceptionMiddleware
        : IMiddleware
    {
        private readonly ILogger<RequestExceptionMiddleware> _logger;
        private readonly ProblemDetailsFactory _factory;

        public RequestExceptionMiddleware(ILogger<RequestExceptionMiddleware> logger, ProblemDetailsFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (ex is not RequestException)
                    throw;

                var exception = (RequestException)ex;

                _logger.LogInformation("Error {0} con el siguiente titulo: {2}",
                    exception.Code,
                    exception.Title);

                var json = string.Empty;

                if (exception.Errors is null)
                {
                    var problemDetails = _factory.CreateProblemDetails(
                        httpContext: context,
                        statusCode: exception.Code,
                        title: exception.Title);

                    json = JsonSerializer.Serialize(problemDetails);
                }
                else
                {
                    var modelStateDictionary = new ModelStateDictionary();

                    foreach (var error in exception.Errors)
                    {
                        modelStateDictionary.AddModelError(
                            error.PropertyName,
                            error.ErrorMessage);
                    }

                    var problemDetails = _factory.CreateValidationProblemDetails(
                        httpContext: context,
                        modelStateDictionary: modelStateDictionary,
                        statusCode: exception.Code,
                        title: exception.Title);

                    json = JsonSerializer.Serialize(problemDetails);
                }

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = exception.Code;

                await context.Response.WriteAsync(json);
            }
        }
    }
}
