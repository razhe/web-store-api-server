using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_store_mvc.Features.Products.Commands;
using web_store_mvc.Features.Products.Queries;
using web_store_server.Dtos.Products;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [ProducesResponseType(
            typeof(IEnumerable<GetProductDto>),
            StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductsQuery(), token);
            return Ok(result);
        }

        [ProducesResponseType(
            typeof(GetProductDto),
            StatusCodes.Status200OK)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProductById(
            Guid id,
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id), token);
            return StatusCode(200, result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            CreateProductDto request,
            CancellationToken token)
        {
            await _sender.Send(new CreateProductCommand(request), token);
            return StatusCode(201);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            UpdateProductDto updateProductDto,
            CancellationToken token)
        {
            await _sender.Send(new UpdateProductCommand(updateProductDto), token);
            return StatusCode(204);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(
            Guid id,
            CancellationToken token)
        {
            await _sender.Send(new DeleteProductCommand(id), token);
            return StatusCode(204);
        }
    }
}
