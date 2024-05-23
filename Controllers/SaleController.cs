using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Features.Sales.Commands;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ErrorResultHandler _errorResultHandler;

        public SaleController(ISender sender, ErrorResultHandler errorResultHandler)
        {
            _sender = sender;
            _errorResultHandler = errorResultHandler;
        }

        /// <summary>
        /// Permite realizar una venta
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateSale(
            IEnumerable<CreateSaleDto> sales,
            CancellationToken token)
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _sender.Send(new CreateSaleCommand(sales, userId), token);

            return result.IsSuccess ?
                Ok(result.Data) :
                _errorResultHandler.HandleError(HttpContext, StatusCodes.Status400BadRequest, result.Message);
        }
    }
}
