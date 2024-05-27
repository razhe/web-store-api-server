using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Features.Sales.Commands;

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
                _APIResultHandler.HandleDefaultResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<Guid>()
                    {
                        IsSuccess = true,
                        Message = "Venta registrada con éxito.",
                        Data = result.Data 
                    }) :
                _APIResultHandler.HandleDefaultResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<Guid>()
                    {
                        IsSuccess = false,
                        Message = "Venta registrada con éxito.",
                        Data = result.Data
                    });
        }
    }
}
