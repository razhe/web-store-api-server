using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_store_mvc.Features.Products.Queries;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Features.Sales.Commands;
using web_store_server.Features.Sales.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public SaleController(ISender sender, ApiResponseHandler APIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = APIResultHandler;
        }

        /// <summary>
        /// Permite realizar una venta
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DefaultAPIResponse<Guid>>> CreateSale(
            IEnumerable<CreateSaleDto> sales,
            CancellationToken token)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _sender.Send(new CreateSaleCommand(sales, userId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<Guid>()
                    {
                        IsSuccess = true,
                        Message = "Venta registrada con éxito.",
                        Data = result.Data 
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<Guid>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Entrega el historial de ventas
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<GetSaleDto>>> GetSalesHistory(
            [FromQuery]SalesHistoryQueryParams queryParams,
            CancellationToken token)
        {
            var result = await _sender.Send(new GetHistoryQuery(queryParams), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<IEnumerable<GetSaleDto>>()
                    {
                        IsSuccess = true,
                        Message = "Proceso realizado exitosamente.",
                        Data = result.Data,
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<IEnumerable<GetSaleDto>>()
                    {
                        IsSuccess = false,
                        Message= result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Entrega el reporte de ventas
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReport(
            [FromQuery] ReportQueryParams queryParams,
            CancellationToken token)
        {
            var result = await _sender.Send(new GetReportQuery(queryParams), token);

            return _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<IEnumerable<ReportDto>>()
                {
                    IsSuccess = true,
                    Message = "Proceso realizado exitosamente.",
                    Data = result.Data,
                });
        }
    }
}
