using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Brands;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Features.Brand.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public BrandController(ISender sender, ApiResponseHandler aPIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = aPIResultHandler;
        }

        public async Task<ActionResult<DefaultAPIResponse<IEnumerable<ProductDto>>>> GetBrands(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetBrandsQuery(), token);

            return _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<IEnumerable<BrandDto>>()
                {
                    IsSuccess = true,
                    Message = "Proceso realizado exitosamente.",
                    Data = result.Data
                });
        }
    }
}
