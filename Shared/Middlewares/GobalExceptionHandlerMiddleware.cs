using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace web_store_server.Shared.Middlewares
{
    public class GobalExceptionHandlerMiddleware
        : IMiddleware
    {
        private readonly ILogger<GobalExceptionHandlerMiddleware> _logger;
        private readonly ProblemDetailsFactory _factory;

        public GobalExceptionHandlerMiddleware(ILogger<GobalExceptionHandlerMiddleware> logger, ProblemDetailsFactory factory)
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
                _logger.LogError("¡Exception Ocurred!");
                _logger.LogError("[Source]: \n {Source}  \n [Message]: \n {Message} \n [StackTrace]: \n {StackTrace}", ex.Source, ex.Message, ex.StackTrace);

                var problemDetails = _factory.CreateProblemDetails(
                    httpContext: context,
                    statusCode: StatusCodes.Status500InternalServerError,
                    detail: $"Exception Ocurred - [Source]: {ex.Source} || [Message]: {ex.Message}");
                
                var json = JsonConvert.SerializeObject(problemDetails);

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(json);
            }
        }
    }
}
