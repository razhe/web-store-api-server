using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace web_store_server.Domain.Communication
{
    public class ApiResponseHandler
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ApiResponseHandler(ProblemDetailsFactory problemDetailsFactory)
        {
            _problemDetailsFactory = problemDetailsFactory;
        }

        public ActionResult HandleError(HttpContext context, int statusCode, string detail)
        {
            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                context,
                statusCode: statusCode,
                title: "error",
                detail: detail
            );

            return new ObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = problemDetails.Status ?? 500
            };
        }
    }
}
