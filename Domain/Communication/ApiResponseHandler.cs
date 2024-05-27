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

        public ActionResult HandleDefaultResponse(int statusCode, dynamic response)
        {
            var result = new
            {
                response.IsSuccess,
                response.Message,
                response.Data,
            };

            return new ObjectResult(result)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = statusCode,
            };
        }

        public ActionResult HandleDefaultResponse(int statusCode, dynamic response, KeyValuePair<int, string> errors)
        {
            var result = new
            {
                response.Status,
                response.Message,
                response.Data,
                errors
            };

            return new ObjectResult(result)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = statusCode,
            };
        }

        public ActionResult HandleProblemDetailsError(HttpContext context, int statusCode, string detail)
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
