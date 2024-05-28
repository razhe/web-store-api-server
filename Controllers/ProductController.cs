using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Razor.Templating.Core;
using System.Text;
using web_store_mvc.Features.Products.Commands;
using web_store_mvc.Features.Products.Queries;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Dtos.Products;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public ProductController(ISender sender, ApiResponseHandler APIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = APIResultHandler;
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<DefaultAPIResponse<IEnumerable<GetProductDto>>>> GetAllProducts(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductsQuery(), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<IEnumerable<GetProductDto>>()
                    {
                        IsSuccess = true,
                        Message = "Proceso realizado exitosamente.",
                        Data = result.Data
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<IEnumerable<GetProductDto>>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Permite obtener un producto especifico
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{productId:Guid}")]
        public async Task<ActionResult<DefaultAPIResponse<GetProductDto>>> GetProductById(
            Guid productId,
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductByIdQuery(productId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<GetProductDto>()
                    {
                        IsSuccess = true,
                        Message = "Proceso realizado exitosamente.",
                        Data = result.Data
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<GetProductDto>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Permite crear un producto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DefaultAPIResponse<Guid>>> CreateProduct(
            CreateUpdateProductDto request,
            CancellationToken token)
        {
            var result = await _sender.Send(new CreateProductCommand(request), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<Guid>()
                    {
                        IsSuccess = true,
                        Message = "Proceso realizado exitosamente.",
                        Data = result.Data
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<Guid>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Permite actualizar un producto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="updateProductDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{productId:Guid}")]
        public async Task<ActionResult<DefaultAPIResponse<ProductDto>>> UpdateProduct(
            [FromRoute] Guid productId,
            [FromBody] CreateUpdateProductDto updateProductDto,
            CancellationToken token)
        {
            var result = await _sender.Send(new UpdateProductCommand(updateProductDto, productId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<ProductDto>()
                    {
                        IsSuccess = true,
                        Message = "Producto actualizado exitosamente.",
                        Data = result.Data
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<ProductDto>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Permite eliminar un producto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{productId:Guid}")]
        public async Task<ActionResult<DefaultAPIResponse<AnyType>>> DeleteProduct(
            Guid productId,
            CancellationToken token)
        {
            var result = await _sender.Send(new DeleteProductCommand(productId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<AnyType?>()
                    {
                        IsSuccess = true,
                        Message = "Proceso realizado exitosamente.",
                        Data = null
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<AnyType?>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    });
        }
    }
}
