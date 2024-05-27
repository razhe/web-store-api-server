using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Features.Dashboard.Queries;
using web_store_server.Features.Sales.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public DashboardController(ISender sender, ApiResponseHandler aPIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = aPIResultHandler;
        }

        /// <summary>
        /// Obtiene el resumen del dashboard
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("summary")]
        public async Task<ActionResult<DefaultAPIResponse<DashboardDto>>> GetDashboardSummary(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetDashboardQuery(), token);

            return _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<DashboardDto>()
                {
                    IsSuccess = true,
                    Message = "Proceso realizado exitosamente.",
                    Data = result.Data,
                });
        }
    }
}
